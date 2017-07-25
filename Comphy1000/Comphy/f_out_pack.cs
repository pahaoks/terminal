using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Bacodelib;

namespace Comfy1000
{
    public partial class f_out_pack : TaskForm
    {
        private bool notConfirmation;

        public f_out_pack()
        {
            InitializeComponent();
            dataGrid1.Focus();
            Scanner.Instance.Enable();
            notConfirmation = false;
            autosavecounter = 0;
        }

        public override void AfterLoad()
        {
            dataGrid1.DataSource = task.TaskData;
            label1.Text = "Тек. место:" + ((OutputPackTask)task).CurrentPack.ToString();
        }

        protected override void OnReadBarCode(string Code, string OriginalCode)
        {
            ((OutputPackTask)task).ReadBarCode(Code);

            if (((OutputPackTask)task).vGridRow > -1)
            {
                if (!notConfirmation)
                {

                    l_name.Text = ((OutputPackTask)task).vName;
                    l_code.Text = ((OutputPackTask)task).vCode;
                    tb_count.Text = ((OutputPackTask)task).Pack.ToString();
                    panel1.Location = new Point(0, 38);
                    tb_count.SelectAll();
                    tb_count.Focus();
                }
                else
                {
                    ((OutputPackTask)task).CountEntered(((OutputPackTask)task).CurrentPack.ToString(), false);
                    if (((OutputPackTask)task).vGridRow > -1)
                    {
                        dataGrid1.Focus();
                        dataGrid1.CurrentCell = new DataGridCell(((OutputPackTask)task).vGridRow, 0);
                        AutoSave();
                    }
                }
            }

        }

        private bool nonNumberEntered = false;

        private void tb_count_KeyDown(object sender, KeyEventArgs e)
        {
            nonNumberEntered = false;

            if (e.KeyCode == Keys.Enter)
            {
                ((OutputPackTask)task).CountEntered(tb_count.Text, string.IsNullOrEmpty(l_code.Text)); // string.IsNullOrEmpty(l_code.Text) - признак редактирования
                if (((OutputPackTask)task).vGridRow > -1)
                {
                    panel1.Location = new Point(360, 35);
                    dataGrid1.Focus();
                    dataGrid1.CurrentCell = new DataGridCell(((OutputPackTask)task).vGridRow, 0);
                    label1.Text = "Тек. место:" + ((OutputPackTask) task).CurrentPack.ToString();
                    AutoSave();
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

        private void dataGrid1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Scanner.Instance.Disable();
                //Save();
                //this.Close();
                if (Save())
                    this.Close();
                else
                {
                    dataGrid1.Focus();
                    Scanner.Instance.Enable();
                }
            }
            if (e.KeyCode == Keys.D1)
            {
                label1.Text = "Тек. место:" + ((OutputPackTask)task).IncCurrentPack().ToString();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                ((OutputPackTask) task).vGridRow = dataGrid1.CurrentRowIndex;
                l_name.Text = dataGrid1[dataGrid1.CurrentRowIndex, 0].ToString();
                l_code.Text = string.Empty;
                tb_count.Text = dataGrid1[dataGrid1.CurrentRowIndex, 1].ToString();
                panel1.Location = new Point(0, 38);
                tb_count.SelectAll();
                tb_count.Focus();
            }
            else if (e.KeyCode == Keys.Space)
            {
                string message = "Подтверждение сканирования сейчас "
                    + ((notConfirmation) ? "ОТКЛЮЧЕНО" : "ВКЛЮЧЕНО")
                    + ".\n"
                    + ((notConfirmation) ? "Включить" : "Выключить")
                    + "?";
                DialogResult result = MessageBox.Show(message,
                    "Настойка подтверждения",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.None,
                    MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                    notConfirmation = !notConfirmation;
            }

        }

        private int autosavecounter;

        private void AutoSave()
        {
            if (++autosavecounter > 20)
            {
                autosavecounter = 0;
                Save();
                dataGrid1.Focus();
            }
        }

        private void ShowPackList()
        {
            dataGrid2.Location = new Point(0, 38);
            var datatable =  new DataTable("PackList");
            datatable.Columns.Add("pack");
            datatable.Columns.Add("item_count");

            //            ((DataTable) dataGrid1.DataSource).Rows.

            dataGrid1.DataSource = datatable;
        }
    }

}