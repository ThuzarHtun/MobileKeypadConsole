using System;
using System.Collections.Generic;
using System.Timers;
using MobileKeypadConsole;
using Timer = System.Timers.Timer;

class Program
{
    static void Main()
    {
        var oldPhoneKeypad = new OldPhoneKeypad();
        
        Console.WriteLine("Enter the sequence of numbers (e.g., 2 for A, 22 for B, etc.):");

        while (true)
        {
            string key = Console.ReadKey(true).KeyChar.ToString();
            var result = OldPhonePad(key);
            if (!string.IsNullOrEmpty(result))
            {
                Console.WriteLine(result);
            }
        }
    }

    public static string OldPhonePad(string input)
    {
        return OldPhoneKeypad.ProcessInput(input);
    }
}
