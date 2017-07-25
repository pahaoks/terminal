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

    }
}