using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Bacodelib;
using System.Windows;
using System.Drawing;
using CardExportLib;
//using Symbol.Barcode;

namespace Comfy1000
{
    static class Program
    {
        static CardExportIface ceInterface;
        static bool CardExportConnected;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// +

        [MTAThread]
        static void Main()
        {
            startCardExport();
            try
            {
                Application.Run(new f_main());
            }
            finally
            {
                endCardExport();
            }

        }

        static void startCardExport()
        {
            #if !DEBUG
                ceInterface = new CardExportIface();
                CardExportConnected = (ceInterface.Connect() == 1);
                if (!CardExportConnected)
                    MessageBox.Show("Can't connect to Card Export driver interface");
                if ( 1 != ceInterface.SendCommand(CardExportIface.CHANGE_USB_CLIENT_TO_MASS_STORAGE))
                    MessageBox.Show("Error switching to CardExport");
            #endif
        }

        static void endCardExport()
        {
            #if !DEBUG
            if (CardExportConnected)
            {
                ceInterface.SendCommand(CardExportIface.CHANGE_USB_CLIENT_TO_DEFAULT);
                ceInterface.Disconnect();
            }
            #endif
        }

    }

    public class TaskForm : Form
    {
        private System.Windows.Forms.Panel panelWait;
        private System.Windows.Forms.Label labelWait;


        private Bacodelib.PowerWatcher pw;
        public Bacodelib.Task task;
        public delegate void LowBattery(int percent);
        protected bool lastSaveError; // признак ошибки при сохранениии


        public TaskForm()
        {
            this.panelWait = new System.Windows.Forms.Panel();
            this.labelWait = new System.Windows.Forms.Label();
            this.panelWait.SuspendLayout();

            this.panelWait.Controls.Add(this.labelWait);
            this.panelWait.Location = new System.Drawing.Point(540, 38);
            this.panelWait.Name = "panelWait";
            this.panelWait.Size = new System.Drawing.Size(240, 198);
            // 
            // labelWait
            // 
            this.labelWait.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.labelWait.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelWait.Location = new System.Drawing.Point(3, 84);
            this.labelWait.Name = "labelWait";
            this.labelWait.Size = new System.Drawing.Size(234, 60);
            this.labelWait.Text = "Обработка...";
            this.labelWait.TextAlign = System.Drawing.ContentAlignment.TopCenter;

            this.Controls.Add(this.panelWait);
            this.panelWait.ResumeLayout(false);

            this.Closing += new System.ComponentModel.CancelEventHandler(TaskForm_Closing);
            Scanner.Instance.OnReadBarCode += OnReadBarCode;
        }

        void TaskForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Scanner.Instance.OnReadBarCode -= OnReadBarCode;
        }


        protected virtual void OnReadBarCode(string Code, string OriginalCode)
        {
        }

        public virtual void AfterLoad()
        {
        }

        public void Execute()
        {
            pw = new Bacodelib.PowerWatcher();
            pw.OnPowerEvent += HandlePowerEvent;
            try
            {
                pw.Run();
                ShowDialog();
            }
            finally
            {
                pw.OnPowerEvent -= HandlePowerEvent;
                pw.Dispose();
            }
        }

        private void HandlePowerEvent(object sender, Bacodelib.PowerEventArgs e)
        {
            Invoke(new LowBattery(OnLowBattery), new Object[] { e.Message });
        }
        protected void OnLowBattery(int percent)
        {
            if (!lastSaveError) // не сохранять при низком питании если УЖЕ была ошибка сохранения
            {

                if (task.NeedSave)
                {
                    Save();
                    MessageBox.Show("Низкий заряд батареи -" + percent.ToString() + "%.\nДанные задания сохранены.\nУстановите терминал на подзарядку.");
                }
            }
            //Close();
        }

        protected bool Save()
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
                    panelWait.Location = new Point(574, 37);
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
                    labelWait.Text = e.Message;
                else
                    labelWait.Text = "Обработка...";
                Application.DoEvents();
            }
        }

    }

}