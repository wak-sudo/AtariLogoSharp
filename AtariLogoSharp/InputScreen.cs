/* Atari Logo Sharp - implementation of Atari Logo in C#. Developed by 404 team. */

using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace AtariLogoSharp
{
    public partial class InputScreen : Form
    {
        private readonly DrawScreen winDrawScreen;

        private readonly Interpreter interpreter;

        /// <summary>
        /// Tells whether the hint in the input box should be active.
        /// </summary>
        private bool hintIsActive = true;

        public InputScreen()
        {
            InitializeComponent();

            this.Icon = Properties.Resources.AtariLogoSharp;

            // Set window position:
            this.TopMost = true;
            this.StartPosition = FormStartPosition.Manual;

            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            int x = Screen.PrimaryScreen.WorkingArea.Left;
            int y = Screen.PrimaryScreen.WorkingArea.Bottom - this.Height;
            this.Location = new Point(x, y);

            // Needed for implementing keyboard shortcuts.
            this.KeyPreview = true;

            winDrawScreen = new DrawScreen(this);
            winDrawScreen.UpdateImage(); // To make the background white.
            winDrawScreen.Show();

            interpreter = new Interpreter(winDrawScreen.DrawUniverse);

            txtLogoCodeInput.Select(0, 0);
        }

        private void TxtLogoCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (hintIsActive)
            {
                txtLogoCodeInput.Clear();
                hintIsActive = false;
            }

            if (e.KeyCode == Keys.Enter && chkUseEnter.Checked == true && hintIsActive == false)
            {
                SendCode();
                e.SuppressKeyPress = true; // Do not pass the new line (enter) character to the textbox.
            }
        }

        /// <summary>
        /// Passes code to the interpreter. 
        /// </summary>
        private void SendCode()
        {
            if (hintIsActive == false)
            {
                IRC interpreterResult = interpreter.Interpret(txtLogoCodeInput.Text);
                if (interpreterResult == IRC.NO_ERROR)
                {
                    UpdateTheHistoryLog(Interpreter.CleanCode(txtLogoCodeInput.Text));
                    lblNotification.Text = Interpreter.ReturnCodesDictionary[interpreterResult];

                    // Update the draw screen.
                    winDrawScreen.UpdateUniverse(interpreter.InterpretedGraphic);
                    winDrawScreen.UpdateImage();

                    if (interpreter.POTSCommandInterpreted == true)
                    {
                        ShowProcedures();
                    }

                    if (chkClearAfterSuccess.Checked == true)
                        txtLogoCodeInput.Clear();

                    // Until there are no more procedures to be edited, fill the input text with the procedure codes.
                    if (interpreter.ProceduresToBeEdited.Count != 0)
                    {
                        txtLogoCodeInput.Clear();
                        string latestProcedureToEdit = interpreter.ProceduresToBeEdited[0];
                        lblNotification.Text = ("Editing: " + latestProcedureToEdit);

                        txtLogoCodeInput.Text = ("TO " + latestProcedureToEdit + " ");
                        foreach (string param in interpreter.ProcedureToParams[latestProcedureToEdit])
                            txtLogoCodeInput.AppendText(param + " ");

                        foreach (string codePart in interpreter.ProcedureToCode[latestProcedureToEdit])
                            txtLogoCodeInput.AppendText(codePart + " ");

                        txtLogoCodeInput.AppendText("END");

                        interpreter.ProceduresToBeEdited.RemoveAt(0);
                    }
                }
                else lblNotification.Text = String.Format(Interpreter.ReturnCodesDictionary[interpreterResult], interpreter.ErrorBreakCommand);
            }
        }

        /// <summary>
        /// Updates the history log by adding the provided code.
        /// </summary>
        /// <param name="commands">Commands to be added to the history log.</param>
        private void UpdateTheHistoryLog(List<string> commands)
        {
            if (commands.Count != 0)
            {
                int lastIndexInList = commands.Count - 1;

                for (int i = 0; i < lastIndexInList; i++)
                    txtHistoryLog.AppendText(commands[i] + ' ');
                txtHistoryLog.AppendText(commands[lastIndexInList] + Environment.NewLine + Environment.NewLine);
            }
        }

        private void ClearLogoInput()
        {
            txtLogoCodeInput.Clear();
        }

        private void ShowHelp()
        {
            HelpScreen winHelpScreen = new HelpScreen();
            winHelpScreen.Show();
        }

        private void ShowAbout()
        {
            AboutScreen winAbout = new AboutScreen();
            winAbout.Show();
        }

        private void ShowProcedures()
        {
            ProceduresScreen winProcedures = new ProceduresScreen();
            winProcedures.FillProceduresList(interpreter.ProcedureToParams);
            winProcedures.Show();
        }

        private void ClearInputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearLogoInput();
        }

        private void BtnClearLogoCode_Click(object sender, EventArgs e)
        {
            ClearLogoInput();
        }

        private void BtnShowDrawScreen_Click(object sender, EventArgs e)
        {
            winDrawScreen.Show();
            winDrawScreen.WindowState = FormWindowState.Maximized;
        }

        private void InputScreen_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                winDrawScreen.WindowState = FormWindowState.Minimized;
        }

        private void InputScreen_Activated(object sender, EventArgs e)
        {
            winDrawScreen.Show();
            winDrawScreen.WindowState = FormWindowState.Maximized;
        }

        private void BtnHelp_Click(object sender, EventArgs e)
        {
            ShowHelp();
        }

        private void InputScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (interpreter != null) interpreter.Dispose();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ClearDrawScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            interpreter.InterpretedGraphic.Clear();
            winDrawScreen.UpdateUniverse(interpreter.InterpretedGraphic);
            winDrawScreen.UpdateImage();
        }

        private void ShowHideDrawScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (winDrawScreen.Visible == false)
            {
                winDrawScreen.Show();
                winDrawScreen.WindowState = FormWindowState.Maximized;
                this.TopMost = true;
            }
            else winDrawScreen.Hide();
        }

        private void AcceptInputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendCode();
        }

        private void BtnAcceptInput_Click(object sender, EventArgs e)
        {
            SendCode();
        }

        private void SaveToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveImageDialog.Filter = "PNG image|*.png|Bitmap image|*.bmp";
            saveImageDialog.Title = "Save an Image File";
            saveImageDialog.FileName = "Screenshot";
            ImageFormat format = ImageFormat.Png;
            if (saveImageDialog.ShowDialog() == DialogResult.OK)
            {
                Image bmp = winDrawScreen.GetImageReference();
                string ext = Path.GetExtension(saveImageDialog.FileName).ToLower();
                switch (ext)
                {
                    case ".png":
                        format = ImageFormat.Png;
                        break;

                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;
                }
                bmp.Save(saveImageDialog.FileName, format);
            }
        }

        private void ManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHelp();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAbout();
        }

        // These functions are specific for Help and About form structures:

        /// <summary>
        /// Calculates required width to fill the whole text in the form. Used in Help and About forms.
        /// </summary>
        /// <param name="formWidth">Width of the form.</param>
        /// <param name="textBox">Used text box.</param>
        /// <param name="panelWidth">Used panel width.</param>
        /// <returns>Width needed to fill the whole text in the form.</returns>
        public static int AdjustWidthToText(int formWidth, RichTextBox textBox, int panelWidth)
        {
            Size size = TextRenderer.MeasureText(textBox.Text, textBox.Font);
            int widthWinMargin = formWidth - panelWidth;
            int hScrollMargin = SystemInformation.HorizontalScrollBarHeight;
            return (size.Width + widthWinMargin + hScrollMargin);
        }

        /// <summary>
        /// Calculates required height to fill the whole text in the form. Used in Help and About forms.
        /// </summary>
        /// <param name="formHeight">Height of the form.</param>
        /// <param name="textBox">Used text box.</param>
        /// <param name="panelHeight">Used panel height.</param>
        /// <returns>Height needed to fill the whole text in the form.</returns>
        public static int AdjustHeightToText(int formHeight, RichTextBox textBox, int panelHeight)
        {
            Size size = TextRenderer.MeasureText(textBox.Text, textBox.Font);
            int heightWinMargin = formHeight - panelHeight;
            int vScrollMargin = SystemInformation.VerticalScrollBarWidth;
            return (size.Height + heightWinMargin + vScrollMargin);
        }


    }
}
