using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Comfy3000
{
    public partial class InfoDialog : Form
    {
        public InfoDialog()
        {
            InitializeComponent();
        }

        private void InfoDialog_KeyDown(object sender, KeyEventArgs e)
        {
            this.Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}