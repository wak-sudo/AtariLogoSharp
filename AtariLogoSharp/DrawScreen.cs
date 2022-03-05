/* Atari Logo Sharp - implementation of Atari Logo in C#. Developed by 404 team. */

using System.Windows.Forms;
using System.Drawing;

namespace AtariLogoSharp
{
    public partial class DrawScreen : Form
    {
        public DrawSet DrawUniverse { get; set; }

        private readonly Form parentForm = null;

        public DrawScreen()
        {
            Construct();
        }

        public DrawScreen(Form parentForm)
        {
            this.parentForm = parentForm;
            Construct();
        }

        private void Construct()
        {
            InitializeComponent();

            // Set window full-screen:
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            picDrawArea.Dock = DockStyle.Fill;
            this.TopMost = true;

            // Needed for implementing keyboard shortcuts.
            this.KeyPreview = true;

            DrawUniverse = new DrawSet();
        }

        private void DrawScreen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                parentForm.BringToFront();
        }

        public void UpdateImage()
        {
            Image oldImage = picDrawArea.Image;
            picDrawArea.Image = DrawUniverse.GetImage();
            if (oldImage != null) oldImage.Dispose();
        }

        public Image GetImageReference()
        {
            return picDrawArea.Image;
        }

        public void UpdateUniverse(DrawSet newDrawSet)
        {
            DrawUniverse.CopyFrom(newDrawSet);
        }

        private void DrawScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Protects the program from potential exceptions (Show() method when resources are free).
            this.Hide();
            e.Cancel = true;
        }

        private void DrawScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (DrawUniverse != null) DrawUniverse.Dispose();
        }
    }
}
