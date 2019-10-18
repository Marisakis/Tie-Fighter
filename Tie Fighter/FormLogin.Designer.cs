namespace Tie_Fighter
{
    partial class FormLogin
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
            this.LoginBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.userNameField = new System.Windows.Forms.TextBox();
            this.ipAddressField = new System.Windows.Forms.TextBox();
            this.serverPortField = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoginBtn
            // 
            this.LoginBtn.BackColor = System.Drawing.Color.Transparent;
            this.LoginBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.LoginBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LoginBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.LoginBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.LoginBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.LoginBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoginBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.11881F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginBtn.ForeColor = System.Drawing.Color.White;
            this.LoginBtn.Location = new System.Drawing.Point(0, 499);
            this.LoginBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.LoginBtn.Name = "LoginBtn";
            this.LoginBtn.Size = new System.Drawing.Size(1067, 55);
            this.LoginBtn.TabIndex = 3;
            this.LoginBtn.Text = "L o g i n";
            this.LoginBtn.UseVisualStyleBackColor = false;
            this.LoginBtn.Click += new System.EventHandler(this.LoginBtn_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.LoginBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1067, 554);
            this.panel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox1.Controls.Add(this.userNameField);
            this.groupBox1.Controls.Add(this.ipAddressField);
            this.groupBox1.Controls.Add(this.serverPortField);
            this.groupBox1.Cursor = System.Windows.Forms.Cursors.No;
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(400, 0, 400, 0);
            this.groupBox1.Size = new System.Drawing.Size(1067, 499);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // userNameField
            // 
            this.userNameField.AcceptsTab = true;
            this.userNameField.BackColor = System.Drawing.SystemColors.Info;
            this.userNameField.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.userNameField.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.267326F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userNameField.ForeColor = System.Drawing.Color.Black;
            this.userNameField.Location = new System.Drawing.Point(400, 424);
            this.userNameField.Margin = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.userNameField.Name = "userNameField";
            this.userNameField.Size = new System.Drawing.Size(267, 25);
            this.userNameField.TabIndex = 0;
            this.userNameField.Text = "Seb";
            this.userNameField.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ipAddressField
            // 
            this.ipAddressField.AcceptsTab = true;
            this.ipAddressField.BackColor = System.Drawing.SystemColors.Info;
            this.ipAddressField.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ipAddressField.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.267326F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ipAddressField.ForeColor = System.Drawing.Color.Black;
            this.ipAddressField.Location = new System.Drawing.Point(400, 449);
            this.ipAddressField.Margin = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.ipAddressField.Name = "ipAddressField";
            this.ipAddressField.Size = new System.Drawing.Size(267, 25);
            this.ipAddressField.TabIndex = 1;
            this.ipAddressField.Text = "localhost";
            this.ipAddressField.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // serverPortField
            // 
            this.serverPortField.AcceptsTab = true;
            this.serverPortField.BackColor = System.Drawing.SystemColors.Info;
            this.serverPortField.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.serverPortField.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.267326F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serverPortField.ForeColor = System.Drawing.Color.Black;
            this.serverPortField.Location = new System.Drawing.Point(400, 474);
            this.serverPortField.Margin = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.serverPortField.Name = "serverPortField";
            this.serverPortField.Size = new System.Drawing.Size(267, 25);
            this.serverPortField.TabIndex = 2;
            this.serverPortField.Text = "1717";
            this.serverPortField.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImage = global::Tie_Fighter.Properties.Resources.splashscreen;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLogin";
            this.Text = "Tie Fighter Shooter login";
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button LoginBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox userNameField;
        private System.Windows.Forms.TextBox ipAddressField;
        private System.Windows.Forms.TextBox serverPortField;
    }
}