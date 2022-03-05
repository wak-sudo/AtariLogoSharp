/* Atari Logo Sharp - implementation of Atari Logo in C#. Developed by 404 team. */

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using System.Linq;

namespace AtariLogoSharp.Test
{
    [TestClass]
    public class SyntaxTest
    {
        [TestMethod]
        public void TestSyntax()
        {
            Interpreter interpreter = new Interpreter(new DrawSet());

            // These test samples MUST appear in the order of IRC values.
            List<List<string>> testInput = new List<List<string>>()
            {
                new List<string>() // NO_ERROR = 0
                {
                    "CS HT ST PU PD", // No arguments commands.
                    "RT 50 LT 90 FD 50 BK 90", // One argument commands. 
                    "REPEAT 1 [FD 50 LT 90]", // Loop.
                    "REPEAT 1 [ FD 50 REPEAT 1 [ CS FD 50 ] LT 90 ]", // Nested loop.
                    "", // Empty string.
                },

                new List<string>() // UNK_COMMAND = 1
                {
                    "CS GW",
                },

                new List<string>() // NO_BRACKETS = 2
                {
                    "REPEAT 1 CS",
                },

                new List<string>() // NO_CLOSING_BRACKET = 3
                {
                    "REPEAT 1 [CS",
                },

                new List<string>() // NO_NUMBER = 4
                {
                    "RT", "RT A", "REPEAT [CS]",
                },

                new List<string>() // NO_IDENTIFIER = 5
                {
                    "TO",
                },

                new List<string>() // NO_PROCEDURE_END = 6
                {
                    "TO STAR", "TO STAR :SIZE",
                },

                new List<string>() // DECLARATION_WITHIN_PROCEDURE = 7
                {
                    "TO STAR TO SQUARE END END",
                },

                new List<string>() // PRIMITIVE_REDEFINED = 8
                {
                    "TO FD CS END",
                    "TO 4",
                    "TO END",
                },

                new List<string>() // NO_PARAMETER_PROVIDED = 9
                {
                    "TO MOVE :LENGTH FD :LENGTH END MOVE",
                    "TO MOVE :LENGTH FD :LENGTH END MOVE A",

                    "TO MOVE :LENGTH :ANGLE RT :ANGLE FD :LENGTH END MOVE",
                    "TO MOVE :LENGTH :ANGLE RT :ANGLE FD :LENGTH END MOVE A",
                    "TO MOVE :LENGTH :ANGLE RT :ANGLE FD :LENGTH END MOVE 1",
                    "TO MOVE :LENGTH :ANGLE RT :ANGLE FD :LENGTH END MOVE A B",
                },
            };

            const string failedTestInputMessage = "Failed at input test {0} no. {1}";
            for (int i = 0; i < testInput.Count; i++)
            {
                if (testInput[i].Count != 0)
                    for (int j = 0; j < testInput[i].Count(); j++)
                    {
                        if (interpreter.Interpret(testInput[i][j]) != (IRC)i)
                            Assert.Fail(String.Format(failedTestInputMessage, i, j));
                    }
            }

            if (interpreter != null)
                interpreter.Dispose();
        }
    }
}
