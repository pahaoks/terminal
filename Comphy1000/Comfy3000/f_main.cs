using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Bacodelib;

namespace Comfy3000
{
    public partial class f_main : Form
    {
        public f_main()
        {
            InitializeComponent();
        }

        //public f_main(bool is_opt)
        //    : this()
        public f_main( TaskKind[] kinds ) : this()
        {
            Task.IsCartAvaliable();

            listBox1.Items.Clear();
            int i = 0;
            foreach (TaskKind k in kinds)
                listBox1.Items.Add(new MenuItem(++i, k));

            /*
            if (!is_opt)
            {
                listBox1.Items.Add(new MenuItem(++i, TaskKind.In));
                listBox1.Items.Add(new MenuItem(++i, TaskKind.Out));
                //listBox1.Items.Add(new MenuItem(++i, TaskKind.CheckOut));
                listBox1.Items.Add(new MenuItem(++i, TaskKind.Recalc));
                listBox1.Items.Add(new MenuItem(++i, TaskKind.Check));
                //listBox1.Items.Add(new MenuItem(++i, TaskKind.Grab));
            }
            else
            {
                listBox1.Items.Add(new MenuItem(++i, TaskKind.In));
                listBox1.Items.Add(new MenuItem(++i, TaskKind.OutWholesale));
                listBox1.Items.Add(new MenuItem(++i, TaskKind.CheckOut));
                listBox1.Items.Add(new MenuItem(++i, TaskKind.Recalc));
                listBox1.Items.Add(new MenuItem(++i, TaskKind.Grab));
            }
             */ 

            listBox1.SelectedIndex = 0;
            linkLabel1.Text = "Верисия: " + ver;
        }


        public class MenuItem
        {
          private TaskKind kind;
          private int num;
          public MenuItem( int num, TaskKind kind)
          {
              this.kind = kind;
              this.num = num;
          }

          public int Num
          {
              get { return num; }
          }
          public TaskKind Kind
          {
              get { return kind; }
          }

            public override string ToString()
          {
              return String.Format("{0}.{1}", num, KindNames.GetKindName(kind));
          }
        }

        string ver = "7.0";
        bool ExitFlag = false;

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.D1) && ExitFlag)
                Application.Exit();
            else if (e.KeyCode == Keys.Enter)
            {
                ExitFlag = false;
                (new f_in( ((MenuItem)listBox1.SelectedItem).Kind )).ShowDialog();
            }
            else if (e.KeyCode == Keys.D9)
                ExitFlag = true;
            else if ((e.KeyCode >= Keys.D1) && (e.KeyCode <= Keys.D9))
            {
                ExitFlag = false;
                MenuItem mi = MenuItemByIndex(e.KeyValue - 48);
                if (mi != null)
                    (new f_in(mi.Kind)).ShowDialog();
            }
            else
                ExitFlag = false;
        }

        private MenuItem MenuItemByIndex( int index)
        {
            MenuItem retval = null;
            foreach (MenuItem mi in listBox1.Items)
                if (mi.Num == index)
                {
                    retval = mi;
                    break;
                }
            return retval;
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("COMFY (TM) "+ver);
        }
    }
}