/* Atari Logo Sharp - implementation of Atari Logo in C#. Developed by 404 team. */

using System.Windows.Forms;

namespace AtariLogoSharp
{
    public partial class AboutScreen : Form
    {
        public AboutScreen()
        {
            InitializeComponent();
            richTextBox.SelectionAlignment = HorizontalAlignment.Center;
            richTextBox.Text = Properties.Resources.AboutInfo;          
            this.Width = InputScreen.AdjustWidthToText(Width, richTextBox, panel.Width);
            this.Height = InputScreen.AdjustHeightToText(Height, richTextBox, panel.Height);
            this.TopMost = true;
            CenterToScreen();
        }
    }
}
