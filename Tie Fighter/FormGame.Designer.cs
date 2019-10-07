using System.Drawing;

namespace Tie_Fighter
{
    partial class FormGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGame));
            this.SuspendLayout();
            // 
            // FormGame
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::Tie_Fighter.Properties.Resources.ShooterBG2;
            this.DoubleBuffered = true;
            this.Name = "FormGame";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormGame_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormGame_KeyDown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FormGame_MouseClick);
            this.ResumeLayout(false);

        }

        #endregion
    }
}

