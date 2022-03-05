/* Atari Logo Sharp - implementation of Atari Logo in C#. Developed by 404 team. */

namespace AtariLogoSharp
{
    partial class InputScreen
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
            this.txtLogoCodeInput = new System.Windows.Forms.TextBox();
            this.btnAcceptInput = new System.Windows.Forms.Button();
            this.txtHistoryLog = new System.Windows.Forms.TextBox();
            this.lblNotification = new System.Windows.Forms.Label();
            this.chkUseEnter = new System.Windows.Forms.CheckBox();
            this.tableLayoutButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnClearLogoCode = new System.Windows.Forms.Button();
            this.chkClearAfterSuccess = new System.Windows.Forms.CheckBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.appToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearDrawScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showHideDrawScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.codeInputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearInputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acceptInputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImageDialog = new System.Windows.Forms.SaveFileDialog();
            this.tableLayoutButtons.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtLogoCodeInput
            // 
            this.txtLogoCodeInput.Location = new System.Drawing.Point(12, 73);
            this.txtLogoCodeInput.Multiline = true;
            this.txtLogoCodeInput.Name = "txtLogoCodeInput";
            this.txtLogoCodeInput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLogoCodeInput.Size = new System.Drawing.Size(325, 62);
            this.txtLogoCodeInput.TabIndex = 0;
            this.txtLogoCodeInput.Text = "Type your code here...";
            this.txtLogoCodeInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtLogoCode_KeyDown);
            // 
            // btnAcceptInput
            // 
            this.btnAcceptInput.Location = new System.Drawing.Point(221, 3);
            this.btnAcceptInput.Name = "btnAcceptInput";
            this.btnAcceptInput.Size = new System.Drawing.Size(101, 22);
            this.btnAcceptInput.TabIndex = 2;
            this.btnAcceptInput.Text = "Accept";
            this.btnAcceptInput.UseVisualStyleBackColor = true;
            this.btnAcceptInput.Click += new System.EventHandler(this.BtnAcceptInput_Click);
            // 
            // txtHistoryLog
            // 
            this.txtHistoryLog.BackColor = System.Drawing.SystemColors.InfoText;
            this.txtHistoryLog.ForeColor = System.Drawing.SystemColors.Info;
            this.txtHistoryLog.Location = new System.Drawing.Point(343, 73);
            this.txtHistoryLog.Multiline = true;
            this.txtHistoryLog.Name = "txtHistoryLog";
            this.txtHistoryLog.ReadOnly = true;
            this.txtHistoryLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtHistoryLog.Size = new System.Drawing.Size(107, 99);
            this.txtHistoryLog.TabIndex = 3;
            // 
            // lblNotification
            // 
            this.lblNotification.AutoSize = true;
            this.lblNotification.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblNotification.Location = new System.Drawing.Point(13, 29);
            this.lblNotification.Name = "lblNotification";
            this.lblNotification.Size = new System.Drawing.Size(69, 16);
            this.lblNotification.TabIndex = 4;
            this.lblNotification.Text = "Welcome.";
            // 
            // chkUseEnter
            // 
            this.chkUseEnter.AutoSize = true;
            this.chkUseEnter.Checked = true;
            this.chkUseEnter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseEnter.Location = new System.Drawing.Point(16, 50);
            this.chkUseEnter.Name = "chkUseEnter";
            this.chkUseEnter.Size = new System.Drawing.Size(73, 17);
            this.chkUseEnter.TabIndex = 5;
            this.chkUseEnter.Text = "Use Enter";
            this.chkUseEnter.UseVisualStyleBackColor = true;
            // 
            // tableLayoutButtons
            // 
            this.tableLayoutButtons.ColumnCount = 3;
            this.tableLayoutButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
            this.tableLayoutButtons.Controls.Add(this.btnHelp, 1, 0);
            this.tableLayoutButtons.Controls.Add(this.btnClearLogoCode, 0, 0);
            this.tableLayoutButtons.Controls.Add(this.btnAcceptInput, 2, 0);
            this.tableLayoutButtons.Location = new System.Drawing.Point(12, 141);
            this.tableLayoutButtons.Name = "tableLayoutButtons";
            this.tableLayoutButtons.RowCount = 1;
            this.tableLayoutButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutButtons.Size = new System.Drawing.Size(325, 31);
            this.tableLayoutButtons.TabIndex = 6;
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(112, 3);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(101, 22);
            this.btnHelp.TabIndex = 6;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.BtnHelp_Click);
            // 
            // btnClearLogoCode
            // 
            this.btnClearLogoCode.Location = new System.Drawing.Point(3, 3);
            this.btnClearLogoCode.Name = "btnClearLogoCode";
            this.btnClearLogoCode.Size = new System.Drawing.Size(103, 22);
            this.btnClearLogoCode.TabIndex = 3;
            this.btnClearLogoCode.Text = "Clear input";
            this.btnClearLogoCode.UseVisualStyleBackColor = true;
            this.btnClearLogoCode.Click += new System.EventHandler(this.BtnClearLogoCode_Click);
            // 
            // chkClearAfterSuccess
            // 
            this.chkClearAfterSuccess.AutoSize = true;
            this.chkClearAfterSuccess.Checked = true;
            this.chkClearAfterSuccess.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkClearAfterSuccess.Location = new System.Drawing.Point(96, 50);
            this.chkClearAfterSuccess.Name = "chkClearAfterSuccess";
            this.chkClearAfterSuccess.Size = new System.Drawing.Size(116, 17);
            this.chkClearAfterSuccess.TabIndex = 7;
            this.chkClearAfterSuccess.Text = "Clear after success";
            this.chkClearAfterSuccess.UseVisualStyleBackColor = true;
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.appToolStripMenuItem,
            this.drawScreenToolStripMenuItem,
            this.codeInputToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(454, 24);
            this.menuStrip.TabIndex = 8;
            this.menuStrip.Text = "menuStrip1";
            // 
            // appToolStripMenuItem
            // 
            this.appToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.appToolStripMenuItem.Name = "appToolStripMenuItem";
            this.appToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.appToolStripMenuItem.Text = "App";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // drawScreenToolStripMenuItem
            // 
            this.drawScreenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToFileToolStripMenuItem,
            this.clearDrawScreenToolStripMenuItem,
            this.showHideDrawScreenToolStripMenuItem});
            this.drawScreenToolStripMenuItem.Name = "drawScreenToolStripMenuItem";
            this.drawScreenToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.drawScreenToolStripMenuItem.Text = "Draw screen";
            // 
            // saveToFileToolStripMenuItem
            // 
            this.saveToFileToolStripMenuItem.Name = "saveToFileToolStripMenuItem";
            this.saveToFileToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.saveToFileToolStripMenuItem.Text = "Save to file";
            this.saveToFileToolStripMenuItem.Click += new System.EventHandler(this.SaveToFileToolStripMenuItem_Click);
            // 
            // clearDrawScreenToolStripMenuItem
            // 
            this.clearDrawScreenToolStripMenuItem.Name = "clearDrawScreenToolStripMenuItem";
            this.clearDrawScreenToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.clearDrawScreenToolStripMenuItem.Text = "Clear";
            this.clearDrawScreenToolStripMenuItem.Click += new System.EventHandler(this.ClearDrawScreenToolStripMenuItem_Click);
            // 
            // showHideDrawScreenToolStripMenuItem
            // 
            this.showHideDrawScreenToolStripMenuItem.Name = "showHideDrawScreenToolStripMenuItem";
            this.showHideDrawScreenToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.showHideDrawScreenToolStripMenuItem.Text = "Show/Hide";
            this.showHideDrawScreenToolStripMenuItem.Click += new System.EventHandler(this.ShowHideDrawScreenToolStripMenuItem_Click);
            // 
            // codeInputToolStripMenuItem
            // 
            this.codeInputToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearInputToolStripMenuItem,
            this.acceptInputToolStripMenuItem});
            this.codeInputToolStripMenuItem.Name = "codeInputToolStripMenuItem";
            this.codeInputToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.codeInputToolStripMenuItem.Text = "Code input";
            // 
            // clearInputToolStripMenuItem
            // 
            this.clearInputToolStripMenuItem.Name = "clearInputToolStripMenuItem";
            this.clearInputToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.clearInputToolStripMenuItem.Text = "Clear";
            this.clearInputToolStripMenuItem.Click += new System.EventHandler(this.ClearInputToolStripMenuItem_Click);
            // 
            // acceptInputToolStripMenuItem
            // 
            this.acceptInputToolStripMenuItem.Name = "acceptInputToolStripMenuItem";
            this.acceptInputToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.acceptInputToolStripMenuItem.Text = "Accept";
            this.acceptInputToolStripMenuItem.Click += new System.EventHandler(this.AcceptInputToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manualToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // manualToolStripMenuItem
            // 
            this.manualToolStripMenuItem.Name = "manualToolStripMenuItem";
            this.manualToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.manualToolStripMenuItem.Text = "Manual";
            this.manualToolStripMenuItem.Click += new System.EventHandler(this.ManualToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // InputScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 179);
            this.Controls.Add(this.chkClearAfterSuccess);
            this.Controls.Add(this.tableLayoutButtons);
            this.Controls.Add(this.chkUseEnter);
            this.Controls.Add(this.lblNotification);
            this.Controls.Add(this.txtHistoryLog);
            this.Controls.Add(this.txtLogoCodeInput);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "InputScreen";
            this.Text = "Input";
            this.Activated += new System.EventHandler(this.InputScreen_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.InputScreen_FormClosed);
            this.Resize += new System.EventHandler(this.InputScreen_Resize);
            this.tableLayoutButtons.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLogoCodeInput;
        private System.Windows.Forms.Button btnAcceptInput;
        private System.Windows.Forms.TextBox txtHistoryLog;
        private System.Windows.Forms.Label lblNotification;
        private System.Windows.Forms.CheckBox chkUseEnter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutButtons;
        private System.Windows.Forms.Button btnClearLogoCode;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.CheckBox chkClearAfterSuccess;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem appToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drawScreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearDrawScreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showHideDrawScreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem codeInputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearInputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem acceptInputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveImageDialog;
        private System.Windows.Forms.ToolStripMenuItem manualToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}