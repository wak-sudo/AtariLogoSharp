/* Atari Logo Sharp - implementation of Atari Logo in C#. Developed by 404 team. */

namespace AtariLogoSharp
{
    partial class HelpScreen
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
            this.txtHelpInfo = new System.Windows.Forms.RichTextBox();
            this.panelHelp = new System.Windows.Forms.Panel();
            this.panelHelp.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtHelpInfo
            // 
            this.txtHelpInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHelpInfo.Location = new System.Drawing.Point(0, 0);
            this.txtHelpInfo.Name = "txtHelpInfo";
            this.txtHelpInfo.ReadOnly = true;
            this.txtHelpInfo.Size = new System.Drawing.Size(268, 262);
            this.txtHelpInfo.TabIndex = 0;
            this.txtHelpInfo.Text = "";
            this.txtHelpInfo.WordWrap = false;
            // 
            // panelHelp
            // 
            this.panelHelp.Controls.Add(this.txtHelpInfo);
            this.panelHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHelp.Location = new System.Drawing.Point(0, 0);
            this.panelHelp.Name = "panelHelp";
            this.panelHelp.Size = new System.Drawing.Size(268, 262);
            this.panelHelp.TabIndex = 0;
            // 
            // HelpScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 262);
            this.Controls.Add(this.panelHelp);
            this.Name = "HelpScreen";
            this.ShowIcon = false;
            this.Text = "Help";
            this.panelHelp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtHelpInfo;
        private System.Windows.Forms.Panel panelHelp;
    }
}