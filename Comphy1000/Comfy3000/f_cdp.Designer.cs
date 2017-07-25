namespace Comfy3000
{
    partial class f_cdp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_Item = new System.Windows.Forms.TextBox();
            this.l_count_doc = new System.Windows.Forms.Label();
            this.l_name = new System.Windows.Forms.Label();
            this.l_code = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_count = new System.Windows.Forms.TextBox();
            this.tb_itemId = new System.Windows.Forms.TextBox();
            this.panelWait = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tb_driver = new System.Windows.Forms.TextBox();
            this.l_driver = new System.Windows.Forms.Label();
            this.lCode = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lName = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lPlace = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panelWait.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(320, 32);
            this.label1.Text = "Список товаров:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.dataGrid1.Location = new System.Drawing.Point(3, 38);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.RowHeadersVisible = false;
            this.dataGrid1.Size = new System.Drawing.Size(317, 279);
            this.dataGrid1.TabIndex = 8;
            this.dataGrid1.TableStyles.Add(this.dataGridTableStyle1);
            this.dataGrid1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGrid1_KeyDown);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn1);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn2);
            this.dataGridTableStyle1.MappingName = "t_main";
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "Наименование";
            this.dataGridTextBoxColumn1.MappingName = "name";
            this.dataGridTextBoxColumn1.Width = 215;
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.HeaderText = "Код доставки";
            this.dataGridTextBoxColumn2.MappingName = "delivery_code";
            this.dataGridTextBoxColumn2.Width = 150;
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Format = "";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.tb_Item);
            this.panel1.Controls.Add(this.l_count_doc);
            this.panel1.Controls.Add(this.l_name);
            this.panel1.Controls.Add(this.l_code);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.tb_count);
            this.panel1.Location = new System.Drawing.Point(326, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(327, 297);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular);
            this.label6.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label6.Location = new System.Drawing.Point(6, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 38);
            this.label6.Text = "Товар:";
            // 
            // tb_Item
            // 
            this.tb_Item.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.tb_Item.Location = new System.Drawing.Point(102, 60);
            this.tb_Item.Name = "tb_Item";
            this.tb_Item.Size = new System.Drawing.Size(195, 39);
            this.tb_Item.TabIndex = 6;
            // 
            // l_count_doc
            // 
            this.l_count_doc.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.l_count_doc.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Regular);
            this.l_count_doc.Location = new System.Drawing.Point(102, 235);
            this.l_count_doc.Name = "l_count_doc";
            this.l_count_doc.Size = new System.Drawing.Size(78, 35);
            // 
            // l_name
            // 
            this.l_name.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.l_name.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.l_name.Location = new System.Drawing.Point(3, 135);
            this.l_name.Name = "l_name";
            this.l_name.Size = new System.Drawing.Size(294, 92);
            // 
            // l_code
            // 
            this.l_code.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.l_code.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.l_code.Location = new System.Drawing.Point(79, 10);
            this.l_code.Name = "l_code";
            this.l_code.Size = new System.Drawing.Size(218, 29);
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular);
            this.label9.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label9.Location = new System.Drawing.Point(3, 234);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(96, 35);
            this.label9.Text = "Кол-во:";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.label5.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label5.Location = new System.Drawing.Point(3, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(177, 30);
            this.label5.Text = "Наименование:";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular);
            this.label4.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label4.Location = new System.Drawing.Point(6, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 38);
            this.label4.Text = "Код:";
            // 
            // tb_count
            // 
            this.tb_count.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.tb_count.Location = new System.Drawing.Point(200, 234);
            this.tb_count.Name = "tb_count";
            this.tb_count.Size = new System.Drawing.Size(96, 39);
            this.tb_count.TabIndex = 4;
            // 
            // tb_itemId
            // 
            this.tb_itemId.Location = new System.Drawing.Point(0, 0);
            this.tb_itemId.Name = "tb_itemId";
            this.tb_itemId.Size = new System.Drawing.Size(100, 23);
            this.tb_itemId.TabIndex = 0;
            // 
            // panelWait
            // 
            this.panelWait.Controls.Add(this.label2);
            this.panelWait.Location = new System.Drawing.Point(659, 38);
            this.panelWait.Name = "panelWait";
            this.panelWait.Size = new System.Drawing.Size(327, 297);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(3, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(321, 78);
            this.label2.Text = "Обработка...";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tb_driver);
            this.panel2.Controls.Add(this.l_driver);
            this.panel2.Controls.Add(this.lCode);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.lName);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.lPlace);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(3, 352);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(327, 297);
            // 
            // tb_driver
            // 
            this.tb_driver.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.tb_driver.Location = new System.Drawing.Point(16, 133);
            this.tb_driver.Name = "tb_driver";
            this.tb_driver.Size = new System.Drawing.Size(294, 39);
            this.tb_driver.TabIndex = 13;
            this.tb_driver.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_driver_KeyDown);
            // 
            // l_driver
            // 
            this.l_driver.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.l_driver.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.l_driver.Location = new System.Drawing.Point(3, 112);
            this.l_driver.Name = "l_driver";
            this.l_driver.Size = new System.Drawing.Size(177, 29);
            this.l_driver.Text = "Водитель";
            // 
            // lCode
            // 
            this.lCode.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lCode.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.lCode.Location = new System.Drawing.Point(16, 196);
            this.lCode.Name = "lCode";
            this.lCode.Size = new System.Drawing.Size(294, 32);
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label10.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label10.Location = new System.Drawing.Point(3, 166);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(177, 30);
            this.label10.Text = "Код";
            // 
            // lName
            // 
            this.lName.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lName.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.lName.Location = new System.Drawing.Point(16, 39);
            this.lName.Name = "lName";
            this.lName.Size = new System.Drawing.Size(294, 73);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label7.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label7.Location = new System.Drawing.Point(3, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(177, 29);
            this.label7.Text = "Наименование";
            // 
            // lPlace
            // 
            this.lPlace.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lPlace.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.lPlace.Location = new System.Drawing.Point(16, 258);
            this.lPlace.Name = "lPlace";
            this.lPlace.Size = new System.Drawing.Size(294, 32);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Location = new System.Drawing.Point(3, 228);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(285, 30);
            this.label3.Text = "Место хранения/Адрес";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label8.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label8.Location = new System.Drawing.Point(16, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(294, 32);
            // 
            // f_cdp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1005, 691);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelWait);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "f_cdp";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panelWait.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_count;
        private System.Windows.Forms.TextBox tb_itemId;
        private System.Windows.Forms.Label l_name;
        private System.Windows.Forms.Label l_code;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label l_count_doc;
        private System.Windows.Forms.Panel panelWait;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lPlace;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lCode;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_Item;
        private System.Windows.Forms.Label l_driver;
        private System.Windows.Forms.TextBox tb_driver;

    }
}

