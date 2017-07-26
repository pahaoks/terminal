using System;
using System.Collections.Generic;
using System.Windows.Forms;
//using Symbol.Barcode;
using Bacodelib;

namespace Comfy3000
{
    // 3000
    public delegate void LowBattery(int percent);

    public abstract class TaskForm : Form
    {
        private Bacodelib.PowerWatcher pw;
        public Bacodelib.Task task;
        protected bool lastSaveError; // признак ошибки при сохранениии

        public TaskForm()
        {
            Scanner.Instance.OnReadBarCode += OnReadBarCode;
            this.Closing += new System.ComponentModel.CancelEventHandler(TaskForm_Closing);
            lastSaveError = false;
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

        /// <summary>
        /// Сохранить двнные 
        /// </summary>
        /// <returns>true - успешно</returns>
        protected abstract bool Save();
        /*
        {
            return true;
        }
         */ 

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
            MessageBox.Show("Заряд батереи " + percent.ToString() + "%");
            if (!lastSaveError) // не сохранять при низком питании если УЖЕ была ошибка сохранения
            {
                if (task.NeedSave)
                {
                    Save();
                    MessageBox.Show("Низкий заряд батареи -" + percent.ToString() + "%.\nДанные задания сохранены.\nУстановите терминал на подзарядку.");
                }
                //Close();
            }
        }
    }


}
