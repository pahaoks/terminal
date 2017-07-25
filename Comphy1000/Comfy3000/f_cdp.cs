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
    /// <summary>
    /// 
    /// </summary>
    public partial class f_cdp : TaskForm
    {
        /// приход
        /// отгрузка
        /// переучет
        /// проверка отгрузки
        private System.Windows.Forms.Label planCaption;
        private System.Windows.Forms.Label planValue;

        private bool notConfirmation;

        public override void AfterLoad()
        {
            dataGrid1.DataSource = task.TaskData;

            DataGridColumnStyle col = new DataGridTextBoxColumn();
            col.MappingName = "plan";
            col.HeaderText = "План";
            dataGrid1.TableStyles[0].GridColumnStyles.Add(col);

            col = new DataGridTextBoxColumn();
            col.MappingName = "count";
            col.HeaderText = "Факт";
            dataGrid1.TableStyles[0].GridColumnStyles.Add(col);

            col = new DataGridTextBoxColumn();
            col.MappingName = "driver";
            col.HeaderText = "Водитель";
            dataGrid1.TableStyles[0].GridColumnStyles.Add(col);

            dataGrid1.TableStyles[0].GridColumnStyles[1].Width = dataGrid1.TableStyles[0].GridColumnStyles[1].Width - 30;
            panel1.Location = new Point(0, 38);
            panel1.Visible = false;

            panel2.Location = new Point(0, 38);
            panel2.Visible = false;
        }

        public f_cdp() : base()
        {
            InitializeComponent();
            dataGrid1.Focus();
            Scanner.Instance.Enable();

            notConfirmation = false;
            autosavecounter = 0;
        }

        private void dataGrid1_KeyDown(object sender, KeyEventArgs e)
        {
            if (dataGrid1.DataSource != null)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    Scanner.Instance.Disable();
                    //Save();

                    if (Save())
                        this.Close();
                    else
                    {
                        dataGrid1.Focus();
                        Scanner.Instance.Enable();
                    }

                }
            }
        }


        /// <summary>
        /// Считан код
        /// </summary>
        /// <param name="Code">The code.</param>
        protected override void OnReadBarCode(string Code, string OriginalCode)
        {
            if (panel1.Visible)
            {
                if (!((CdpSortTask)task).ReadBarCode(Code))
                {
                    panel1.Visible = false;
                    return;
                }

                l_name.Text = ((CdpSortTask)task).vName;
                tb_Item.Text = ((CdpSortTask)task).vCode;
                tb_count.Text = ((CdpSortTask)task).vCount;

                panel1.Visible = false;

                InfoDialog info = new InfoDialog();
                info.l_itemIdText.Text = ((CdpSortTask)task).vCode;
                info.l_itemNameText.Text = ((CdpSortTask)task).vName;
                info.l_driverText.Text = ((CdpSortTask)task).Driver;
                info.ShowDialog();

                return;
            }
            if (!panel1.Visible)
            {
                if (!((CdpSortTask)task).ReadDeliveryBarCode(Code))
                {
                    return;
                }
                
                l_code.Text = ((CdpSortTask)task).DeliveryCode;
                l_name.Text = "";
                tb_Item.Text = "";

                panel1.Visible = true;
                return;
            }
        }

        private bool nonNumberEntered = false;

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
                    return (lastSaveError = task.Save()); // сохраняем и запоминаем признак ошибки
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

        private int autosavecounter;

        private void AutoSave()
        {
            if (++autosavecounter > 5)
            {
                autosavecounter = 0;
                Save();
                dataGrid1.Focus();
            }
        }

        private void tb_driver_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D0)
            {
                panel2.Visible = false;
            }
        }



    }
}