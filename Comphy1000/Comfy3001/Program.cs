using System;

using System.Collections.Generic;
using System.Windows.Forms;
using Comfy3000;
using Bacodelib;

namespace Comfy3001
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            //Application.Run(new f_main(true));
            Application.Run(new f_main( new[]
                  { TaskKind.In, 
                    TaskKind.OutWholesale, 
                    TaskKind.CheckOut, 
                    TaskKind.Recalc,
                    TaskKind.Grab} ));
        }
    }
}