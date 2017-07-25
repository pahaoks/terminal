using System;
using System.Collections.Generic;
using System.Windows.Forms;
//using Symbol.Barcode;
using Bacodelib;

namespace Comfy3000
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            //Application.Run(new f_main( false ));
            Application.Run(new f_main(new[]
                  { TaskKind.InSup,
                    TaskKind.In, 
                    TaskKind.Out, 
                    //TaskKind.CheckOut, 
                    TaskKind.Recalc,
                    TaskKind.Check,
                    TaskKind.CDPSort}));
        }
    }


}