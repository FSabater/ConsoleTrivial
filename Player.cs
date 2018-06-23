// Francisco Sabater Villora

using System;
using System.Collections.Generic;

public class Player
{
    public string Name { get; set; }
    public int Position { get; set; }
    public ConsoleColor color { get; set; }
    public Dictionary<string, int> attemptedQuestions { get; set; }
    public Dictionary<string, int> acceptedQuestions { get; set; }
}

