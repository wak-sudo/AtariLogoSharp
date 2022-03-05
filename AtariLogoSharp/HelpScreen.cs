/* Atari Logo Sharp - implementation of Atari Logo in C#. Developed by 404 team. */

using System.Drawing;
using System.Windows.Forms;

namespace AtariLogoSharp
{
    public partial class HelpScreen : Form
    {
        public HelpScreen()
        {
            InitializeComponent();

            // Set window position:
            this.TopMost = true;
            this.StartPosition = FormStartPosition.Manual;
            const int padding = 40;
            int x = Screen.PrimaryScreen.WorkingArea.Left;
            int y = Screen.PrimaryScreen.WorkingArea.Top + padding;
            this.Location = new Point(x, y);

            // Load user manual.
            txtHelpInfo.Text = Properties.Resources.AtariLogoUserManualEng;

            this.Width = InputScreen.AdjustWidthToText(Width, txtHelpInfo, panelHelp.Width);
        }
    }
}
