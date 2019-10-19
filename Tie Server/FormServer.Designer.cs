namespace Tie_Server
{
    partial class FormServer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormServer));
            this.StartBtn = new System.Windows.Forms.Button();
            this.PortNumberField = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // StartBtn
            // 
            this.StartBtn.BackColor = System.Drawing.Color.Transparent;
            this.StartBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.StartBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.StartBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.StartBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.StartBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.StartBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.11881F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartBtn.ForeColor = System.Drawing.Color.White;
            this.StartBtn.Location = new System.Drawing.Point(0, 398);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(800, 52);
            this.StartBtn.TabIndex = 1;
            this.StartBtn.Text = "S t a r t";
            this.StartBtn.UseVisualStyleBackColor = false;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // PortNumberField
            // 
            this.PortNumberField.BackColor = System.Drawing.Color.Red;
            this.PortNumberField.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PortNumberField.Location = new System.Drawing.Point(350, 375);
            this.PortNumberField.Margin = new System.Windows.Forms.Padding(0);
            this.PortNumberField.Name = "PortNumberField";
            this.PortNumberField.Size = new System.Drawing.Size(100, 20);
            this.PortNumberField.TabIndex = 0;
            this.PortNumberField.Text = "Port number";
            this.PortNumberField.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox1.Controls.Add(this.PortNumberField);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(350, 3, 350, 3);
            this.groupBox1.Size = new System.Drawing.Size(800, 398);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // FormServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Tie_Server.Properties.Resources.ServerSplashScreen;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.StartBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormServer";
            this.Text = "Tie Fighter server";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.TextBox PortNumberField;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}