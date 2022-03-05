/* Atari Logo Sharp - implementation of Atari Logo in C#. Developed by 404 team. */

namespace AtariLogoSharp
{
    partial class DrawScreen
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
            this.picDrawArea = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picDrawArea)).BeginInit();
            this.SuspendLayout();
            // 
            // picDrawArea
            // 
            this.picDrawArea.Location = new System.Drawing.Point(12, 12);
            this.picDrawArea.Name = "picDrawArea";
            this.picDrawArea.Size = new System.Drawing.Size(145, 123);
            this.picDrawArea.TabIndex = 0;
            this.picDrawArea.TabStop = false;
            // 
            // DrawScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 222);
            this.Controls.Add(this.picDrawArea);
            this.Name = "DrawScreen";
            this.ShowIcon = false;
            this.Text = "Draw Screen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DrawScreen_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DrawScreen_FormClosed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DrawScreen_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.picDrawArea)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picDrawArea;
    }
}

