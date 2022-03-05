/* Atari Logo Sharp - implementation of Atari Logo in C#. Developed by 404 team. */

using System.Drawing;
using System;

namespace AtariLogoSharp
{
    /// <summary>
    /// This class stores a Graphics object and a connected Bitmap object.
    /// </summary>
    public class Layer : IDisposable
    {
        public Graphics Surface { get; set; }

        public Bitmap Image { get; set; }

        /// <summary>
        /// Constructor of the Layer. Creates a bitmap with provided width and height and connects a Graphics object to it.
        /// </summary>
        /// <param name="width">Width of the bitmap.</param>
        /// <param name="height">Height of the bitmap.</param>
        public Layer(int width, int height)
        {
            Image = new Bitmap(width, height);
            Surface = Graphics.FromImage(Image);
        }

        /// <summary>
        /// Disposes object.
        /// </summary>
        public void Dispose()
        {
            if (Surface != null) Surface.Dispose();
            if (Image != null) Image.Dispose();
        }
    }
}
