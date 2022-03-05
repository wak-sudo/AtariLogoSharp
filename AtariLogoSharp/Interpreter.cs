/* Atari Logo Sharp - implementation of Atari Logo in C#. Developed by 404 team. */

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

namespace AtariLogoSharp
{
    /// <summary>
    /// Class created for interpreting Atari Logo commands.
    /// </summary>
    public partial class Interpreter : IDisposable
    {
        /// <summary>
        /// Hold the last valid instruction before error.
        /// </summary>
        public string ErrorBreakCommand = "";

        /// <summary>
        /// The current interpretation of the graphic.
        /// </summary>
        private readonly DrawSet ActiveGraphic = new DrawSet();

        /// <summary>
        /// The last successful interpretation of the graphic.
        /// </summary>
        public DrawSet InterpretedGraphic { get; set; } = new DrawSet();

        /// <summary>
        /// Stores the cleaned up code.
        /// </summary>
        private List<string> Code;

        /// <summary>
        /// Holds the index of Code.
        /// </summary>
        private int CodePointer { get; set; } = 0;

        /// <summary>
        /// Stores the IDs of the active turtles. 
        /// REMARK:
        /// There might be turtles that exist but are not active.
        /// </summary>
        private List<int> activeTurtles = new List<int>() { 0 };

        /// <summary>
        /// Assign procedure code to the procedure name.
        /// </summary>
        public Dictionary<string, List<string>> ProcedureToCode { get; set; } = new Dictionary<string, List<string>>();

        /// <summary>
        /// Assign procedure parameters to the procedure name.
        /// </summary>
        public Dictionary<string, List<string>> ProcedureToParams { get; set; } = new Dictionary<string, List<string>>();

        /// <summary>
        /// The part of the syntax that is immutable, reserved.
        /// </summary>
        private readonly string[] primitives = { "CS", "HT", "ST", "PU", "PD", "RT", "LT", "FD", "BK", "REPEAT", "[", "]", "TO", "END", "ERALL", "TELL", "ASK", "POTS", "LOAD", "SAVE", "SETC", "ED", "EACH" };

        /// <summary>
        /// This character correspond to the opening bracket character.
        /// </summary>
        private const string openingBracketCharacter = "[";

        /// <summary>
        /// This character correspond to the closing bracket character.
        /// </summary>
        private const string closingBracketCharacter = "]";

        /// <summary>
        /// Indicates whether a POTS command was successfully entered in the last interpretation.
        /// </summary>
        public bool POTSCommandInterpreted { get; private set; } = false;

        /// <summary>
        /// Procedures awaiting editing, the list is filled via the ED command.
        /// </summary>
        public List<string> ProceduresToBeEdited { get; set; } = new List<string>();

        /// <summary>
        /// Interpreter constructor. Set the initial state of the graphic to the provided DrawSet.
        /// </summary>
        /// <param name="currentImage">DrawSet to be copied from.</param>
        public Interpreter(DrawSet currentImage)
        {
            ActiveGraphic.CopyFrom(currentImage);
            InterpretedGraphic.CopyFrom(currentImage);
        }

        /// <summary>
        /// Disposes object.
        /// </summary>
        public void Dispose()
        {
            if (ActiveGraphic != null) ActiveGraphic.Dispose();
            if (InterpretedGraphic != null) InterpretedGraphic.Dispose();
        }

        /// <summary>
        /// The main logic behind the interpreter. Cleans code, interprets commands, updates graphics (if no error occurs).
        /// </summary>
        /// <param name="rawCode">The raw code with whitespaces.</param>
        /// <returns>An interpreter return code (IRC). If returns NO_ERROR, the code is valid and the InterpretedGraphic is updated.</returns>
        public IRC Interpret(string rawCode)
        {
            Code = CleanCode(rawCode);
            CodePointer = 0;
            POTSCommandInterpreted = false;
            ActiveGraphic.CopyFrom(InterpretedGraphic);
            while (CodePointer < Code.Count)
            {
                IRC interpreterResults = CommandInterpreter();
                if (interpreterResults != IRC.NO_ERROR)
                {
                    ActiveGraphic.CopyFrom(InterpretedGraphic);

                    if (CodePointer - 1 >= 0 && interpreterResults != IRC.UNK_COMMAND)
                        CodePointer--; // Move to the last successful instruction.
                    ErrorBreakCommand = Code[CodePointer];
                    POTSCommandInterpreted = false;
                    ProceduresToBeEdited.Clear();
                    return interpreterResults;
                }
                else CodePointer++;
            }
            InterpretedGraphic.CopyFrom(ActiveGraphic);
            // Remove duplicates.
            ProceduresToBeEdited = ProceduresToBeEdited.Distinct().ToList();

            return IRC.NO_ERROR;
        }

        /// <summary>
        /// Interprets the current command.
        /// </summary>
        /// <param name="actMode">If true, the command will be applied to the graphic.</param>
        /// <returns>An interpreter return code (IRC). If returns NO_ERROR, the current command is valid.</returns>
        private IRC CommandInterpreter(bool actMode = true)
        {
            switch (Code[CodePointer])
            {
                case "CS": // clear screen
                    {
                        if (actMode)
                            ClearScreen();
                    }
                    break;

                case "HT": // hide turtle
                    {
                        if (actMode)
                            HideTurtle();
                    }
                    break;

                case "ST": // show turtle
                    {
                        if (actMode)
                            ShowTurtle();
                    }
                    break;

                case "PU": // pen up
                    {
                        if (actMode)
                            PenUp();
                    }
                    break;

                case "PD": // pen down
                    {
                        if (actMode)
                            PenDown();
                    }
                    break;

                case "RT": // right turtle
                    {
                        if (MoveToNextInstruction() == IRC.NO_ERROR)
                        {
                            IRC FirstArgResult = NumberArgHandler(out int angle);
                            if (FirstArgResult == IRC.NO_ERROR)
                            {
                                if (actMode)
                                    RightTurtle(angle);
                            }
                            else return FirstArgResult;
                        }
                        else return IRC.NO_NUMBER;
                    }
                    break;
                case "LT": // left turtle
                    {
                        if (MoveToNextInstruction() == IRC.NO_ERROR)
                        {
                            IRC FirstArgResult = NumberArgHandler(out int angle);
                            if (FirstArgResult == IRC.NO_ERROR)
                            {
                                if (actMode)
                                    LeftTurtle(angle);
                            }
                            else return FirstArgResult;
                        }
                        else return IRC.NO_NUMBER;
                    }
                    break;
                case "FD": // forward
                    {
                        if (MoveToNextInstruction() == IRC.NO_ERROR)
                        {
                            IRC FirstArgResult = NumberArgHandler(out int steps);
                            if (FirstArgResult == IRC.NO_ERROR)
                            {
                                if (actMode)
                                    Forward(steps);
                            }
                            else return FirstArgResult;
                        }
                        else return IRC.NO_NUMBER;
                    }
                    break;

                case "BK": // backwards
                    {
                        if (MoveToNextInstruction() == IRC.NO_ERROR)
                        {
                            IRC FirstArgResult = NumberArgHandler(out int steps);
                            if (FirstArgResult == IRC.NO_ERROR)
                            {
                                if (actMode)
                                    Backwards(steps);
                            }
                            else return FirstArgResult;
                        }
                        else return IRC.NO_NUMBER;
                    }
                    break;

                case "SETC":
                    {
                        if (MoveToNextInstruction() == IRC.NO_ERROR)
                        {
                            IRC FirstArgResult = NumberArgHandler(out int colorID);
                            if (FirstArgResult == IRC.NO_ERROR)
                            {
                                if (colorID >= 0 && colorID < 128)
                                {
                                    if (actMode)
                                        SetColorOfTurtle((uint)colorID);
                                }
                                else return IRC.COLOR_OUT_OF_RANGE;
                            }
                            else return FirstArgResult;
                        }
                        else return IRC.NO_NUMBER;
                    }
                    break;

                case "SETPN":
                    {
                        if (MoveToNextInstruction() == IRC.NO_ERROR)
                        {
                            IRC FirstArgResult = NumberArgHandler(out int penID);
                            if (FirstArgResult == IRC.NO_ERROR)
                            {
                                if (penID >= 0 && penID <= 2)
                                {
                                    if (actMode)
                                        SetActivePen(penID);
                                }
                                else return IRC.PEN_INDEX_OUT_OF_RANGE;
                            }
                            else return FirstArgResult;
                        }
                        else return IRC.NO_NUMBER;
                    }
                    break;

                case "SETPC":
                    {
                        if (MoveToNextInstruction() == IRC.NO_ERROR)
                        {
                            IRC FirstArgResult = NumberArgHandler(out int penID);
                            if (FirstArgResult == IRC.NO_ERROR)
                                if (penID >= 0 && penID <= 2)
                                    if (MoveToNextInstruction() == IRC.NO_ERROR)
                                    {
                                        IRC SecondArgResult = NumberArgHandler(out int colorID);
                                        if (SecondArgResult == IRC.NO_ERROR)
                                            if (colorID >= 0 && colorID < 128)
                                            {
                                                if (actMode)
                                                    SetPenColor(penID, (uint)colorID);
                                            }
                                            else return IRC.COLOR_OUT_OF_RANGE;
                                        else return SecondArgResult;
                                    }
                                    else return IRC.NO_NUMBER;
                                else return IRC.PEN_INDEX_OUT_OF_RANGE;
                            else return FirstArgResult;
                        }
                        else return IRC.NO_NUMBER;
                    }
                    break;

                case "REPEAT":
                    {
                        if (MoveToNextInstruction() == IRC.NO_ERROR)
                        {
                            IRC NumberArgResult = NumberArgHandler(out int number);
                            if (NumberArgResult == IRC.NO_ERROR)
                            {
                                if (MoveToNextInstruction() != IRC.NO_ERROR) // Checks if there is an opening bracket index.
                                    return IRC.NO_BRACKETS;

                                if (Code[CodePointer] == openingBracketCharacter)
                                {
                                    int startingPosition = CodePointer; // For returning to starting position after each repetition.

                                    for (int i = 0; i < number; i++)
                                    {
                                        // TODO: Is this can be done in a better way without breaking the logic?
                                        while (true)
                                        {
                                            if (MoveToNextInstruction() != IRC.NO_ERROR)
                                                return IRC.NO_CLOSING_BRACKET;

                                            if (Code[CodePointer] == closingBracketCharacter)
                                                break;
                                            else
                                            {
                                                IRC interpreterResult = CommandInterpreter(actMode);
                                                if (interpreterResult != IRC.NO_ERROR)
                                                    return interpreterResult;
                                            }
                                        }

                                        if (i != number - 1) // Return to beginning every time, except the final iteration.
                                            CodePointer = startingPosition;
                                    }
                                }
                                else return IRC.NO_BRACKETS;
                            }
                            else return NumberArgResult;
                        }
                        else return IRC.NO_NUMBER;
                    }
                    break;


                case "TELL":
                    {
                        if (MoveToNextInstruction() != IRC.NO_ERROR) // If there is no parameter after TELL.
                            return IRC.NO_NUMBER;

                        if (Code[CodePointer] == openingBracketCharacter) // If there are brackets.
                        {
                            IRC getNumbersResult = GetNumbersBetweenBrackets(out List<int> selectedTurtles);
                            if (getNumbersResult == IRC.NO_ERROR)
                            {
                                if (actMode)
                                    activeTurtles = selectedTurtles;
                            }
                            else return getNumbersResult;
                        }
                        else // otherwise, there must be a single number.
                        {
                            IRC result = NumberArgHandler(out int selectedTurtle);
                            if (result == IRC.NO_ERROR)
                            {
                                if (actMode)
                                {
                                    activeTurtles.Clear();
                                    activeTurtles.Add(selectedTurtle);
                                }
                            }
                            else return result;
                        }

                        if (actMode)
                        {
                            foreach (int number in activeTurtles)
                                ActiveGraphic.AddTurtleIfAbsent(number);
                        }
                    }
                    break;

                case "ASK":
                    {
                        if (MoveToNextInstruction() != IRC.NO_ERROR)
                            return IRC.NO_BRACKETS;

                        // Get selected turtles.
                        List<int> selectedTurtles = new List<int>();
                        if (Code[CodePointer] == openingBracketCharacter)
                        {
                            IRC getNumbersResult = GetNumbersBetweenBrackets(out List<int> numbersInBrackets);
                            if (getNumbersResult == IRC.NO_ERROR)
                            {
                                if (actMode)
                                    selectedTurtles = numbersInBrackets;
                            }
                            else return getNumbersResult;

                            if (actMode)
                            {
                                foreach (int number in selectedTurtles)
                                    ActiveGraphic.AddTurtleIfAbsent(number);
                            }
                        }
                        else return IRC.NO_BRACKETS;

                        if (MoveToNextInstruction() != IRC.NO_ERROR)
                            return IRC.NO_BRACKETS;

                        // Execute the provided code.
                        List<int> orgActiveTurtles = new List<int>(activeTurtles);
                        if (Code[CodePointer] == openingBracketCharacter)
                        {
                            activeTurtles = selectedTurtles;
                            while (true)
                            {
                                if (MoveToNextInstruction() != IRC.NO_ERROR)
                                    return IRC.NO_CLOSING_BRACKET;

                                if (Code[CodePointer] == closingBracketCharacter)
                                    break;
                                else
                                {
                                    IRC interpreterResult = CommandInterpreter(actMode);
                                    if (interpreterResult != IRC.NO_ERROR)
                                    {
                                        // Something went wrong, so we return to initial selected turtles.
                                        if (actMode)
                                            activeTurtles = orgActiveTurtles;
                                        return interpreterResult;
                                    }
                                }
                            }
                            // Move back to the initial state.
                            if (actMode)
                                activeTurtles = orgActiveTurtles;
                        }
                        else return IRC.NO_BRACKETS;
                    }
                    break;

                case "TO":
                    {
                        if (MoveToNextInstruction() == IRC.NO_ERROR)
                        {
                            IRC ProcedureNameResult = ProcedureNameHandler(out string procedureName);
                            if (ProcedureNameResult == IRC.NO_ERROR)
                            {
                                if (MoveToNextInstruction() != IRC.NO_ERROR)
                                    return IRC.NO_PROCEDURE_END;

                                // Get provided parameter names.
                                List<string> parameters = new List<string>();
                                while (Code[CodePointer][0] == ':')
                                {
                                    if (!parameters.Contains(Code[CodePointer]))
                                        parameters.Add(Code[CodePointer]);
                                    else return IRC.PARAMETER_REPEATED;
                                    if (MoveToNextInstruction() != IRC.NO_ERROR)
                                        return IRC.NO_PROCEDURE_END;
                                }

                                // Get provided code and check for some prohibited criteria.
                                List<string> procedureCode = new List<string>();
                                int startIndex = CodePointer;
                                while (Code[CodePointer] != "END")
                                {
                                    string command = Code[CodePointer];

                                    if (command == "TO")
                                        return IRC.DECLARATION_WITHIN_PROCEDURE;

                                    if (MoveToNextInstruction() != IRC.NO_ERROR)
                                        return IRC.NO_PROCEDURE_END;

                                    procedureCode.Add(command);
                                }

                                // Replace parameters within code with test value. It is done this way due to the design of the syntax checker.
                                const string testValue = "0";
                                for (int i = startIndex; i < CodePointer; i++)
                                {
                                    foreach (string paramName in parameters)
                                        if (Code[i] == paramName)
                                            Code[i] = testValue;
                                }

                                // Finally, check the code with replaced parameters.
                                CodePointer = startIndex;
                                while (Code[CodePointer] != "END")
                                {
                                    IRC interpretedResult = CommandInterpreter(false);
                                    if (interpretedResult != IRC.NO_ERROR)
                                        return interpretedResult;

                                    CodePointer++;
                                }

                                // Assign corresponding parameters and code to the procedure name.
                                ProcedureToCode[procedureName] = procedureCode;
                                ProcedureToParams[procedureName] = parameters;
                            }
                            else return ProcedureNameResult;
                        }
                        else return IRC.NO_IDENTIFIER;
                    }
                    break;

                case "ERALL":
                    {
                        if (actMode)
                        {
                            RemoveAllProcedures();
                            ProceduresToBeEdited.Clear();
                        }

                    }
                    break;

                case "POTS":
                    {
                        if (actMode)
                            POTSCommandInterpreted = true;
                    }
                    break;

                case "LOAD":
                    {
                        if (MoveToNextInstruction() == IRC.NO_ERROR)
                        {
                            string filePath = Code[CodePointer];
                            int filePathIndex = CodePointer;
                            try
                            {
                                if (File.Exists(filePath))
                                {
                                    List<string> procedureCode = CleanCode(File.ReadAllText(filePath));

                                    // Remove LOAD filepath and replace it with loaded code.
                                    int starIndex = CodePointer - 1;
                                    int endIndex = starIndex + procedureCode.Count - 1;
                                    Code.RemoveRange(starIndex, 2);
                                    Code.InsertRange(starIndex, procedureCode);
                                    CodePointer = starIndex;

                                    while (CodePointer <= endIndex)
                                    {
                                        IRC interpretedResult = CommandInterpreter(false);
                                        if (interpretedResult != IRC.NO_ERROR)
                                            return IRC.INVALID_FILE;

                                        CodePointer++;
                                    }

                                    // The value will be increased in the main loop by one, we want to stay at the right index, so we decrement by one.
                                    CodePointer--;
                                }
                                else return IRC.NO_FILEPATH;
                            }
                            catch
                            {
                                CodePointer = filePathIndex + 1;
                                // In this special case, we want to show the unsuccessful file path,
                                // so we have to increment the code pointer to stay at the right index.
                                return IRC.FAILED_LOADING;
                            }

                        }
                        else return IRC.NO_FILEPATH;
                    }
                    break;

                case "SAVE":
                    {
                        if (MoveToNextInstruction() == IRC.NO_ERROR)
                        {
                            try
                            {
                                if (actMode)
                                {
                                    string filePath = Code[CodePointer];
                                    FileInfo file = new FileInfo(filePath);
                                    using (StreamWriter writer = new StreamWriter(filePath))
                                    {
                                        List<string> procedureNames = new List<string>(ProcedureToCode.Keys);
                                        foreach (string procedureName in procedureNames)
                                        {
                                            string parameters = string.Join(" ", ProcedureToParams[procedureName]);
                                            string code = string.Join(" ", ProcedureToCode[procedureName]);
                                            string fullDeclaration = ("TO " + procedureName + " " + parameters + " " + code + " END");
                                            writer.WriteLine(fullDeclaration);
                                        }
                                    }
                                }
                            }
                            catch(Exception e)
                            {
                                CodePointer++; // In this special case, we want to show the unsuccessful file path,
                                               // so we have to increment the code pointer to stay at the right index.
                                return IRC.FAILED_SAVING;
                            }
                        }
                        else return IRC.NO_FILEPATH;
                    }
                    break;

                case "ED":
                    {
                        if (MoveToNextInstruction() != IRC.NO_ERROR)
                            return IRC.NO_IDENTIFIER;

                        if (Code[CodePointer] == openingBracketCharacter) // If there are brackets.
                        {
                            int supportingIndex = CodePointer; // Needed for checking error PROCEDURE_IS_NOT_DEFINED.

                            IRC getNamesResult = GetStringsBetweenBrackets(out List<string> selectedProcedures);
                            if (getNamesResult != IRC.NO_ERROR)
                                return getNamesResult;

                            foreach (string procedureName in selectedProcedures)
                            {
                                supportingIndex++;
                                if (!ProcedureToCode.ContainsKey(procedureName))
                                {
                                    CodePointer = supportingIndex;
                                    return IRC.PROCEDURE_IS_NOT_DEFINED;
                                }

                            }

                            if (actMode)
                                ProceduresToBeEdited.AddRange(selectedProcedures);
                        }
                        else // otherwise, there must be a single name.
                        {
                            string procedureName = Code[CodePointer];
                            if (ProcedureToCode.ContainsKey(procedureName))
                            {
                                if (actMode)
                                    ProceduresToBeEdited.Add(procedureName);
                            }
                            else return IRC.PROCEDURE_IS_NOT_DEFINED;
                        }
                    }
                    break;

                case "EACH":
                    {
                        if (MoveToNextInstruction() != IRC.NO_ERROR)
                            return IRC.NO_BRACKETS;

                        if (Code[CodePointer] != openingBracketCharacter)
                            return IRC.NO_BRACKETS;

                        List<int> orgActiveTurtles = new List<int>(activeTurtles);
                        int numberOfTurtles = orgActiveTurtles.Count;
                        activeTurtles = ActiveGraphic.Turtles.Keys.ToList();
                        int startingPosition = CodePointer; // For returning to starting position after each repetition.

                        for (int i = 0; i < numberOfTurtles; i++)
                        {
                            while (true)
                            {
                                if (MoveToNextInstruction() != IRC.NO_ERROR)
                                    return IRC.NO_CLOSING_BRACKET;

                                if (Code[CodePointer] == closingBracketCharacter)
                                    break;
                                else
                                {
                                    IRC interpreterResult = CommandInterpreter(actMode);
                                    if (interpreterResult != IRC.NO_ERROR)
                                    {
                                        // Something went wrong, so we return to initial selected turtles.
                                        if (actMode)
                                            activeTurtles = orgActiveTurtles;
                                        return interpreterResult;
                                    }

                                }
                            }
                            // Move back to the initial state.
                            if (actMode)
                                activeTurtles = orgActiveTurtles;

                            if (i != numberOfTurtles - 1) // Return to beginning every time, except the final iteration.
                                CodePointer = startingPosition;
                        }
                    }
                    break;

                default:
                    {
                        string unknownCommand = Code[CodePointer];
                        if (ProcedureToCode.ContainsKey(unknownCommand))
                        {
                            string procedureName = unknownCommand;
                            int procedureStarIndex = CodePointer;

                            // Assign provided values to the corresponding parameter names.
                            Dictionary<string, int> parameterToValue = new Dictionary<string, int>();
                            int numberOfParameters = ProcedureToParams[procedureName].Count;
                            for (int i = 0; i < numberOfParameters; i++)
                            {
                                if (MoveToNextInstruction() == IRC.NO_ERROR)
                                {
                                    IRC ParamterResult = ProcedureParamValueHandler(out int number);
                                    if (ParamterResult == IRC.NO_ERROR)
                                        parameterToValue[ProcedureToParams[procedureName][i]] = number;
                                    else return ParamterResult;
                                }
                                else return IRC.NO_PARAMETER_PROVIDED;
                            }

                            // Replacing parameter names with corresponding values in code.
                            List<string> tempCode = new List<string>(ProcedureToCode[procedureName]);
                            for (int i = 0; i < tempCode.Count; i++)
                            {
                                foreach (string paramName in ProcedureToParams[procedureName])
                                    if (tempCode[i] == paramName)
                                    {
                                        string a = parameterToValue[paramName].ToString();
                                        tempCode[i] = a;
                                    }
                            }

                            Code.RemoveRange(procedureStarIndex, numberOfParameters + 1);
                            Code.InsertRange(procedureStarIndex, tempCode);
                            // The value will be increased in the main loop by one, we want to stay at the procedure start index, so we decrement by one.
                            CodePointer = procedureStarIndex - 1;
                        }
                        else return IRC.UNK_COMMAND;
                    }
                    break;
            }
            return IRC.NO_ERROR;
        }

        /// <summary>
        /// Handles the argument for a command. Checks if the first argument exists, and then checks if it is a number.
        /// </summary>
        /// <param name="number">Stores the first argument if it exists, otherwise stores 0.</param>
        /// <returns>An interpreter return code (IRC). If returns NO_ERROR, the first argument is valid.</returns>
        private IRC NumberArgHandler(out int number)
        {
            number = 0;

            if (CodePointer >= Code.Count) return IRC.POINTER_OUT_OF_RANGE;

            if (!int.TryParse(Code[CodePointer], out number))
                return IRC.NO_NUMBER;

            return IRC.NO_ERROR;
        }

        /// <summary>
        /// Handles the procedure name. Checks if the name exists, and then checks if it is not a primitve.
        /// </summary>
        /// <param name="name">Stores the procedure name if it exists, otherwise stores an empty string.</param>
        /// <returns>An interpreter return code (IRC). If returns NO_ERROR, the procedure name is valid.</returns>
        private IRC ProcedureNameHandler(out string name)
        {
            name = "";

            if (CodePointer >= Code.Count)
                return IRC.POINTER_OUT_OF_RANGE;

            if (int.TryParse(Code[CodePointer], out _))
                return IRC.PRIMITIVE_REDEFINED;

            foreach (string primitve in primitives)
                if (Code[CodePointer] == primitve)
                    return IRC.PRIMITIVE_REDEFINED;

            name = Code[CodePointer];

            return IRC.NO_ERROR;
        }

        /// <summary>
        /// Handles provided value of a parameter. Checks if the parameter exists, and then checks if it is a number.
        /// </summary>
        /// <param name="number">Stores the value of a parameter if it exists, otherwise stores 0.</param>
        /// <returns>An interpreter return code (IRC). If returns NO_ERROR, the value is valid.</returns>
        private IRC ProcedureParamValueHandler(out int number)
        {
            number = 0;

            if (CodePointer >= Code.Count) return IRC.POINTER_OUT_OF_RANGE;

            if (!int.TryParse(Code[CodePointer], out number))
                return IRC.NO_PARAMETER_PROVIDED;

            return IRC.NO_ERROR;
        }

        /// <summary>
        /// Moves the code pointer forward by one and checks if the instruction it points to exists.
        /// </summary>
        /// <returns>An interpreter return code (IRC). If returns NO_ERROR, the next instruction exists and is pointed to.</returns>
        private IRC MoveToNextInstruction()
        {
            CodePointer++; // The main loop will decrement it. So we will be still at the last successful instruction.
            if (CodePointer >= Code.Count) return IRC.POINTER_OUT_OF_RANGE;
            return IRC.NO_ERROR;
        }

        /// <summary>
        /// Gets all the numbers all the way to the first closing bracket character. Used for ASK and TELL command.
        /// </summary>
        /// <param name="numbers">Retrieved numbers.</param>
        /// <returns>An interpreter return code (IRC). If returns NO_ERROR, the syntax is valid and numbers are retrieved.</returns>
        private IRC GetNumbersBetweenBrackets(out List<int> numbers)
        {
            numbers = new List<int>();

            if (MoveToNextInstruction() != IRC.NO_ERROR)
                return IRC.NO_CLOSING_BRACKET;

            if (Code[CodePointer] == closingBracketCharacter) // If no number is present.
                return IRC.NO_NUMBER;

            while (Code[CodePointer] != closingBracketCharacter)
            {
                IRC interpreterResult = NumberArgHandler(out int providedNumber);
                if (interpreterResult == IRC.NO_ERROR)
                    numbers.Add(providedNumber);
                else return interpreterResult;

                if (MoveToNextInstruction() != IRC.NO_ERROR)
                    return IRC.NO_CLOSING_BRACKET;
            }

            return IRC.NO_ERROR;
        }

        /// <summary>
        /// Gets all strings all the way to the first closing bracket character. Used for ASK and TELL command.
        /// </summary>
        /// <param name="names">Retrieved strings.</param>
        /// <returns>An interpreter return code (IRC). If returns NO_ERROR, the syntax is valid and strings are retrieved.</returns>
        private IRC GetStringsBetweenBrackets(out List<string> names)
        {
            names = new List<string>();

            if (MoveToNextInstruction() != IRC.NO_ERROR)
                return IRC.NO_CLOSING_BRACKET;

            if (Code[CodePointer] == closingBracketCharacter) // If no string is present.
                return IRC.NO_IDENTIFIER;

            while (Code[CodePointer] != closingBracketCharacter)
            {
                names.Add(Code[CodePointer]);

                if (MoveToNextInstruction() != IRC.NO_ERROR)
                    return IRC.NO_CLOSING_BRACKET;
            }

            return IRC.NO_ERROR;
        }
    }
}
