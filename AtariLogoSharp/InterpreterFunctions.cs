/* Atari Logo Sharp - implementation of Atari Logo in C#. Developed by 404 team. */

using System;
using System.Collections.Generic;
using System.Linq;

namespace AtariLogoSharp
{
    public partial class Interpreter
    {
        /// <summary>
        /// Splits code into individual strings and removes all white spaces, accepts lower case characters.
        /// </summary>
        /// <param name="rawCode">The raw code with whitespaces.</param>
        /// <returns>The cleaned up code to work with.</returns>
        public static List<string> CleanCode(string rawCode)
        {
            List<string> cleanedCode = SplitBrackets(rawCode.ToUpper().Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList());
            return cleanedCode;
        }

        /// <summary>
        /// Splits brackets apart from the commands. Ex.: if the word is "[FD", it will split it to "[" and "FD". Likewise, "FD]" to "FD" and "]".
        /// </summary>
        /// <param name="commands">List of commands.</param>
        /// <returns>List with commands split apart from brackets.</returns>
        private static List<string> SplitBrackets(List<string> commands)
        {
            for (int i = 0; i < commands.Count; i++)
            {
                if (commands[i][0] == '[' && commands[i].Length != 1)
                {
                    commands[i] = commands[i].Substring(1);
                    commands.Insert(i, "[");
                    i++;
                }
                if (commands[i][commands[i].Length - 1] == ']' && commands[i].Length != 1)
                {
                    commands[i] = commands[i].Remove(commands[i].Length - 1);
                    commands.Insert(i + 1, "]");
                    i++;
                }
            }
            return commands;
        }

        /// <summary>
        /// Modulo implementation, ex. Mod(-15, 360) = 345
        /// </summary>
        public static int Mod(int i, int k = 360)
        {
            int reminder = i % k;
            if (reminder >= 0)
                return reminder;
            else return reminder + k;
        }
    }
}
