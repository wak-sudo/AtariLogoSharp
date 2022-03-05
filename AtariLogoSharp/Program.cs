/* Atari Logo Sharp - implementation of Atari Logo in C#. Developed by 404 team. */

using System;
using System.Windows.Forms;

namespace AtariLogoSharp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new InputScreen());
        }
    }
}
