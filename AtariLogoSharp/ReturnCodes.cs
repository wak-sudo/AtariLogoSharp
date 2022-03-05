/* Atari Logo Sharp - implementation of Atari Logo in C#. Developed by 404 team. */

using System.Collections.Generic;

namespace AtariLogoSharp
{
    /// <summary>
    /// Interpreter return codes. They provide information about the interpretation status.
    /// </summary>
    public enum IRC : uint
    {
        NO_ERROR,
        UNK_COMMAND,
        NO_BRACKETS,
        NO_CLOSING_BRACKET,
        NO_NUMBER,
        NO_IDENTIFIER,
        NO_PROCEDURE_END,
        DECLARATION_WITHIN_PROCEDURE,
        PRIMITIVE_REDEFINED,
        NO_PARAMETER_PROVIDED,
        PARAMETER_REPEATED,
        NO_FILEPATH,
        INVALID_FILE,
        FAILED_SAVING,
        FAILED_LOADING,
        COLOR_OUT_OF_RANGE,
        PEN_INDEX_OUT_OF_RANGE,
        PROCEDURE_IS_NOT_DEFINED,
        POINTER_OUT_OF_RANGE, // Technial error.
    };

    public partial class Interpreter
    {
        /// <summary>
        /// Stores info messages connected to interpreter return codes.
        /// </summary>
        public static Dictionary<IRC, string> ReturnCodesDictionary { get; } = new Dictionary<IRC, string>()
        {
            {IRC.NO_ERROR, "Everything is fine." },
            {IRC.UNK_COMMAND, "Unknown command: {0}"},
            {IRC.NO_BRACKETS, "There must be brackets after: {0}"},
            {IRC.NO_CLOSING_BRACKET, "There must be a closing bracket after: {0}"},
            {IRC.NO_NUMBER, "There is no number provided after: {0}"},
            {IRC.NO_IDENTIFIER, "There must be an identifier after: {0}" },
            {IRC.NO_PROCEDURE_END, "There is no END clause after: {0}" },
            {IRC.DECLARATION_WITHIN_PROCEDURE, "Declaration of procedure within procedure is not allowed, after: {0}" },
            {IRC.PRIMITIVE_REDEFINED, "Redefinition of primitive {0} is prohibited." },
            {IRC.NO_PARAMETER_PROVIDED, "There must be another numeric parameter provided after: {0}" },
            {IRC.POINTER_OUT_OF_RANGE, "Interpreter error: code pointer out of range." },
            {IRC.PARAMETER_REPEATED, "There is a repeated parameter name after: {0}" },
            {IRC.NO_FILEPATH, "There must be a valid file path after: {0}" },
            {IRC.INVALID_FILE, "File provided after {0} contains syntax errors." },
            {IRC.FAILED_SAVING, "Saving of {0} failed, check accessibility of your file path." },
            {IRC.FAILED_LOADING, "Cannot load {0}, check accessibility of your file." },
            {IRC.COLOR_OUT_OF_RANGE, "Color number after {0} must be in range of [0; 127]." },
            {IRC.PEN_INDEX_OUT_OF_RANGE, "Specified pen ID after {0} must be in range of [0; 2]." },
            {IRC.PROCEDURE_IS_NOT_DEFINED, "Procedure after {0} is not defined." },
        };
    }
}
