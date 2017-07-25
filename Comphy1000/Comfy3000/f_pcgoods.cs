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
    /// проверка отгрузки
    /// </summary>
    public partial class f_pcgoods : TaskForm
    {
        public f_pcgoods() : base()
        {
            InitializeComponent();
            dataGrid1.Focus();
            Scanner.Instance.Enable();
        }

        public override void AfterLoad()
        {
            dataGrid1.DataSource = task.TaskData;
        }

        private void dataGrid1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Scanner.Instance.Disable();
                Save();
                this.Close();
            }
        }

        protected override void OnReadBarCode(string Code, string OriginalCode)
        {
            ((CheckTask)task).ReadBarCode(Code);
            if (((CheckTask)task).vGridRow > -1)
            {
                l_name.Text = ((CheckTask)task).vName;
                l_code.Text = ((CheckTask)task).vCode;
                label_flag.Text = ((CheckTask)task).vFlag;
                tb_count.Text = ((CheckTask)task).vCount;

                panel1.Location = new Point(0, 38);
                tb_count.Focus();
            }
        }

        private void tb_count_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ((CheckTask)task).SetCheck("1");
                dataGrid1.Focus();
                dataGrid1.CurrentCell = new DataGridCell(((CheckTask)task).vGridRow, 0);
                panel1.Location = new Point(360, 35);
            }
            if (e.KeyCode == Keys.Escape)
            {
                ((CheckTask)task).SetCheck("-1");
                dataGrid1.Focus();
                dataGrid1.CurrentCell = new DataGridCell(((CheckTask)task).vGridRow, 0);
                panel1.Location = new Point(360, 35);
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
                    label2.Text = e.Message;
                else
                    label2.Text = "Обработка...";
                Application.DoEvents();
            }
        }


    }
}