using System;
using System.Timers;
using MobileKeypadConsole.Data;
using MobileKeypadConsole.Data.Interfaces;
using MobileKeypadConsole.Extensions;
using MobileKeypadConsole.Services.Interfaces;
using Timer = System.Timers.Timer;

namespace MobileKeypadConsole.Services
{
    public class OldPhoneKeypad : IOldPhoneKeypad
    {
        private Timer? timer;
        private bool isTimerEnabled = false;
        private string result = "";
        private int count = 0;
        private char prevChar = ' ';
        private readonly int duration = 1000;    // in millisecond
        private IKeypad keypad;

        public OldPhoneKeypad(bool enableTimer = false)
        {
            if (enableTimer)
                InitializeTimer();

            keypad = new Keypad();
        }

        private void InitializeTimer()
        {
            isTimerEnabled = true;
            timer = new Timer(duration);
            timer.Elapsed += OnTimedEvent;
        }

        public string GetLetters(string keys)
        {
            if (string.IsNullOrEmpty(keys))
                return "";

            string output = "";
            foreach (var c in keys)
            {
                output += ProcessInput(c);
            }
            return output;
        }

        private string ProcessInput(char key)
        {
            string output = "";
            if (key == '#')
            {
                if (count > 0 && prevChar != ' ')
                {
                    result += keypad.GetCharacter(prevChar, count);
                }
                output = result;
                Reset();
                return output;
            }
            else if (key == '*')
            {
                if (result.Length > 0)
                {
                    if (count > 0 && prevChar != ' ')
                    {
                        result += keypad.GetCharacter(prevChar, count);
                        prevChar = ' ';
                        count = 0;
                    }

                    result = result.Substring(0, result.Length - 1);
                    Console.Write("\b \b"); // Simulate backspace in console
                }
                prevChar = ' ';
                count = 0;

                timer.ToggleTimer(start: false);
            }
            else if (key == ' ')
            {
                timer.ToggleTimer(start: false);
                if (count > 0 && prevChar != ' ')
                {
                    result += keypad.GetCharacter(prevChar, count);
                    prevChar = ' ';
                    count = 0;
                }
            }
            else if (keypad.IsContainKey(key))
            {
                HandleKeyInput(key);
            }
            return output;
        }

        private void HandleKeyInput(char key)
        {
            timer.ToggleTimer(start: false);
            if (key == prevChar)
            {
                count++;
            }
            else
            {
                if (count > 0 && prevChar != ' ')
                {
                    result += keypad.GetCharacter(prevChar, count);
                }
                prevChar = key;
                count = 1;
            }
            timer.ToggleTimer(start: true);
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            timer.ToggleTimer(start: false);
            if (count > 0 && prevChar != ' ')
            {
                result += keypad.GetCharacter(prevChar, count);
                prevChar = ' ';
                count = 0;
            }
        }

        private void Reset()
        {
            result = "";
            count = 0;
            prevChar = ' ';
        }
    }
}

