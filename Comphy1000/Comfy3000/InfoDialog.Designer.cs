namespace Comfy3000
{
    partial class InfoDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.l_itemId = new System.Windows.Forms.Label();
            this.l_itemIdText = new System.Windows.Forms.Label();
            this.l_itemName = new System.Windows.Forms.Label();
            this.l_itemNameText = new System.Windows.Forms.Label();
            this.l_driver = new System.Windows.Forms.Label();
            this.l_driverText = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // l_itemId
            // 
            this.l_itemId.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.l_itemId.Location = new System.Drawing.Point(11, 28);
            this.l_itemId.Name = "l_itemId";
            this.l_itemId.Size = new System.Drawing.Size(100, 20);
            this.l_itemId.Text = "Товар";
            // 
            // l_itemIdText
            // 
            this.l_itemIdText.BackColor = System.Drawing.Color.White;
            this.l_itemIdText.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.l_itemIdText.Location = new System.Drawing.Point(45, 53);
            this.l_itemIdText.Name = "l_itemIdText";
            this.l_itemIdText.Size = new System.Drawing.Size(208, 20);
            this.l_itemIdText.Text = "text";
            // 
            // l_itemName
            // 
            this.l_itemName.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.l_itemName.Location = new System.Drawing.Point(11, 74);
            this.l_itemName.Name = "l_itemName";
            this.l_itemName.Size = new System.Drawing.Size(100, 20);
            this.l_itemName.Text = "Название";
            // 
            // l_itemNameText
            // 
            this.l_itemNameText.BackColor = System.Drawing.Color.White;
            this.l_itemNameText.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.l_itemNameText.Location = new System.Drawing.Point(45, 97);
            this.l_itemNameText.Name = "l_itemNameText";
            this.l_itemNameText.Size = new System.Drawing.Size(208, 39);
            this.l_itemNameText.Text = "text";
            // 
            // l_driver
            // 
            this.l_driver.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.l_driver.Location = new System.Drawing.Point(11, 139);
            this.l_driver.Name = "l_driver";
            this.l_driver.Size = new System.Drawing.Size(100, 20);
            this.l_driver.Text = "Водитель";
            // 
            // l_driverText
            // 
            this.l_driverText.BackColor = System.Drawing.Color.White;
            this.l_driverText.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Regular);
            this.l_driverText.Location = new System.Drawing.Point(45, 159);
            this.l_driverText.Name = "l_driverText";
            this.l_driverText.Size = new System.Drawing.Size(208, 39);
            this.l_driverText.Text = "text";
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(85, 201);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(117, 37);
            this.buttonClose.TabIndex = 10;
            this.buttonClose.Text = "ОК";
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // InfoDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(298, 247);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.l_driverText);
            this.Controls.Add(this.l_driver);
            this.Controls.Add(this.l_itemNameText);
            this.Controls.Add(this.l_itemName);
            this.Controls.Add(this.l_itemIdText);
            this.Controls.Add(this.l_itemId);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Location = new System.Drawing.Point(10, 20);
            this.Menu = this.mainMenu1;
            this.Name = "InfoDialog";
            this.Text = "Информация";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InfoDialog_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label l_itemId;
        public System.Windows.Forms.Label l_itemIdText;
        private System.Windows.Forms.Label l_itemName;
        public System.Windows.Forms.Label l_itemNameText;
        private System.Windows.Forms.Label l_driver;
        public System.Windows.Forms.Label l_driverText;
        private System.Windows.Forms.Button buttonClose;
    }
}