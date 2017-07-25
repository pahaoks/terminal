using System;

using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using Symbol.Audio;

namespace Bacodelib
{
    public class Beeper
    {
        [DllImport("CoreDll.dll")]
        public static extern void MessageBeep(int code);

        public static void MessageBeep()
        {
            //MessageBeep(1);  // Default beep code is -1
            if (Device.AvailableDevices.Length > 0)
            {
                Controller c = new StandardAudio(Device.AvailableDevices[0]);
                //string WaveFile = "\\Windows\\question.wav";
                //c.PlayAudio(1000, 10000, WaveFile);
                c.PlayAudio(1000, 10000);
                Thread.Sleep(1010);
                c.Dispose();
            }
            //StandardAudio a = new StandardAudio( Device.AvailableDevices[0] );
            
        }


    }
}
