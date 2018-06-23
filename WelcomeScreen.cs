// Francisco Sabater Villora

using System;
using System.Threading;

public class WelcomeScreen
{
    public void Run()
    {
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.Clear();
        Console.SetCursorPosition(30, 5);
        Console.Write("C O N S O L E");
        Console.SetCursorPosition(40, 6);
        Console.Write("T R I V I A L (_)_)");
        Console.SetCursorPosition(30, 9);
        Console.Write("Version: 0.05 (Jun. 18)");
        Console.SetCursorPosition(30, 10);
        Console.Write("Fran Sabater");

        DateTime auxDate = DateTime.Now;
        bool exit = false;
        while(!exit)
        {
            
            exit = Console.KeyAvailable || 
                DateTime.Now.Subtract(auxDate).Seconds > 5;
        }  
    }
}