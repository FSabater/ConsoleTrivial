// Francisco Sabater Villora

using System;
using System.Collections.Generic;

public class Player
{
    public string Name { get; set; }
    public int Position { get; set; }
    public ConsoleColor Color { get; set; }
    public Dictionary<string, int> AttemptedQuestions { get; set; }
    public Dictionary<string, int> AcceptedQuestions { get; set; }
    public int DrawAjustX;
    public int DrawAjustY;


    public Player(string name, ConsoleColor color, int drawAjustX,
        int drawAjustY)
    {
        AttemptedQuestions = new Dictionary<string, int>();
        AcceptedQuestions = new Dictionary<string, int>();

        Name = name;
        Color = color;
        DrawAjustX = drawAjustX;
        DrawAjustY = drawAjustY;
        Position = 0;

        AttemptedQuestions.Add("PR", 0);
        AttemptedQuestions.Add("DB", 0);
        AttemptedQuestions.Add("SY", 0);
        AttemptedQuestions.Add("WB", 0);

        AcceptedQuestions.Add("PR", 0);
        AcceptedQuestions.Add("DB", 0);
        AcceptedQuestions.Add("SY", 0);
        AcceptedQuestions.Add("WB", 0);
    }

    public void DrawPlayer(int x, int y)
    {
        Console.SetCursorPosition(x + DrawAjustX, y + DrawAjustY);
        Console.BackgroundColor = Color;
        Console.Write(" ");
        Console.BackgroundColor = ConsoleColor.Blue;
    }
}

