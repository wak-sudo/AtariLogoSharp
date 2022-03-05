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
                    "CS HT ST PU PD POTS ERALL", // No arguments commands.
                    "RT 50 LT 90 FD 50 BK 90", // Some one argument commands. 
                    "REPEAT 1 [FD 50 LT 90]", // Loop.
                    "REPEAT 1 [ FD 50 REPEAT 1 [ CS FD 50 ] LT 90 ]", // Nested loop.
                    "TO FORWARD FD 100 END FORWARD", // Procedure declaration and invocation.
                    "TELL 1", // Tell without brackets.
                    "TELL [ 0 1 ]", // Tell with brackets.
                    "ASK [ 0 ] [ FD 100 ]",
                    "SETC 1",
                    "SETPN 1",
                    "SETPC 0 1",
                    "EACH [ FD 100 ]",
                    "TO A FD 100 END ED A", // Edit procedure.
                    "", // Empty string.
                },

                new List<string>() // UNK_COMMAND = 1
                {
                    "CS GW",
                },

                new List<string>() // NO_BRACKETS = 2
                {
                    "REPEAT 1 CS",
                    "ASK", "ASK A", "ASK [ 0 ]",
                    "EACH", "EACH A",
                },

                new List<string>() // NO_CLOSING_BRACKET = 3
                {
                    "REPEAT 1 [CS",
                    "EACH [ CS",
                    "ASK [ 0", "ASK [ 0 ] [ CS",
                },

                new List<string>() // NO_NUMBER = 4
                {
                    "RT", "RT A", "REPEAT [CS]",
                    "SETC", "SETC A", "SETPN", "SETPN A", "SETPC", "SETPC A", "SETPC 1 A",
                },

                new List<string>() // NO_IDENTIFIER = 5
                {
                    "TO", "ED", "ED []",
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

                new List<string>() // PARAMETER_REPEATED = 10
                {
                    "TO STAR :A :A FD :A",
                },

                new List<string>() // NO_FILEPATH = 11
                {
                    "SAVE", "LOAD",
                },

                new List<string>() // COLOR_OUT_OF_RANGE = 12
                {
                    "SETC -1", "SETC 128", "SETPC 0 -1", "SETPC 0 128",
                },

                new List<string>() // PEN_INDEX_OUT_OF_RANGE = 13
                {
                    "SETPN -1", "SETPN 3", "SETPC -1 0", "SETPC 3 0",
                },

                new List<string>() // PROCEDURE_IS_NOT_DEFINED = 14
                {
                    "ED [ NOT_DEFINED ]", "ED NOT_DEFINED",
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
