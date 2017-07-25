using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Bacodelib;

namespace Comfy3000
{
    /// <summary>
    /// Диалог - выбор задания
    /// </summary>
    public partial class f_in : Form
    {
        private TaskKind kind = TaskKind.None;
        private System.Windows.Forms.Timer myTimer;
        private bool runTaskFlag;


        public f_in(TaskKind kind)
        {
            this.kind = kind;
            InitializeComponent();
            Scanner.Instance.OnReadBarCode += scanner_OnReadBarCode;
            Scanner.Instance.Enable();

            myTimer = new System.Windows.Forms.Timer();
            myTimer.Tick += new EventHandler(TimerEventProcessor);
            myTimer.Interval = 200;
            myTimer.Enabled = true;
        }

        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            if ( runTaskFlag )
            {
                runTaskFlag = false;
                myTimer.Enabled = false;
                RunTask();
                myTimer.Enabled = true;
            }
        }


        void scanner_OnReadBarCode(string Code, string OriginalCode)
        {
            if (listBox1.Items.Count < 1)
                return;

            int index = listBox1.Items.IndexOf(Code);
            if (index >= 0)
            {
                listBox1.SelectedIndex = index;
                runTaskFlag = true;
                //RunTask();
            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Scanner.Instance.OnReadBarCode -= scanner_OnReadBarCode;
                Scanner.Instance.Disable();
                this.Close();
            }
            else if ((e.KeyCode == Keys.Enter) && (listBox1.SelectedItem != null))
                RunTask();
        }

        private void RunTask()
        {
            if (listBox1.SelectedItem == null)
                return;

            listBox1.Enabled = false;
            Scanner.Instance.OnReadBarCode -= scanner_OnReadBarCode;
            Scanner.Instance.Disable();
            try
            {
                ShowTaskDialog(listBox1.SelectedItem.ToString());
            }
            finally
            {
                listBox1.Enabled = true;
                listBox1.Focus();
                Scanner.Instance.OnReadBarCode += scanner_OnReadBarCode;
                Scanner.Instance.Enable();
            }
        }

        private void ShowTaskDialog(string TaskName)
        {
            TaskForm forma;
            if (kind != TaskKind.None)
            {
                switch (kind)
                {
                    case TaskKind.In:
                    case TaskKind.Out:
                    case TaskKind.CheckOut:
                    case TaskKind.Recalc:
                    case TaskKind.InSup: 
                            forma = new f_goods(); break;
                    case TaskKind.OutWholesale: forma = new f_goods_wholesale(); break;
                    case TaskKind.Check: forma = new f_pcgoods(); break;
                    case TaskKind.Grab: forma = new f_skgoods(); break;
                    case TaskKind.CDPSort: forma = new f_cdp(); break;
                    default:
                        forma = new f_skgoods();
                        break;
                }
                Task task = Task.CreateTask(kind);
                try
                {
                    panelWait.Location = new Point(0, 62);
                    Application.DoEvents();
                    try
                    {
                        EventHandler<WaitEventArgs> lh = new EventHandler<WaitEventArgs>(f_WaitEvent);
                        task.WaitEvent += lh;
                        try
                        {
                            task.Load(TaskName);
                        }
                        finally
                        {
                            task.WaitEvent -= lh;
                        }
                    }
                    finally
                    {
                        l_wait.Text = "Обработка...";
                        panelWait.Location = new Point(653, 3);
                    }
                    forma.task = task;
                    forma.AfterLoad();
                    forma.Execute();
                }
                finally
                {
                    forma.Dispose();
                    task.Dispose();
                }
            }
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
                    l_wait.Text = e.Message;
                else
                    l_wait.Text = "Обработка...";
                Application.DoEvents();
            }
        }
        private void f_in_Load(object sender, EventArgs e)
        {
            label1.Text = KindNames.GetKindName(kind);
            Bacodelib.Task.GetTaskList(kind, listBox1.Items);
            if (listBox1.Items.Count > 0)
            {
                listBox1.Focus();
                listBox1.SelectedIndex = 0;
                //scanner.Enable();
            }
        }
    }
}