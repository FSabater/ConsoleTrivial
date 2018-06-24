// Francisco Sabater Villora
//

using System;
using System.Collections.Generic;

public class StatsDisplay
{
    public static void ShowStats(List<Player> playersList)
    {
        int x = 80;
        int y = 2;

        foreach (Player p in playersList)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("{0} - ", p.Name);

            Console.Write("PR: {0}/{1}",
                p.AttemptedQuestions["PR"],
                p.AcceptedQuestions["PR"]);

            Console.SetCursorPosition(x+4, ++y);
            Console.WriteLine("DB: {0}/{1}",
                p.AttemptedQuestions["DB"],
                p.AcceptedQuestions["DB"]);

            Console.SetCursorPosition(x+4, ++y);
            Console.WriteLine("SY: {0}/{1}",
                p.AttemptedQuestions["SY"],
                p.AcceptedQuestions["SY"]);

            Console.SetCursorPosition(x+4, ++y);
            Console.WriteLine("WB: {0}/{1}",
                p.AttemptedQuestions["WB"],
                p.AcceptedQuestions["WB"]);

            y += 2;
        }
    }
}
