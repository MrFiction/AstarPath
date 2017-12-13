namespace PathFinder
{
    partial class Grid
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Grid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "Grid";
            this.Load += new System.EventHandler(this.Grid_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Grid_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Grid_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Grid_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
