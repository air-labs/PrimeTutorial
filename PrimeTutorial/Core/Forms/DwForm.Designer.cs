namespace PrimeTutorial.Core.Forms
{
    partial class DwForm
    {
        private const double scale = 25;

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
            this.SuspendLayout();
            // 
            // DwForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Name = "DwForm";
            this.Text = "DwForm";
            this.Load += new System.EventHandler(this.DwForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DwForm_Paint);
            this.Resize += new System.EventHandler(this.DwForm_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}