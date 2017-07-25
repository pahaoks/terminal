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
    public partial class f_goods : TaskForm
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
            if (task.Kind == TaskKind.CheckOut)
            {
                DataGridColumnStyle col = new DataGridTextBoxColumn();
                col.MappingName = "plan";
                col.HeaderText = "План";
                dataGrid1.TableStyles[0].GridColumnStyles.Add(col);

                dataGrid1.TableStyles[0].GridColumnStyles[1].Width = dataGrid1.TableStyles[0].GridColumnStyles[1].Width - 30;

                lName.Height = 80;
                label10.Top = 119;
                lCode.Top = 149;
                label3.Top = 228 - 47;
                lPlace.Top = 258 - 47;

                planCaption = new System.Windows.Forms.Label();
                this.panel2.Controls.Add(planCaption);
                planCaption.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
                planCaption.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
                planCaption.Location = new System.Drawing.Point(3, 258);
                planCaption.Name = "planCaption";
                planCaption.Size = new System.Drawing.Size(200, 32);
                planCaption.Text = "План отгрузки";


                planValue = new System.Windows.Forms.Label();
                this.panel2.Controls.Add(planValue);
                planValue.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
                planValue.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
                planValue.Location = new System.Drawing.Point(210, 258);
                planValue.Name = "planValue";
                planValue.Size = new System.Drawing.Size(100, 32);
            }
            else if (task.Kind == TaskKind.InSup)
            {
                DataGridColumnStyle col = new DataGridTextBoxColumn();
                col.MappingName = "plan";
                col.HeaderText = "План";
                dataGrid1.TableStyles[0].GridColumnStyles.Add(col);

                dataGrid1.TableStyles[0].GridColumnStyles[1].Width = dataGrid1.TableStyles[0].GridColumnStyles[1].Width - 30;
            }
            else if (task.Kind == TaskKind.Recalc)
            {

            }
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
                else if (e.KeyCode == Keys.Enter)
                {
                    ShowInfoPanel();
                }
                else if (e.KeyCode == Keys.Space)
                {
                    if (task.Kind == TaskKind.Recalc)
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
                else if (e.KeyCode == Keys.F1)
                {
                    Scanner.Instance.OnReadBarCode -= OnReadBarCode;
                    f_recalcItemLookup lookUpDialog = new f_recalcItemLookup(
                                                        task, 
                                                        task.TaskData.Rows[dataGrid1.CurrentCell.RowNumber]["good_code"].ToString()
                                                      );
                    lookUpDialog.ShowDialog();
                    ((RecalcTask)task).NeedSave = lookUpDialog.NeedSave || ((RecalcTask)task).NeedSave;
                    Scanner.Instance.OnReadBarCode += OnReadBarCode;
                    dataGrid1.CurrentCell = new DataGridCell(lookUpDialog.CurrentRowNum, 0);
                }
            }
        }

        /// <summary>
        /// Считан код
        /// </summary>
        /// <param name="Code">The code.</param>
        protected override void OnReadBarCode(string Code, string OriginalCode)
        {
            switch (task.Kind)
            {
                case TaskKind.In:
                    {
                        ((InputTask)task).ReadBarCode(Code);
                        l_name.Text = ((InputTask)task).vName;
                        l_code.Text = ((InputTask)task).vCode;
                        tb_count.Text = ((InputTask)task).vCount;
                        panel1.Location = new Point(0, 38);
                        tb_count.SelectAll();
                        tb_count.Focus();
                    }
                    break;
                case TaskKind.Out:
                    {
                        ((OutputTask)task).ReadBarCode(Code);
                        if (((OutputTask)task).vGridRow > -1)
                        {
                            l_name.Text = ((OutputTask)task).vName;
                            l_code.Text = ((OutputTask)task).vCode;
                            l_count_doc.Text = ((OutputTask)task).vCountDoc;
                            tb_count.Text = ((OutputTask)task).vCount;
                            panel1.Location = new Point(0, 38);
                            tb_count.SelectAll();
                            tb_count.Focus();
                        }
                    }
                    break;
                case TaskKind.CheckOut:
                    {
                        ((CheckOutputTask)task).ReadBarCode(Code);
                        if (((CheckOutputTask)task).vGridRow > -1)
                        {
                            l_name.Text = ((CheckOutputTask)task).vName;
                            l_code.Text = ((CheckOutputTask)task).vCode;
                            l_count_doc.Text = ((CheckOutputTask)task).vCountDoc;
                            tb_count.Text = ((CheckOutputTask)task).vCount;
                            panel1.Location = new Point(0, 38);
                            tb_count.SelectAll();
                            tb_count.Focus();
                        }
                    }
                    break;
                case TaskKind.Recalc:
                    {
                        if (!((RecalcTask)task).ReadBarCode(Code))
                        {
                            Beeper.MessageBeep(16);
                            Beeper.MessageBeep(16);
                            Beeper.MessageBeep(16);
                        }

                        if (!notConfirmation)
                        {
                            l_name.Text = ((RecalcTask)task).vName;
                            l_code.Text = ((RecalcTask)task).vCode;
                            tb_count.Text = ((RecalcTask)task).vCount;
                            l_count_doc.Text = ((RecalcTask)task).place;
                            panel1.Location = new Point(0, 38);
                            tb_count.SelectAll();
                            tb_count.Focus();
                        }
                        else
                        {
                            ((RecalcTask)task).CountEntered("1");
                            dataGrid1.CurrentCell = new DataGridCell(((RecalcTask)task).vGridRow, 0);
                            dataGrid1.Focus();
                            // AutoSave();
                        }
                    }
                    break;
                case TaskKind.InSup:
                    {
                        ((InputFromSupplierTask)task).ReadBarCode(Code);
                        l_name.Text = ((InputFromSupplierTask)task).vName;
                        l_code.Text = ((InputFromSupplierTask)task).vCode;
                        tb_count.Text = ((InputFromSupplierTask)task).vCount;
                        panel1.Location = new Point(0, 38);
                        tb_count.SelectAll();
                        tb_count.Focus();
                    }
                    break;
            }
        }

        private bool nonNumberEntered = false;

        /// <summary>
        /// Нажатие клавиши в окне аодтверждения ввода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    case TaskKind.CheckOut:
                        {
                            ((CheckOutputTask)task).CountEntered(tb_count.Text);
                            if (((CheckOutputTask)task).vGridRow > -1)
                            {
                                panel1.Location = new Point(360, 35);
                                dataGrid1.Focus();
                                dataGrid1.CurrentCell = new DataGridCell(((CheckOutputTask)task).vGridRow, 0);
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
                            //AutoSave();
                        }
                        break;
                    case TaskKind.InSup:
                        {
                            ((InputFromSupplierTask)task).CountEntered(tb_count.Text);
                            dataGrid1.CurrentCell = new DataGridCell(((InputFromSupplierTask)task).vGridRow, 0);
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

        /// <summary>
        /// Показать панель информации.
        /// </summary>
        private void HideInfoPanel()
        {
            panel2.Location = new Point(3, 352);
        }

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
    }
}