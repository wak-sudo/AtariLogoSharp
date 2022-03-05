/* Atari Logo Sharp - implementation of Atari Logo in C#. Developed by 404 team. */

using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System;

namespace AtariLogoSharp
{
    /// <summary>
    /// A class consisting of the elements needed for drawing.
    /// </summary>
    public class DrawSet : IDisposable
    {
        /// <summary>
        /// Every line is drawn to this layer (no turtle drawn).
        /// </summary>
        private readonly Layer BaseLayer;

        /// <summary>
        /// Assigns a turtle to an ID.
        /// </summary>
        public Dictionary<int, Turtle> Turtles;

        /// <summary>
        /// Protects from potential overflows (e.x. turtle art drawing out of zone).
        /// </summary>
        private readonly float safeZonePadding;

        /// <summary>
        /// Holds limits for inner screen around padding (i.e. for visible drawing area). 
        /// </summary>
        private ScreenLimits bounds;

        public DrawSet()
        {
            // The safe zone should be twice the width of the turtle to always have room for it.
            safeZonePadding = Properties.Resources.TurtleArt.Width * Turtle.Scale * 2;
            bounds = new ScreenLimits((int)safeZonePadding);

            int widthWithSafeZone = Screen.PrimaryScreen.Bounds.Width + (int)safeZonePadding * 2;
            int heightWithSafeZone = Screen.PrimaryScreen.Bounds.Height + (int)safeZonePadding * 2;

            BaseLayer = new Layer(widthWithSafeZone, heightWithSafeZone);
            BaseLayer.Surface.Clear(Color.White);

            Turtles = new Dictionary<int, Turtle>
            {
                { 0, new Turtle(GetCenterOfTheScreen()) }
            };
        }

        /// <summary>
        /// Disposes object.
        /// </summary>
        public void Dispose()
        {
            if (BaseLayer != null) BaseLayer.Dispose();
            foreach (Turtle turtle in Turtles.Values)
                if (turtle != null) turtle.Dispose();
        }

        /// <summary>
        /// Clears draw set; restores the initial state.
        /// </summary>
        public void Clear()
        {
            BaseLayer.Surface.Clear(Color.White);
            ClearTurtles();
            Turtles.Add(0, new Turtle(GetCenterOfTheScreen()));
        }

        /// <summary>
        /// Sets the current state to the provided one.
        /// </summary>
        /// <param name="sourceSet">DrawSet to be copied from.</param>
        public void CopyFrom(DrawSet sourceSet)
        {
            Bitmap sourceBitmap = sourceSet.BaseLayer.Image;
            BaseLayer.Surface.DrawImage(sourceBitmap, new Point(0, 0));

            ClearTurtles();
            List<int> keys = new List<int>(sourceSet.Turtles.Keys);
            foreach (int key in keys)
                Turtles[key] = new Turtle(sourceSet.Turtles[key]);

            bounds = new ScreenLimits((int)sourceSet.safeZonePadding);
        }

        /// <summary>
        /// Adds turtle to the Dictionary if it doesn't currently exist.
        /// </summary>
        /// <param name="turtleNumber">ID of the potential new turtle.</param>
        public void AddTurtleIfAbsent(int turtleNumber)
        {
            if (!Turtles.ContainsKey(turtleNumber))
                Turtles.Add(turtleNumber, new Turtle(GetCenterOfTheScreen()));
        }

        /// <summary>
        /// Clears Dictionary - disposes all turtles.
        /// </summary>
        private void ClearTurtles()
        {
            foreach (Turtle turtle in Turtles.Values)
                if (turtle != null) turtle.Dispose();

            Turtles.Clear();
        }

        /// <summary>
        /// Returns the final image; working area of the final surface without the safe zone.
        /// </summary>
        /// <returns>Bitmap of the final image.</returns>
        public Bitmap GetImage()
        {
            using (Bitmap FinalBitmap = new Bitmap(BaseLayer.Image))
            {
                using (Graphics FL = Graphics.FromImage(FinalBitmap))
                    foreach (Turtle turtle in Turtles.Values)
                        if (!turtle.IsHidden)
                            using (Bitmap RotatedTurtleArt = turtle.RotateSquareTurtleImage(turtle.Angle - Turtle.defaultAngle))
                            {
                                FL.DrawImage(RotatedTurtleArt, CalculateTurtlePoint(RotatedTurtleArt.Width, RotatedTurtleArt.Height, turtle.CurrentPoint));
                            }


                RectangleF rectangle = new RectangleF(bounds.Left, bounds.Top, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                return FinalBitmap.Clone(rectangle, FinalBitmap.PixelFormat);
            }
        }

        /// <summary>
        /// Draw a line on the base surface from the current point of the specified turtle. Handles wrapping if the destination point is out of borders. 
        /// </summary>
        /// <param name="steps">Steps to move.</param>
        /// <param name="turtleNumber">ID of the turtle to move.</param>
        public void DrawLine(float steps, int turtleNumber)
        {
            if (Turtles.ContainsKey(turtleNumber))
            {
                Turtle currentTurtle = Turtles[turtleNumber];
                while (steps != 0)
                {
                    PointF destinationPoint = CalculateNewPoint(steps, currentTurtle.Angle, currentTurtle.CurrentPoint);

                    // Contact point is a point at which the turtle reaches a wall.

                    if (destinationPoint.Y < bounds.Top)
                    {
                        float knownY = bounds.Top;

                        float contactPointX = CalculateContactPointX(knownY, currentTurtle.Angle, currentTurtle.CurrentPoint, out float stepsToWall);
                        PointF contactPoint = new PointF(contactPointX, knownY);
                        DrawToPoint(contactPoint, currentTurtle);

                        currentTurtle.CurrentPoint = new PointF(contactPointX, bounds.Bottom);
                        steps -= stepsToWall;
                    }
                    else if (destinationPoint.Y > bounds.Bottom)
                    {
                        float knownY = bounds.Bottom;

                        float contactPointX = CalculateContactPointX(knownY, currentTurtle.Angle, currentTurtle.CurrentPoint, out float stepsToWall);
                        PointF contactPoint = new PointF(contactPointX, knownY);
                        DrawToPoint(contactPoint, currentTurtle);

                        currentTurtle.CurrentPoint = new PointF(contactPointX, bounds.Top);
                        steps -= stepsToWall;
                    }
                    else if (destinationPoint.X > bounds.Right)
                    {
                        float knownX = bounds.Right;

                        float contactPointY = CalculateContactPointY(knownX, currentTurtle.Angle, currentTurtle.CurrentPoint, out float stepsToWall);
                        PointF contactPoint = new PointF(knownX, contactPointY);
                        DrawToPoint(contactPoint, currentTurtle);

                        currentTurtle.CurrentPoint = new PointF(bounds.Left, contactPointY);
                        steps -= stepsToWall;
                    }
                    else if (destinationPoint.X < bounds.Left)
                    {
                        float knownX = bounds.Left;

                        float contactPointY = CalculateContactPointY(knownX, currentTurtle.Angle, currentTurtle.CurrentPoint, out float stepsToWall);
                        PointF contactPoint = new PointF(knownX, contactPointY);
                        DrawToPoint(contactPoint, currentTurtle);

                        currentTurtle.CurrentPoint = new PointF(bounds.Right, contactPointY);
                        steps -= stepsToWall;
                    }
                    else
                    {
                        DrawToPoint(destinationPoint, currentTurtle);
                        currentTurtle.CurrentPoint = destinationPoint;
                        steps = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Calculate Y of the contact point based on the known X, current angle and current point. 
        /// In addition, calculates required steps to reach the point.
        /// </summary>
        /// <param name="contactPointX">Known X of the contact point.</param>
        /// <param name="angle">Known angle.</param>
        /// <param name="currentPoint">Current point.</param>
        /// <param name="steps">Number of steps required to reach the contact point.</param>
        /// <returns>Float of Y of the contact point.</returns>
        private float CalculateContactPointY(float contactPointX, int angle, PointF currentPoint, out float steps)
        {
            // Shouldn't happen; implemented for safety.
            if (angle == 90)
            {
                steps = bounds.Top - currentPoint.Y;
                return bounds.Top;
            }
            if (angle == 270)
            {
                steps = bounds.Bottom - currentPoint.Y;
                return bounds.Bottom;
            }

            float angleInRadians = DegreesToRadians(angle);
            steps = (float)((contactPointX - currentPoint.X) / Math.Cos(angleInRadians));
            float y = (float)(currentPoint.Y + (steps * Math.Sin(angleInRadians)));
            return y;
        }

        /// <summary>
        /// Calculate X of the contact point based on the known Y, current angle and current point. 
        /// In addition, calculates required steps to reach the point.
        /// </summary>
        /// <param name="contactPointY">Known Y of the contact point.</param>
        /// <param name="angle">Known angle.</param>
        /// <param name="currentPoint">Current point.</param>
        /// <param name="steps">Number of steps required to reach the contact point.</param>
        /// <returns>Float of X of the contact point.</returns>
        private float CalculateContactPointX(float contactPointY, int angle, PointF currentPoint, out float steps)
        {
            // Shouldn't happen; implemented for safety.
            if (angle == 0)
            {
                steps = bounds.Left - currentPoint.X;
                return bounds.Left;
            }
            if (angle == 180)
            {
                steps = bounds.Right - currentPoint.X;
                return bounds.Right;
            }

            float angleInRadians = DegreesToRadians(angle);
            steps = (float)((contactPointY - currentPoint.Y) / Math.Sin(angleInRadians));
            float x = (float)(currentPoint.X + (steps * Math.Cos(angleInRadians)));
            return x;
        }

        /// <summary>
        /// Draw a line by the given turtle to a given point.
        /// </summary>
        /// <param name="destinationPoint">Point to be drawn to.</param>
        /// <param name="turtle">A turtle to take care of drawing.</param>
        private void DrawToPoint(PointF destinationPoint, Turtle turtle)
        {
            if (turtle.PenIsDown)
                BaseLayer.Surface.DrawLine(turtle.Pens[turtle.ActivePenIndex], turtle.CurrentPoint, destinationPoint);
        }

        /// <summary>
        /// Returns center of the screen.
        /// </summary>
        /// <returns>PointF as a center of the screen.</returns>
        private PointF GetCenterOfTheScreen()
        {
            int widthWithSafeZone = Screen.PrimaryScreen.Bounds.Width + (int)safeZonePadding * 2;
            int heightWithSafeZone = Screen.PrimaryScreen.Bounds.Height + (int)safeZonePadding * 2;
            return new PointF(widthWithSafeZone / 2, heightWithSafeZone / 2);
        }

        /// <summary>
        /// Uses mathematical formula to calculate new point based on initial point, distance and angle.
        /// </summary>
        /// <param name="steps">Distance to a new point.</param>
        /// <param name="angle">Angle to a new point.</param>
        /// <param name="currentPoint">Current point.</param>
        /// <returns>A new point.</returns>
        private PointF CalculateNewPoint(float steps, int angle, PointF currentPoint)
        {
            float angleInRadians = DegreesToRadians(angle);
            float x = (float)(currentPoint.X + (steps * Math.Cos(angleInRadians)));
            float y = (float)(currentPoint.Y + (steps * Math.Sin(angleInRadians)));
            return new PointF(x, y);
        }

        /// <summary>
        /// Converts degrees to radians.
        /// </summary>
        /// <param name="degrees">Degrees to be converted.</param>
        /// <returns>The number of radians representing provided degrees.</returns>
        private float DegreesToRadians(int degrees)
        {
            return (float)(degrees * Math.PI / 180);
        }

        /// <summary>
        /// Calculates required padding for turtle position based on the current point (centers the image).
        /// </summary>
        /// <param name="turtleWidth">Width of the turtle image.</param>
        /// <param name="turtleHeight">Height of the turtle image.</param>
        /// <param name="currentPoint">Current point of the turtle.</param>
        /// <returns>Point - center of the Turtle image.</returns>
        private PointF CalculateTurtlePoint(int turtleWidth, int turtleHeight, PointF currentPoint)
        {
            // TODO: There is a problem with centering the image of the turtle due to the modification (rotation).
            // It is hard to tell whether the issue has a simple solution.
            // Let's decrement by one for now, it looks fine for the first turtle, but the others start to shift.

            float turtlePaddingX = (turtleWidth / 2) - 1;
            float turtlePaddingY = (turtleHeight / 2) - 1;

            float imageX = currentPoint.X - turtlePaddingX;
            float imageY = currentPoint.Y - turtlePaddingY;

            return new PointF(imageX, imageY);
        }
    }
}
