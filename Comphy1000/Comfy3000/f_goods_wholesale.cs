using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using Symbol.Barcode;
using System.IO;
using Bacodelib;

namespace Comfy3000
{
    public partial class f_goods_wholesale : TaskForm
    {
        private System.Windows.Forms.Label planCaption;
        private System.Windows.Forms.Label planValue;
        private bool wait_serial = false;

        public override void AfterLoad()
        {
            dataGrid1.DataSource = task.TaskData;
        }
        public f_goods_wholesale()
            : base()
        {
            InitializeComponent();
            dataGrid1.Focus();
            Scanner.Instance.Enable();
        }

        private void dataGrid1_KeyDown(object sender, KeyEventArgs e)
        {
            if (dataGrid1.DataSource != null)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    Scanner.Instance.Disable();
                    Save();
                    this.Close();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    ShowInfoPanel();
                }
            }
        }

        private int FindBarCode(DataTable table, string Code)
        {
            int ret = -1;

            Cursor.Current = Cursors.WaitCursor;
            panelWait.Location = new Point(0, 38);
            Application.DoEvents();
            try
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if ((table.Rows[i][6].ToString() == Code) || (table.Rows[i][3].ToString() == Code) || (table.Rows[i][4].ToString() == Code) || (table.Rows[i][5].ToString() == Code))
                    {
                        ret = i;
                        break;
                    }

                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                panelWait.Location = new Point(574, 37);
            }
            return ret;
        }

        /// <summary>
        /// Called when [read bar code].
        /// </summary>
        /// <param name="Code">The code.</param>
        protected override void OnReadBarCode(string Code, string OriginalCode)
        {
            
            OutputWholesaleTask t = (OutputWholesaleTask) task;
            if (!wait_serial)
            {
                t.ReadBarCode(Code);
                if (t.vGridRow > -1)
                {
                    if (!t.HasSerial)
                    {
                        // sn не нужен
                        l_name.Text = t.vName;
                        l_code.Text = t.vCode;

                        l_count_doc.Text = t.vCountDoc;
                        tb_count.Text = t.vCount;
                        panel1.Location = new Point(0, 38);
                        tb_count.SelectAll();
                        tb_count.Focus();
                    }
                    else
                    {
                        // ожиданм ввода sn
                        l_name2.Text = t.vName;
                        l_code2.Text = t.vCode;
                        tb_sn.Text = String.Empty;
                        panel3.Location = new Point(0, 38);
                        tb_sn.Focus();
                        wait_serial = true;
                        Scanner.Instance.EnableAddBarCodes(true);

                    }
                }
            }
            else
            {
                tb_sn.Text = OriginalCode;
                tb_sn.SelectAll();
                tb_sn.Focus();
            }
        }

        private bool nonNumberEntered = false;

        private void tb_sn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                dataGrid1.Focus();
                panel3.Location = new Point(348, 352);
                wait_serial = false;
                Scanner.Instance.EnableAddBarCodes(false);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                ((OutputWholesaleTask)task).CountEntered("1");
                if (((OutputWholesaleTask)task).vGridRow > -1)
                {
                    ((OutputWholesaleTask) task).SerialNumberEntered(tb_sn.Text.Trim());
                    dataGrid1.Focus();
                    panel3.Location = new Point(348, 352);
                    wait_serial = false;
                    Scanner.Instance.EnableAddBarCodes(false);
                }
            }

        }

        private void tb_count_KeyDown(object sender, KeyEventArgs e)
        {
            nonNumberEntered = false;

            if (e.KeyCode == Keys.Enter)
            {
                ((OutputWholesaleTask)task).CountEntered(tb_count.Text);
                if (((OutputWholesaleTask)task).vGridRow > -1)
                {
                    panel1.Location = new Point(360, 35);
                    dataGrid1.Focus();
                    dataGrid1.CurrentCell = new DataGridCell(((OutputWholesaleTask)task).vGridRow, 0);
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                dataGrid1.Focus();
                panel1.Location = new Point(360, 35);
            }
            else if (((e.KeyCode < Keys.D0) || (e.KeyCode > Keys.D9)) && (e.KeyCode != Keys.Back))
            {
                nonNumberEntered = true;
            }
        }

        private void tb_count_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (nonNumberEntered == true)
            {
                // Stop the character from being entered into the control since it is non-numerical.
                e.Handled = true;
            }
        }

        protected override bool Save()
        {
            if (task.NeedSave)
            {
                Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                panelWait.Location = new Point(0, 38);
                Application.DoEvents();

                EventHandler<WaitEventArgs> lh = new EventHandler<WaitEventArgs>(f_WaitEvent);
                task.WaitEvent += lh;

                try
                {
                    return task.Save();
                }
                finally
                {
                    task.WaitEvent -= lh;
                    Cursor.Current = Cursors.Default;
                    panelWait.Location = new Point(673, 38);
                    Enabled = true;
                }
            }
            else
                return true;
        }

        void f_WaitEvent(object sender, WaitEventArgs e)
        {
            if (e.Is_err)
            {
                MessageBox.Show("Ошибка.\n" + e.Message);
            }
            else
            {
                if (e.Message != "")
                    label2.Text = e.Message;
                else
                    label2.Text = "Обработка...";
                Application.DoEvents();
            }
        }

        #region ShowInfoPanel
        /// <summary>
        /// Показать панель информации.
        /// </summary>
        private void ShowInfoPanel()
        {
            WareInfo info = task.GetInfo(dataGrid1.CurrentRowIndex);
            lName.Text = info.Name;
            lCode.Text = info.Code;
            lPlace.Text = info.Place + "/" + info.Adr;

            if (task.Kind == TaskKind.CheckOut)
            {
                planValue.Text = info.PlanOut.ToString();
            }
            panel2.Location = new Point(0, 38);
        }
        #endregion

        #region HideInfoPanel
        /// <summary>
        /// Показать панель информации.
        /// </summary>
        private void HideInfoPanel()
        {
            panel2.Location = new Point(3, 352);
        }
        #endregion

        private void dataGrid1_KeyUp(object sender, KeyEventArgs e)
        {
            if (task != null)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    HideInfoPanel();
                }
            }
        }


    }
}