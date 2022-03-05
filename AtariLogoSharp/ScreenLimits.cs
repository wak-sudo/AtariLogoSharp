/* Atari Logo Sharp - implementation of Atari Logo in C#. Developed by 404 team. */

using System.Windows.Forms;

namespace AtariLogoSharp
{
    /// <summary>
    /// This class holds limits for inner screen around padding.
    /// </summary>
    class ScreenLimits
    {
        public int Top { get; set; }

        public int Bottom { get; set; }

        public int Left { get; set; }

        public int Right { get; set; }

        /// <summary>
        /// Constructor of the ScreenLimits. Calculates the limits of the visible screen with the provided padding included.
        /// </summary>
        /// <param name="padding">Padding that is in use.</param>
        public ScreenLimits(int padding = 0)
        {
            Top = Left = padding;
            Bottom = Screen.PrimaryScreen.Bounds.Height + padding;
            Right = Screen.PrimaryScreen.Bounds.Width + padding;
        }
    }
}
