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

namespace Comfy1000
{
    public partial class f_goods : TaskForm
    {
        private bool notConfirmation;

        public override void AfterLoad()
        {
            dataGrid1.DataSource = task.TaskData;
        }
        public f_goods() : base()
        {
            InitializeComponent();
            dataGrid1.Focus();
            Scanner.Instance.Enable();
            notConfirmation = false;
            autosavecounter = 0;
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
            else if (e.KeyCode == Keys.Enter)
            { 
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

        private bool IntTryParse(string strValue, out int resultValue)
        {
            resultValue = 0;
            try
            {
                resultValue = Int32.Parse(strValue);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool DialogShowed = false;
        private int savedRowNumber = 0;

        protected override void OnReadBarCode(string Code, string OriginalCode)
        {
            switch (task.Kind)
            {
                case TaskKind.In:
                    {
                        ((InputTask)task).ReadBarCode(Code);
                        if (task.number > -1)
                        {
                            if (!notConfirmation)
                            {
                                if (!DialogShowed)
                                {
                                    l_name.Text = ((InputTask)task).vName;
                                    l_code.Text = ((InputTask)task).vCode;
                                    tb_count.Text = ((InputTask)task).vCount;
                                    panel1.Location = new Point(0, 38);
                                    tb_count.SelectAll();
                                    tb_count.Focus();
                                    savedRowNumber = task.number;
                                    DialogShowed = true;
                                    AutoSave();
                                }
                                else
                                {
                                    if (l_code.Text == ((InputTask)task).vCode)
                                    {
                                        l_name.Text = ((InputTask)task).vName;
                                        l_code.Text = ((InputTask)task).vCode;
                                        int prevValue = 0;
                                        if (IntTryParse(tb_count.Text, out prevValue))
                                        {
                                            int newValue = 0;
                                            if (IntTryParse(((InputTask)task).vCount, out newValue))
                                            {
                                                tb_count.Text = (prevValue + newValue).ToString();
                                            }
                                        }
                                        else
                                        {
                                            tb_count.Text = ((InputTask)task).vCount;
                                        }
                                        savedRowNumber = task.number;
                                        panel1.Location = new Point(0, 38);
                                        tb_count.SelectAll();
                                        tb_count.Focus();
                                        AutoSave();
                                    }
                                    else
                                    {
                                        var currentRowNumber = task.number;
                                        task.number = savedRowNumber;
                                        ((InputTask)task).CountEntered(tb_count.Text);
                                        task.number = currentRowNumber;
                                        savedRowNumber = task.number;

                                        l_name.Text = ((InputTask)task).vName;
                                        l_code.Text = ((InputTask)task).vCode;
                                        tb_count.Text = ((InputTask)task).vCount;
                                        panel1.Location = new Point(0, 38);
                                        tb_count.SelectAll();
                                        tb_count.Focus();
                                        AutoSave();
                                    }
                                }
                            }
                            else
                            {
                                ((InputTask)task).CountEntered("1");
                                dataGrid1.CurrentCell = new DataGridCell(((InputTask)task).vGridRow, 0);
                                dataGrid1.Focus();
                                AutoSave();
                            }
                        }
                    }
                    break;
                case TaskKind.Out:
                    {
                        ((OutputTask)task).ReadBarCode(Code);

                        if (((OutputTask)task).vGridRow > -1)
                        {
                            if (!notConfirmation)
                            {

                                l_name.Text = ((OutputTask)task).vName;
                                l_code.Text = ((OutputTask)task).vCode;
                                l_count_doc.Text = ((OutputTask)task).vCountDoc;
                                tb_count.Text = ((OutputTask)task).vCount;
                                panel1.Location = new Point(0, 38);
                                tb_count.SelectAll();
                                tb_count.Focus();
                            }
                            else
                            {
                                ((OutputTask)task).CountEntered("1");
                                if (((OutputTask)task).vGridRow > -1)
                                {
                                    dataGrid1.Focus();
                                    dataGrid1.CurrentCell = new DataGridCell(((OutputTask)task).vGridRow, 0);
                                    AutoSave();
                                }

                            }
                        }
                    }
                    break;
                case TaskKind.Recalc:
                    {
                        if (!((RecalcTask)task).ReadBarCode(Code))
                        {
                            Beeper.MessageBeep();
                        }

                        if (!notConfirmation)
                        {
                            l_name.Text = ((RecalcTask)task).vName;
                            l_code.Text = ((RecalcTask)task).vCode;
                            tb_count.Text = ((RecalcTask)task).vCount;
                            panel1.Location = new Point(0, 38);
                            tb_count.SelectAll();
                            tb_count.Focus();
                        }
                        else
                        {
                            ((RecalcTask)task).CountEntered("1");
                            dataGrid1.CurrentCell = new DataGridCell(((RecalcTask)task).vGridRow, 0);
                            dataGrid1.Focus();
                            AutoSave();
                        }


                    }
                    break;
            }
        }

        private bool nonNumberEntered = false;
    
        private void tb_count_KeyDown(object sender, KeyEventArgs e)
        {
            nonNumberEntered = false;

            if (e.KeyCode == Keys.Enter)
            {
                switch (task.Kind)
                {
                    case TaskKind.In:
                        {
                            ((InputTask)task).CountEntered(tb_count.Text);
                            dataGrid1.CurrentCell = new DataGridCell(((InputTask)task).vGridRow, 0);
                            panel1.Location = new Point(360, 35);
                            dataGrid1.Focus();
                            DialogShowed = false;
                            tb_count.Text = "0";
                            AutoSave();
                        }
                        break;
                    case TaskKind.Out:
                        {
                            ((OutputTask)task).CountEntered(tb_count.Text);
                            if (((OutputTask)task).vGridRow > -1)
                            {
                                panel1.Location = new Point(360, 35);
                                dataGrid1.Focus();
                                dataGrid1.CurrentCell = new DataGridCell(((OutputTask)task).vGridRow, 0);
                                AutoSave();
                            }
                        }
                        break;
                    case TaskKind.Recalc:
                        {
                            ((RecalcTask)task).CountEntered(tb_count.Text);
                            dataGrid1.CurrentCell = new DataGridCell(((RecalcTask)task).vGridRow, 0);
                            panel1.Location = new Point(360, 35);
                            dataGrid1.Focus();
                            AutoSave();
                        }
                        break;
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                dataGrid1.Focus();
                panel1.Location = new Point(360, 35);
                DialogShowed = false;
                tb_count.Text = "0";
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

        private int autosavecounter;

        private void AutoSave()
        {
            if (++autosavecounter > 100)
            {
                autosavecounter = 0;
                Save();
                dataGrid1.Focus();
            }
        }

    }
}