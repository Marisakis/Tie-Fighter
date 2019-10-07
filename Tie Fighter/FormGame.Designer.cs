using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Tie_Fighter.GameObjects;

namespace Tie_Fighter
{
    partial class FormGame
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private ObjectManager objectManager;

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
        
        
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            /*Brush brush = new SolidBrush(Color.Red);
            Rectangle rect = new Rectangle(50, 50, 100, 100);
            e.Graphics.FillRectangle(brush, rect);*/
            foreach(GameObject<double> g in objectManager.gameObjects)
            {
                g.Draw(e.Graphics);
            }
            

        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::Tie_Fighter.Properties.Resources.ShooterBG;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(784, 460);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormGame";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormGame_KeyDown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FormGame_MouseClick);
            this.ResumeLayout(false);

        }

        #endregion
    }
}

