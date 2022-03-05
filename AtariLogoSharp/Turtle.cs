/* Atari Logo Sharp - implementation of Atari Logo in C#. Developed by 404 team. */

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

namespace AtariLogoSharp
{
    public class Turtle : IDisposable
    {
        /// <summary>
        /// Holds an artwork of the turtle (from the Resources), which can be scaled and painted.
        /// </summary>
        private Bitmap Art;

        /// <summary>
        /// Pens available to the turtle, should be 3. 
        /// </summary>
        public List<Pen> Pens { get; set; }

        /// <summary>
        /// Represents the currently active pen (its index) of the turtle.
        /// </summary>
        public int ActivePenIndex { get; set; } = 0;

        /// <summary>
        /// Current point of the turtle on the surface.
        /// </summary>
        public PointF CurrentPoint { get; set; }

        /// <summary>
        /// Indicates whether pen is down (can be drawn with).
        /// </summary>
        public bool PenIsDown { get; set; } = true;

        /// <summary>
        /// Indicates whether turtle is hidden (is not visible).
        /// </summary>
        public bool IsHidden { get; set; } = false;

        /// <summary>
        /// Default, start angle of the turtle.
        /// </summary>
        public static readonly int defaultAngle = 90;

        /// <summary>
        /// Turtle angle in relation to the horizontal.
        /// </summary>
        public int Angle { get; set; } = defaultAngle;

        /// <summary>
        /// Indicates how much the turtle graphic should be resized. This scale is just a programmer preference at this point.
        /// </summary>
        public static readonly int Scale = 2;

        /// <summary>
        /// Turtle object constructor. Creates an object and sets the current point of the turtle to the provided one.
        /// </summary>
        /// <param name="initialPoint">The initial point of the turtle.</param>
        public Turtle(PointF initialPoint)
        {
            Art = ScaleImage(Properties.Resources.TurtleArt, Scale);
            CurrentPoint = initialPoint;
            Pens = new List<Pen>()
            {
                new Pen(Color.Black, 1),
                new Pen(Color.Black, 1),
                new Pen(Color.Black, 1),
            };
        }

        /// <summary>
        /// Turtle object constructor. Creates an object and sets it state to the provided one.
        /// </summary>
        /// <param name="sourceTurtle">Turtle object to be copied from.</param>
        public Turtle(Turtle sourceTurtle)
        {
            Pens = new List<Pen>();
            CopyFrom(sourceTurtle);
        }

        /// <summary>
        /// Disposes object.
        /// </summary>
        public void Dispose()
        {
            if (Art != null) Art.Dispose();
            foreach (Pen singlePen in Pens)
                if (singlePen != null) singlePen.Dispose();
        }

        /// <summary>
        /// Sets the current state to the provided one.
        /// </summary>
        /// <param name="sourceTurtle">Turtle object to be copied from.</param>
        public void CopyFrom(Turtle sourceTurtle)
        {
            Art = (Bitmap)sourceTurtle.Art.Clone();

            foreach (Pen singlePen in Pens)
                if (singlePen != null) singlePen.Dispose();

            for (int i = 0; i < sourceTurtle.Pens.Count; i++)
                Pens.Add((Pen)sourceTurtle.Pens[i].Clone());

            ActivePenIndex = sourceTurtle.ActivePenIndex;
            CurrentPoint = sourceTurtle.CurrentPoint;
            PenIsDown = sourceTurtle.PenIsDown;
            IsHidden = sourceTurtle.IsHidden;
            Angle = sourceTurtle.Angle;
        }

        /// <summary>
        /// Scales image.
        /// </summary>
        /// <param name="orgImage">Bitmap to be scaled.</param>
        /// <param name="scale">Scale to applied.</param>
        /// <returns>A bitmap of scaled image.</returns>
        private Bitmap ScaleImage(Bitmap orgImage, int scale)
        {
            int scaledWidth = orgImage.Width * scale;
            int scaledHeight = orgImage.Height * scale;
            return new Bitmap(orgImage, scaledWidth, scaledHeight);
        }

        /// <summary>
        /// Rotates square Turtle image without cropping.
        /// </summary>
        /// <param name="angle">Angle of rotation.</param>
        /// <returns>A bitmap of rotated image.</returns>
        public Bitmap RotateSquareTurtleImage(int angle)
        {
            Bitmap rotatedImage = new Bitmap(this.Art.Width * 2, this.Art.Height * 2); // The values are doubled to avoid image cropping while rotating.
            using (Graphics surface = Graphics.FromImage(rotatedImage))
            {
                surface.TranslateTransform((float)(Art.Width), (float)(Art.Height)); // Because center formula can be simplified: orgBitmap.Width*2/2 = orgBitmap.Width
                surface.RotateTransform(angle);
                surface.TranslateTransform(-(float)(Art.Width), -(float)(Art.Height));
                surface.InterpolationMode = InterpolationMode.HighQualityBicubic; // To keep "good" quality of image.
                float cordyline = Art.Width / 2;
                PointF leftCorner = new PointF(cordyline, cordyline); // This point centers the image on the enlarged bitmap.
                surface.DrawImage(Art, leftCorner);
            }
            return rotatedImage;
        }

        /// <summary>
        /// Paint the non-transparent pixels of the turtle art to the specified color.
        /// </summary>
        /// <param name="targetColor">New color of a turtle.</param>
        public void PaintImage(Color targetColor)
        {
            for (int x = 0; x < Art.Width; x++)
                for (int y = 0; y < Art.Height; y++)
                    if (Art.GetPixel(x, y).A == 255)
                        Art.SetPixel(x, y, targetColor);
        }

        /// <summary>
        /// Rotates the turtle by the given angle.
        /// </summary>
        /// <param name="angle">Angle of rotation.</param>
        public void RotateTurtle(int angle)
        {
            Angle = Interpreter.Mod(Angle + angle);
        }
    }
}
