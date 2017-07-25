using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;


namespace Bacodelib
{
    public class PowerWatcher : IDisposable
    {
        private struct PowerStatus
        {
            public ACLineStatus ACLineStatus;
            public Byte BatteryFlag;
            public Byte BatteryLifePercent;
            public Byte Reserved1;
            public Int32 BatteryLifeTime;
            public Int32 BatteryFullLifeTime;
            public Byte Reserved2;
            public Byte BackupBatteryFlag;
            public Byte BackupBatteryLifePercent;
            public Byte Reserved3;
            public Int32 BackupBatteryLifeTime;
            public Int32 BackupBatteryFullLifeTime;
            public Int32 BatteryVoltage;
            public Int32 BatteryCurrent;
            public Int32 BatteryAverageCurrent;
            public Int32 BatteryAverageInterval;
            public Int32 BatterymAHourConsumed;
            public Int32 BatteryTemperature;
            public Int32 BackupBatteryVoltage;
            public Byte BatteryChemistry;
        }
        private enum ACLineStatus : byte
        {
            Offline = 0,
            Online = 1,
            Unknown = 255,
        }

        private Thread t;
        public event EventHandler<PowerEventArgs> OnPowerEvent;
        private PowerStatus st;
        private bool IsGo = false;
        Byte BoundaryPercent = 5;

        [DllImport("coredll.dll", EntryPoint = "GetSystemPowerStatusEx2", SetLastError = true)]
        private extern static Int32 GetSystemPowerStatus(ref PowerStatus powerStatus,
                                                        Int32 length,
                                                        Boolean update);

        private void ThreadProc()
        {
            while (true)
            {
                if (IsGo)
                {
                    GetSystemPowerStatus(ref st, Marshal.SizeOf(st),
                                                    true);
                    if ((st.ACLineStatus != ACLineStatus.Online) && (st.BatteryLifePercent <= BoundaryPercent))
                    {
                        RaisePowerEvent(new PowerEventArgs(st.BatteryLifePercent));
                    }
                }
                Thread.Sleep(1000);

            }
        }

        public void Run()
        {
            st = new PowerStatus();
            
            t = new Thread(new ThreadStart(ThreadProc));
            IsGo = true;
            t.Start();
        }

        protected virtual void RaisePowerEvent(PowerEventArgs e)
        {
            EventHandler<PowerEventArgs> handler = OnPowerEvent;
            if (handler != null)
            {
                IsGo = false;
                handler(this, e);
                IsGo = true;
            }
        }


        #region IDisposable Members

        public void Dispose()
        {
            if (t != null)
            {
                t.Abort();
            }
        }

        #endregion

    }

    public class PowerEventArgs : EventArgs
    {
        public PowerEventArgs(int percent)
        {
            msg = percent;
        }
        private int msg;
        public int Message
        {
            get { return msg; }
        } 
    }

}
