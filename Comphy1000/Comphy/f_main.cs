using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Bacodelib;

namespace Comfy1000
{
    public partial class f_main : Form
    {
        public f_main()
        {
            InitializeComponent();
            listBox1.Items.Clear();
            int i = 0;
            listBox1.Items.Add((++i).ToString()+ ". Приёмка товара");
            listBox1.Items.Add((++i).ToString() + ". Отгрузка товара");
            listBox1.Items.Add((++i).ToString() + ". Переучёт");
            listBox1.Items.Add((++i).ToString() + ". Проверка цен");
            listBox1.Items.Add((++i).ToString() + ". Отгрузка по ГМ");
            listBox1.Items.Add((++i).ToString() + ". Назначение МХ");
            //listBox1.Items.Add((++i).ToString() + ". Присв. нового ШК");
            listBox1.SelectedIndex = 0;

            #if !DEBUG
                linkLabel1.Text = "Версия: " + ver;
            #else
                linkLabel1.Text = "Версия: " + ver+"d";
            #endif
            linkLabel1.Text = "Версия: "+ver;
            label1.Text = "v"+ver + ". MC1000";

        }

        string ver = "4.0";
        bool ExitFlag = false;

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.D1) && ExitFlag)
                Application.Exit();
            else if (e.KeyCode == Keys.Enter)
            {
                ExitFlag = false;
                GoTask(listBox1.SelectedIndex);
            }
            else if (e.KeyCode == Keys.D9)
                ExitFlag = true;
            else if ((e.KeyCode >= Keys.D1) && (e.KeyCode <= Keys.D9))
            {
                ExitFlag = false;
                GoTask(e.KeyValue - 49);
            }
            else
                ExitFlag = false;
        }

        private void GoTask(int index)
        {
            switch (index)
            {
                case 0:
                    (new f_in(TaskKind.In)).ShowDialog();
                    break;
                case 1:
                    (new f_in(TaskKind.Out)).ShowDialog();
                    break;
                case 2:
                    (new f_in(TaskKind.Recalc)).ShowDialog();
                    break;
                case 3:
                    (new f_in(TaskKind.Check)).ShowDialog();
                    break;
                case 4:
                    (new f_in(TaskKind.OutPack)).ShowDialog();
                    break;
                case 5:
                    (new f_in(TaskKind.InPack)).ShowDialog();
                    break;
                //case 4:
                //    (new f_in(TaskKind.Grab)).ShowDialog();
                //    break;
            }
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("COMFY (TM) " + ver);
        }
    }
}