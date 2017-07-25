namespace Comfy3000
{
    partial class f_recalcItemLookup
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
            this.descLabel = new System.Windows.Forms.Label();
            this.codeLabel = new System.Windows.Forms.Label();
            this.codeTextBox = new System.Windows.Forms.TextBox();
            this.findButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.codeTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.nameTextBoxColumn = new System.Windows.Forms.DataGridTextBoxColumn();
            this.placeTextBoxColumn = new System.Windows.Forms.DataGridTextBoxColumn();
            this.countTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.editButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.closePanelButton = new System.Windows.Forms.Button();
            this.panelCountTextBox = new System.Windows.Forms.TextBox();
            this.panelButton = new System.Windows.Forms.Button();
            this.descPanelLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // descLabel
            // 
            this.descLabel.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.descLabel.Location = new System.Drawing.Point(44, 2);
            this.descLabel.Name = "descLabel";
            this.descLabel.Size = new System.Drawing.Size(225, 26);
            this.descLabel.Text = "Поиск номенклатуры";
            // 
            // codeLabel
            // 
            this.codeLabel.Location = new System.Drawing.Point(21, 26);
            this.codeLabel.Name = "codeLabel";
            this.codeLabel.Size = new System.Drawing.Size(264, 20);
            this.codeLabel.Text = "Введите код товара, ШК или название";
            // 
            // codeTextBox
            // 
            this.codeTextBox.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.codeTextBox.Location = new System.Drawing.Point(27, 42);
            this.codeTextBox.Name = "codeTextBox";
            this.codeTextBox.Size = new System.Drawing.Size(251, 29);
            this.codeTextBox.TabIndex = 3;
            this.codeTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.codeTextBox_KeyDown);
            // 
            // findButton
            // 
            this.findButton.Location = new System.Drawing.Point(32, 81);
            this.findButton.Name = "findButton";
            this.findButton.Size = new System.Drawing.Size(99, 42);
            this.findButton.TabIndex = 5;
            this.findButton.Text = "Поиск";
            this.findButton.Click += new System.EventHandler(this.findButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(104, 272);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(99, 32);
            this.cancelButton.TabIndex = 12;
            this.cancelButton.Text = "Закрыть";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.dataGrid1.Location = new System.Drawing.Point(3, 135);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.RowHeadersVisible = false;
            this.dataGrid1.Size = new System.Drawing.Size(314, 130);
            this.dataGrid1.TabIndex = 20;
            this.dataGrid1.TableStyles.Add(this.dataGridTableStyle1);
            this.dataGrid1.CurrentCellChanged += new System.EventHandler(this.dataGrid1_CurrentCellChanged);
            this.dataGrid1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGrid1_KeyDown);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.codeTextBoxColumn1);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.nameTextBoxColumn);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.placeTextBoxColumn);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.countTextBoxColumn1);
            this.dataGridTableStyle1.MappingName = "gridTable";
            // 
            // codeTextBoxColumn1
            // 
            this.codeTextBoxColumn1.Format = "";
            this.codeTextBoxColumn1.FormatInfo = null;
            this.codeTextBoxColumn1.HeaderText = "Код";
            this.codeTextBoxColumn1.MappingName = "good_code";
            this.codeTextBoxColumn1.Width = 70;
            // 
            // nameTextBoxColumn
            // 
            this.nameTextBoxColumn.Format = "";
            this.nameTextBoxColumn.FormatInfo = null;
            this.nameTextBoxColumn.HeaderText = "Наименование";
            this.nameTextBoxColumn.MappingName = "name";
            this.nameTextBoxColumn.Width = 200;
            // 
            // placeTextBoxColumn
            // 
            this.placeTextBoxColumn.Format = "";
            this.placeTextBoxColumn.FormatInfo = null;
            this.placeTextBoxColumn.HeaderText = "КУ";
            this.placeTextBoxColumn.MappingName = "place";
            // 
            // countTextBoxColumn1
            // 
            this.countTextBoxColumn1.Format = "";
            this.countTextBoxColumn1.FormatInfo = null;
            this.countTextBoxColumn1.HeaderText = "КФ";
            this.countTextBoxColumn1.MappingName = "count";
            // 
            // editButton
            // 
            this.editButton.Enabled = false;
            this.editButton.Location = new System.Drawing.Point(172, 81);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(99, 42);
            this.editButton.TabIndex = 21;
            this.editButton.Text = "Изменить";
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Brown;
            this.panel1.Controls.Add(this.closePanelButton);
            this.panel1.Controls.Add(this.panelCountTextBox);
            this.panel1.Controls.Add(this.panelButton);
            this.panel1.Controls.Add(this.descPanelLabel);
            this.panel1.Location = new System.Drawing.Point(54, 117);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(212, 100);
            this.panel1.Visible = false;
            // 
            // closePanelButton
            // 
            this.closePanelButton.Location = new System.Drawing.Point(106, 65);
            this.closePanelButton.Name = "closePanelButton";
            this.closePanelButton.Size = new System.Drawing.Size(88, 31);
            this.closePanelButton.TabIndex = 4;
            this.closePanelButton.Text = "Убрать";
            this.closePanelButton.Click += new System.EventHandler(this.closePanelButton_Click);
            // 
            // panelCountTextBox
            // 
            this.panelCountTextBox.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.panelCountTextBox.Location = new System.Drawing.Point(32, 30);
            this.panelCountTextBox.Name = "panelCountTextBox";
            this.panelCountTextBox.Size = new System.Drawing.Size(143, 29);
            this.panelCountTextBox.TabIndex = 3;
            this.panelCountTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.panelCountTextBox_KeyDown);
            // 
            // panelButton
            // 
            this.panelButton.Location = new System.Drawing.Point(12, 65);
            this.panelButton.Name = "panelButton";
            this.panelButton.Size = new System.Drawing.Size(88, 31);
            this.panelButton.TabIndex = 2;
            this.panelButton.Text = "Изменить";
            this.panelButton.Click += new System.EventHandler(this.panelButton_Click);
            // 
            // descPanelLabel
            // 
            this.descPanelLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.descPanelLabel.Location = new System.Drawing.Point(12, 11);
            this.descPanelLabel.Name = "descPanelLabel";
            this.descPanelLabel.Size = new System.Drawing.Size(192, 20);
            this.descPanelLabel.Text = "Изменить количество";
            // 
            // f_recalcItemLookup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(320, 320);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.findButton);
            this.Controls.Add(this.codeTextBox);
            this.Controls.Add(this.codeLabel);
            this.Controls.Add(this.descLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "f_recalcItemLookup";
            this.Text = "Compfy";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.f_recalcItemLookup_KeyDown);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label descLabel;
        private System.Windows.Forms.Label codeLabel;
        private System.Windows.Forms.TextBox codeTextBox;
        private System.Windows.Forms.Button findButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.DataGridTextBoxColumn codeTextBoxColumn1;
        private System.Windows.Forms.DataGridTextBoxColumn nameTextBoxColumn;
        private System.Windows.Forms.DataGridTextBoxColumn placeTextBoxColumn;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label descPanelLabel;
        private System.Windows.Forms.TextBox panelCountTextBox;
        private System.Windows.Forms.Button panelButton;
        private System.Windows.Forms.Button closePanelButton;
        private System.Windows.Forms.DataGridTextBoxColumn countTextBoxColumn1;

    }
}

