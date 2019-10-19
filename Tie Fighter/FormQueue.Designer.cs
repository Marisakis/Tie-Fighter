namespace Tie_Fighter
{
    partial class FormQueue
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormQueue));
            this.lobbyPlayersLabel = new System.Windows.Forms.Label();
            this.StartBtn = new System.Windows.Forms.Button();
            this.chatBox = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.highscoresbutton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lobbyPlayersLabel
            // 
            this.lobbyPlayersLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lobbyPlayersLabel.Location = new System.Drawing.Point(3, 16);
            this.lobbyPlayersLabel.Name = "lobbyPlayersLabel";
            this.lobbyPlayersLabel.Size = new System.Drawing.Size(194, 431);
            this.lobbyPlayersLabel.TabIndex = 0;
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
            this.StartBtn.Location = new System.Drawing.Point(3, 395);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(594, 52);
            this.StartBtn.TabIndex = 2;
            this.StartBtn.Text = "S t a r t";
            this.StartBtn.UseVisualStyleBackColor = false;
            this.StartBtn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.StartBtn_MouseClick);
            // 
            // chatBox
            // 
            this.chatBox.BackColor = System.Drawing.SystemColors.Info;
            this.chatBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.chatBox.Location = new System.Drawing.Point(3, 395);
            this.chatBox.Name = "chatBox";
            this.chatBox.Size = new System.Drawing.Size(194, 52);
            this.chatBox.TabIndex = 3;
            this.chatBox.Text = "Message";
            this.chatBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChatBox_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chatBox);
            this.groupBox1.Controls.Add(this.lobbyPlayersLabel);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Location = new System.Drawing.Point(600, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 450);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "L o b b y";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.highscoresbutton);
            this.groupBox2.Controls.Add(this.StartBtn);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(600, 450);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // highscoresbutton
            // 
            this.highscoresbutton.BackColor = System.Drawing.Color.Transparent;
            this.highscoresbutton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.highscoresbutton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.highscoresbutton.Dock = System.Windows.Forms.DockStyle.Top;
            this.highscoresbutton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.highscoresbutton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.highscoresbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.highscoresbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.11881F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.highscoresbutton.ForeColor = System.Drawing.Color.White;
            this.highscoresbutton.Location = new System.Drawing.Point(3, 16);
            this.highscoresbutton.Name = "highscoresbutton";
            this.highscoresbutton.Size = new System.Drawing.Size(594, 52);
            this.highscoresbutton.TabIndex = 2;
            this.highscoresbutton.Text = "G e t   H i g h s c o r e s";
            this.highscoresbutton.UseVisualStyleBackColor = false;
            this.highscoresbutton.Click += new System.EventHandler(this.Highscoresbutton_Click);
            // 
            // FormQueue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Tie_Fighter.Properties.Resources.tiefighterxwing;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormQueue";
            this.Text = "Tie Fighter Shooter lobby";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lobbyPlayersLabel;
        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.RichTextBox chatBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button highscoresbutton;
    }
}