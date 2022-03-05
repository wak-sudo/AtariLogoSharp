/* Atari Logo Sharp - implementation of Atari Logo in C#. Developed by 404 team. */

namespace AtariLogoSharp
{
    public partial class Interpreter
    {
        private void ClearScreen()
        {
            ActiveGraphic.Clear();
            InterpretedGraphic.Clear();

            activeTurtles.Clear();
            activeTurtles.Add(0);
        }

        private void HideTurtle()
        {
            foreach (int turtleNumber in activeTurtles)
                if (ActiveGraphic.Turtles.ContainsKey(turtleNumber)) // Implemented just for safety.
                    ActiveGraphic.Turtles[turtleNumber].IsHidden = true;
        }

        private void ShowTurtle()
        {
            foreach (int turtleNumber in activeTurtles)
                if (ActiveGraphic.Turtles.ContainsKey(turtleNumber)) // Implemented just for safety.
                    ActiveGraphic.Turtles[turtleNumber].IsHidden = false;
        }

        private void PenUp()
        {
            foreach (int turtleNumber in activeTurtles)
                if (ActiveGraphic.Turtles.ContainsKey(turtleNumber)) // Implemented just for safety.
                    ActiveGraphic.Turtles[turtleNumber].PenIsDown = false;
        }

        private void PenDown()
        {
            foreach (int turtleNumber in activeTurtles)
                if (ActiveGraphic.Turtles.ContainsKey(turtleNumber)) // Implemented just for safety.
                    ActiveGraphic.Turtles[turtleNumber].PenIsDown = true;
        }

        private void RightTurtle(int angle)
        {
            foreach (int turtleNumber in activeTurtles)
                if (ActiveGraphic.Turtles.ContainsKey(turtleNumber)) // Implemented just for safety.
                    ActiveGraphic.Turtles[turtleNumber].RotateTurtle(angle);
        }

        private void LeftTurtle(int angle)
        {
            RightTurtle(-angle);
        }

        private void Forward(int steps)
        {
            steps = -steps; // Because the biggest numbers are at the bottom, the directions must be reversed. 
            foreach (int turtleNumber in activeTurtles)
                if (ActiveGraphic.Turtles.ContainsKey(turtleNumber)) // Implemented just for safety.
                    ActiveGraphic.DrawLine(steps, turtleNumber);
        }

        private void Backwards(int steps)
        {
            Forward(-steps); // Move the other way.
        }

        private void RemoveAllProcedures()
        {
            ProcedureToCode.Clear();
            ProcedureToParams.Clear();
        }

        /// <summary>
        /// Paints turtle arts with specified color.
        /// </summary>
        /// <param name="colorID">Color ID regarding Atari pallet.</param>
        private void SetColorOfTurtle(uint colorID)
        {
            foreach (int turtleNumber in activeTurtles)
                if (ActiveGraphic.Turtles.ContainsKey(turtleNumber)) // Implemented just for safety.
                    ActiveGraphic.Turtles[turtleNumber].PaintImage(AtariColorPallet.GetColor(colorID));
        }

        /// <summary>
        /// Sets an active pen for turtles. 
        /// </summary>
        /// <param name="penID">Selected pen.</param>
        private void SetActivePen(int penID)
        {
            foreach (int turtleNumber in activeTurtles)
                if (ActiveGraphic.Turtles.ContainsKey(turtleNumber)) // Implemented just for safety.
                    ActiveGraphic.Turtles[turtleNumber].ActivePenIndex = penID;
        }

        /// <summary>
        /// Sets color for a specified pen.
        /// </summary>
        /// <param name="penID">The ID of a pen whose color is to be changed.</param>
        /// <param name="colorID">New color of a specified pen.</param>
        private void SetPenColor(int penID, uint colorID)
        {
            foreach (int turtleNumber in activeTurtles)
                if (ActiveGraphic.Turtles.ContainsKey(turtleNumber)) // Implemented just for safety.
                    ActiveGraphic.Turtles[turtleNumber].Pens[penID].Color = AtariColorPallet.GetColor(colorID);
        }
    }
}
