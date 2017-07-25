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
    /// проверка цен
    /// </summary>
    public partial class f_skgoods : TaskForm
    {
        public override void AfterLoad()
        {
            dataGrid1.DataSource = task.TaskData;
        }

        public f_skgoods() : base()
        {
            InitializeComponent();
            dataGrid1.Focus();
            Scanner.Instance.Enable();
        }

        private bool CheckPressed = false;

        private void dataGrid1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Scanner.Instance.Disable();
                Save();
                this.Close();
            }
            if (e.KeyCode == Keys.Enter)
            {
                textBox1.Text = dataGrid1[dataGrid1.CurrentRowIndex, 1].ToString();
                button1_Click(null, null);
            }
        }
        protected override void OnReadBarCode(string Code, string OriginalCode)
        {
            if (tb_code.Focused)
                tb_code.Text = Code;
            else
            {
                if (((GrabTask)task).FindByWareCode(Code) > 0)
                {
                    l_name.Text = ((GrabTask)task).vName;
                    tb_code.Text = ((GrabTask)task).vCode;
                    l_good_code.Text = ((GrabTask)task).vGoodCode;
                    checkBox1.Checked = ((GrabTask)task).V_checked;
                    ShowPropertyPanel();
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (((GrabTask)task).FindByCode(textBox1.Text) > 0)
            {
                l_name.Text = ((GrabTask)task).vName;
                tb_code.Text = ((GrabTask)task).vCode;
                l_good_code.Text = ((GrabTask)task).vGoodCode;
                checkBox1.Checked = ((GrabTask)task).V_checked;
                ShowPropertyPanel();
            }
            textBox1.Text = "";
        }
        private void ShowPropertyPanel()
        {
            panel1.Location = new Point(3, 35);
            tb_code.Focus();
            dataGrid1.TabStop = false;
            textBox1.TabStop = false;
        }
        private void HidePropertyPanel()
        {
            panel1.Location = new Point(360, 35);
            dataGrid1.TabStop = true;
            textBox1.TabStop = true;


        }
        private void button2_Click(object sender, EventArgs e)
        {
            dataGrid1.Focus();
            if (tb_code.Text.Trim().Length >= 8)
            {
                ((GrabTask)task).SetCheck(false);
                dataGrid1.Select(((GrabTask)task).SetCode(tb_code.Text));
            }
            else
            {
                ((GrabTask)task).SetCheck(true);
                dataGrid1.Select(((GrabTask)task).SetCode(""));
            }
            HidePropertyPanel();
        }
        private void tb_code_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2_Click(null, null);
            }
            if (e.KeyCode == Keys.Escape)
            {
                button3_Click(null, null);
            }
            if (e.KeyCode == (Keys.F17 | Keys.LButton | Keys.MButton | Keys.Back | Keys.ShiftKey | Keys.Space))
            {
                checkBox1.Checked = !checkBox1.Checked;
                CheckPressed = true;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            dataGrid1.Focus();
            HidePropertyPanel();
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Scanner.Instance.Disable();
                Save();
                this.Close();
            }
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(null, null);
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
                    panelWait.Location = new Point(574, 37);
                    Enabled = true;
                }
            }
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
                    label6.Text = e.Message;
                else
                    label6.Text = "Обработка...";
                Application.DoEvents();
            }
        }

        private void tb_code_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (CheckPressed)
            {
                CheckPressed = false;
                e.Handled = true;
            }
        }
    }
}