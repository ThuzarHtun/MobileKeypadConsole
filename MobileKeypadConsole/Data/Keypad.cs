using MobileKeypadConsole.Data.Interfaces;

namespace MobileKeypadConsole.Data
{
    public class Keypad : IKeypad
    {
        private readonly Dictionary<char, string> keypadLetter = new Dictionary<char, string>
        {
            {'0', " "},
            {'1', "&'("},
            {'2', "ABC"},
            {'3', "DEF"},
            {'4', "GHI"},
            {'5', "JKL"},
            {'6', "MNO"},
            {'7', "PQRS"},
            {'8', "TUV"},
            {'9', "WXYZ"}
        };

        public bool IsContainKey(char key)
        {
            return keypadLetter.ContainsKey(key);
        }

        public string GetCharacter(char key, int count)
        {
            if (!char.IsDigit(key))
                return "";

            if (count < 0 || count > 9)
                return "";

            if (!IsContainKey(key))
                return "";

            string letters = keypadLetter[key];
            return letters[(count - 1) % letters.Length].ToString();
        }
    }
}

