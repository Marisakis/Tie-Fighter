namespace Tie_Fighter
{
    partial class FormGamePictureBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGamePictureBox));
            this.SuspendLayout();
            // 
            // FormGamePictureBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::Tie_Fighter.Properties.Resources.ShooterBG2;
            resources.ApplyResources(this, "$this");
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.DoubleBuffered = true;
            this.Name = "FormGamePictureBox";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.FormGamePictureBox_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormGamePictureBox_KeyDown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FormGamePictureBox_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormGamePictureBox_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion
    }
}