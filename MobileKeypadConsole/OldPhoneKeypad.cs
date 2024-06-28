using System;
using System.Timers;
using Timer = System.Timers.Timer;

namespace MobileKeypadConsole
{
	public class OldPhoneKeypad
	{
        static Timer timer;
        static string result = "";
        static int count = 0;
        static string prevChar = " ";
        
        private static readonly Dictionary<string, string> keypad = new Dictionary<string, string>
        {
            {"2", "ABC"},
            {"3", "DEF"},
            {"4", "GHI"},
            {"5", "JKL"},
            {"6", "MNO"},
            {"7", "PQRS"},
            {"8", "TUV"},
            {"9", "WXYZ"},
            {"0", " "},
            {"1", "."},
        };

        public OldPhoneKeypad()
        {
            timer = new Timer(1000);
            timer.Elapsed += OnTimedEvent;
        }

        public static string ProcessInput(string key)
        {
            string output = "";
            if (key == "#")
            {
                if (count > 0 && prevChar != " ")
                {
                    result += GetCharacterFromKeypad(prevChar, count);
                }
                output = result;
                Reset();
                return output;
            }
            else if (key == "*")
            {
                if (result.Length > 0)
                {
                    result = result.Substring(0, result.Length - 1);
                    Console.Write("\b \b"); // Simulate backspace in console
                }
                prevChar = " ";
                count = 0;
                timer.Stop();
            }
            else if (key == " ")
            {
                timer.Stop();
                if (count > 0 && prevChar != " ")
                {
                    result += GetCharacterFromKeypad(prevChar, count);
                    prevChar = " ";
                    count = 0;
                }
            }
            else if (keypad.ContainsKey(key))
            {
                HandleKeyInput(key);
            }
            return output;
        }

        private static void HandleKeyInput(string key)
        {
            timer.Stop();
            if (key == prevChar)
            {
                count++;
            }
            else
            {
                if (count > 0 && prevChar != " ")
                {
                    result += GetCharacterFromKeypad(prevChar, count);
                }
                prevChar = key;
                count = 1;
            }
            timer.Start();
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            timer.Stop();
            if (count > 0 && prevChar != " ")
            {
                result += GetCharacterFromKeypad(prevChar, count);
                prevChar = " ";
                count = 0;
            }
        }

        private static string GetCharacterFromKeypad(string key, int count)
        {
            string letters = keypad[key];
            return letters[(count - 1) % letters.Length].ToString();
        }

        private static void Reset()
        {
            result = "";
            count = 0;
            prevChar = " ";
        }
    }
}
