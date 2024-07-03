using System;
using System.Collections.Generic;
using System.Timers;
using MobileKeypadConsole;
using MobileKeypadConsole.Services;
using MobileKeypadConsole.Services.Interfaces;
using Timer = System.Timers.Timer;

class Program
{
    static IOldPhoneKeypad oldPhoneKeypad;
    
    static void Main()
    {
        try
        {
            Console.WriteLine("Do you want to perform non-interactive checking? (yes/no)");
            string response = Console.ReadLine();

            bool nonInteractive = response.Equals("yes", StringComparison.OrdinalIgnoreCase);

            oldPhoneKeypad = new OldPhoneKeypad(enableTimer: !nonInteractive);

            if (nonInteractive)
            {
                NonInteractiveMode();
            }
            else
            {
                InteractiveMode();
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine("Something went wrong. Please contact support!");
            Console.ReadLine();
        }
    }

    private static void NonInteractiveMode()
    {
        string input = "222 2 22#";
        string output = OldPhonePad(input);
        Console.WriteLine($"Input: \"{input}\" -> Output: \"{output}\"");  // Output: "Input: "222 2 22" -> Output: "CAB"

        string input1 = "33#";
        string output1 = OldPhonePad(input1);
        Console.WriteLine($"Input: \"{input1}\" -> Output: \"{output1}\"");  // Output: "Input: "33" -> Output: "E"

        string input2 = "227*#";
        string output2 = OldPhonePad(input2);
        Console.WriteLine($"Input: \"{input2}\" -> Output: \"{output2}\"");  // Output: "Input: "227*" -> Output: "B"

        string input3 = "4433555 555666#";
        string output3 = OldPhonePad(input3);
        Console.WriteLine($"Input: \"{input3}\" -> Output: \"{output3}\"");  // Output: "Input: "4433555 555666" -> Output: "HELLO"

        string input4 = "8 88777444666*664#";
        string output4 = OldPhonePad(input4);
        Console.WriteLine($"Input: \"{input4}\" -> Output: \"{output4}\"");  // Output: "Input: "8 88777444666*664" -> Output: "TURING"

        string input5 = "8&#";
        string output5 = OldPhonePad(input5);
        Console.WriteLine($"Input: \"{input5}\" -> Output: \"{output5}\"");  // Output: "Input: "8&" -> Output: "T"

        string input6 = "1 11#";
        string output6 = OldPhonePad(input6);
        Console.WriteLine($"Input: \"{input6}\" -> Output: \"{output6}\"");  // Output: "Input: "1 11" -> Output: "&'"


        Console.ReadLine();
    }

    private static void InteractiveMode()
    {
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
        return oldPhoneKeypad.GetLetters(input);
    }
}
