using System;
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
        public ObjectManager objectManager;

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
            //System.Diagnostics.Debug.WriteLine("Painting window");
            base.OnPaint(e);
            /*Brush brush = new SolidBrush(Color.Red);
            Rectangle rect = new Rectangle(50, 50, 100, 100);
            e.Graphics.FillRectangle(brush, rect);*/
            objectManager.PaintObjects(e.Graphics);
            
            

        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            //base.Size = new Size(base.Size.Width, base.Size.Width / 3); //allows for fixing of window proportions
            if (objectManager != null)
            objectManager.onResize(this, e);
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

