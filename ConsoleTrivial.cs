// Francisco Sabater Villora

// Version 0.01: Load and answer questions
// Version 0.02: Board and movement
// Version 0.03: Choosing a square
// Version 0.04: Real game, part 1
// Version 0.05: Classes structure

using System;
using System.IO;
using System.Collections.Generic;

public struct Square
{
    public int x;
    public int y;
    public string category;
}

public class ConsoleTrivial
{
    private static WelcomeScreen welcomeScreen;

    public void Run()
    {
        welcomeScreen = new WelcomeScreen();
        welcomeScreen.Run();
        Game game = new Game();
        game.Run();
    }

    public static void Main(string[] args)
    {
        ConsoleTrivial consoleTrivial = new ConsoleTrivial();
        consoleTrivial.Run();
        
    }
}