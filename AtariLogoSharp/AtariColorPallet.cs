/* Atari Logo Sharp - implementation of Atari Logo in C#. Developed by 404 team. */

using System.Collections.Generic;
using System.Drawing;

namespace AtariLogoSharp
{
    /// <summary>
    /// Designed to convert numbers to the colors of Atari 8 bits NTSC palette.
    /// </summary>
    class AtariColorPallet
    {
        /// <summary>
        /// Numbers (from 0 to 127) corresponding to the colors (hex rrggbb without hash) of Atari 8 bits NTSC palette.
        /// Based on: https://commons.wikimedia.org/wiki/File:Atari_CTIA_%26_TIA_NTSC_palette.png
        /// </summary>
        private static readonly Dictionary<uint, string> numberToColor = new Dictionary<uint, string>()
        {
            { 0, "000000" }, { 1, "404040" }, { 2, "6c6c6c" }, { 3, "909090" }, { 4, "b0b0b0" }, { 5, "c8c8c8" }, { 6, "dcdcdc" }, { 7, "ececec" },
            { 8, "444400" }, { 9, "646410" }, { 10, "848424" }, { 11, "a0a034" }, { 12, "b8b840" }, { 13, "d0d050" }, { 14, "e8e85c" }, { 15, "fcfc68" },
            { 16, "702800" }, { 17, "844414" }, { 18, "985c28" }, { 19, "ac783c" }, { 20, "bc8c4c" }, { 21, "cca05c" }, { 22, "dcb468" }, { 23, "ecc878" },
            { 24, "841800" }, { 25, "983418" }, { 26, "ac5030" }, { 27, "c06848" }, { 28, "d0805c" }, { 29, "e09470" }, { 30, "eca880" }, { 31, "fcbc94" },
            { 32, "880000" }, { 33, "983418" }, { 34, "b03c3c" }, { 35, "c05858" }, { 36, "d07070" }, { 37, "e08888" }, { 38, "eca0a0" }, { 39, "fcb4b4" },
            { 40, "78005c" }, { 41, "8c2074" }, { 42, "a03c88" }, { 43, "b0589c" }, { 44, "c070b0" }, { 45, "d084c0" }, { 46, "dc9cd0" }, { 47, "ecb0e0" },
            { 48, "480078" }, { 49, "602090" }, { 50, "783ca4" }, { 51, "8c58b8" }, { 52, "a070cc" }, { 53, "b484dc" }, { 54, "c49cec" }, { 55, "d4b0fc" },
            { 56, "140084" }, { 57, "302098" }, { 58, "4c3cac" }, { 59, "6858c0" }, { 60, "7c70d0" }, { 61, "9488e0" }, { 62, "a8a0ec" }, { 63, "bcb4fc" },
            { 64, "000088" }, { 65, "1c209c" }, { 66, "3840b0" }, { 67, "505cc0" }, { 68, "6874d0" }, { 69, "7c8ce0" }, { 70, "90a4ec" }, { 71, "a4b8fc" },
            { 72, "00187c" }, { 73, "1c3890" }, { 74, "3854a8" }, { 75, "5070bc" }, { 76, "6888cc" }, { 77, "7c9cdc" }, { 78, "90b4ec" }, { 79, "a4c8fc" },
            { 80, "002c5c" }, { 81, "1c4c78" }, { 82, "386890" }, { 83, "5084ac" }, { 84, "689cc0" }, { 85, "7cb4d4" }, { 86, "90cce8" }, { 87, "a4e0fc" },
            { 88, "003c2c" }, { 89, "1c5c48" }, { 90, "387c64" }, { 91, "509c80" }, { 92, "68b494" }, { 93, "7cd0ac" }, { 94, "90e4c0" }, { 95, "a4fcd4" },
            { 96, "003c00" }, { 97, "205c20" }, { 98, "407c40" }, { 99, "5c9c5c" }, { 100, "74b474" }, { 101, "8cd08c" }, { 102, "a4e4a4" }, { 103, "b8fcb8" },
            { 104, "143800" }, { 105, "345c1c" }, { 106, "507c38" }, { 107, "6c9850" }, { 108, "84b468" }, { 109, "9ccc7c" }, { 110, "b4e490" }, { 111, "c8fca4" },
            { 112, "2c3000" }, { 113, "4c501c" }, { 114, "687034" }, { 115, "848c4c" }, { 116, "9ca864" }, { 117, "b4c078" }, { 118, "ccd488" }, { 119, "e0ec9c" },
            { 120, "442800" }, { 121, "644818" }, { 122, "846830" }, { 123, "a08444" }, { 124, "b89c58" }, { 125, "d0b46c" }, { 126, "e8cc7c" }, { 127, "fce08c" },
        };

        /// <summary>
        /// Returns color corresponding to the specified number.
        /// </summary>
        /// <param name="index">Number of color.</param>
        /// <returns>Corresponding color, if index is bigger than 127, returns Black.</returns>
        public static Color GetColor(uint index)
        {
            if (index < 128)
            {
                return ColorTranslator.FromHtml("#" + numberToColor[index]);
            }
            return Color.Black;
        }
    }
}
