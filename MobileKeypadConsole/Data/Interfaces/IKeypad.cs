using System;
namespace MobileKeypadConsole.Data.Interfaces
{
    public interface IKeypad
    {
        bool IsContainKey(char key);
        string GetCharacter(char key, int count);
    }
}

