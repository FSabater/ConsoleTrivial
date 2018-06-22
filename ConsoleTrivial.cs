// Francisco Sabater Villora

// Version 0.01: Load and answer questions
// Version 0.02: Board and movement

using System;
using System.IO;
using System.Collections.Generic;

public struct Question
{
    public string text;
    public string category;
    public string answer1;
    public string answer2;
    public string answer3;
    public string answer4;
    public ushort correct;
}

public struct Player
{
    public string name;
    public ushort score; 
}

public struct Square
{
    public int x;
    public int y;
    public string category;
}

class ConsoleTrivial
{
    public static String[] ReadFile(string fileName)
    {
        try
        {
            return File.ReadAllLines(fileName);
        }
        catch (PathTooLongException)
        {
            Console.WriteLine("Path too long");
            String[] error = new string[0];
            return error;
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not accessible");
            String[] error = new string[0];
            return error;
        }
        catch (IOException e)
        {
            Console.WriteLine("I/O error: " + e.Message);
            String[] error = new string[0];
            return error;
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
            String[] error = new string[0];
            return error;
        }
    }

    public static void PlayersSelect(ref ushort totalPlayers,
        ref List<Player> playersList)
    {
        bool validNumPlayers = false;
        while (!validNumPlayers)
        {
            Console.WriteLine("How many players? choose between 2 - 4");
            string input = Console.ReadLine();

            switch (input)
            {
                case "2":
                case "3":
                case "4":
                    totalPlayers = Convert.ToUInt16(input);
                    validNumPlayers = true;
                    break;
                default:
                    Console.WriteLine("Invalid input, choose a valid option");
                    break;
            }
        }

        for (ushort i = 0; i < totalPlayers; i++)
        {
            Console.Write("Enter player {0} name: ", i + 1);
            string nameAux = Console.ReadLine();
            Player aux = new Player();
            aux.name = nameAux;
            aux.score = 0;
            playersList.Add(aux);
        }
    }

    public static void QuestionShow(ref ushort points,
        string[] allFileLines)
    {
        Random rnd = new Random();
        int questionNumber = rnd.Next(0, allFileLines.Length);
        string line = allFileLines[questionNumber];
        Question actualQuestion = new Question();
        string[] questionArray = line.Split('#');
        actualQuestion.category = questionArray[0];
        actualQuestion.text = questionArray[1];
        actualQuestion.answer1 = questionArray[2];
        actualQuestion.answer2 = questionArray[3];
        actualQuestion.answer3 = questionArray[4];
        actualQuestion.answer4 = questionArray[5];
        actualQuestion.correct = Convert.ToUInt16(questionArray[6]);

        Console.WriteLine(actualQuestion.text);
        Console.WriteLine(actualQuestion.answer1);
        Console.WriteLine(actualQuestion.answer2);
        Console.WriteLine(actualQuestion.answer3);
        Console.WriteLine(actualQuestion.answer4);
        Console.Write("Enter the question number: ");

        string userInput = Console.ReadLine();
        if (userInput == Convert.ToString(actualQuestion.correct))
        {
            Console.WriteLine("Correct!");
            points += 1; 
        }
        else
        {
            Console.WriteLine("Incorrect!");
        }

        Console.WriteLine("Press Enter to end turn");
        Console.ReadLine();
    }

    public static void ShowScores(List<Player> playersList)
    {
        foreach (Player p in playersList)
        {
            Console.Write("{0} - ",
                p.name);
            Console.WriteLine("Score: {0}",
                p.score);
        }
    }

    public static void GameStart()
    {
        string fileName = "txt/questions.txt";
        string[] allFileLines = ReadFile(fileName);
        List<Player> playersList = new List<Player>();

        bool exit = false;
        ushort totalPlayers = 0;

        // players select
        PlayersSelect(ref totalPlayers, ref playersList);


        // Game Loop
        ushort actualPlayer = 0;
        do
        {
            Console.Clear();
            ShowScores(playersList);

            Console.WriteLine();
            Console.WriteLine("{0} playing... ",
                playersList[actualPlayer].name);
            ushort actualScore = playersList[actualPlayer].score;
            // Question Show
            QuestionShow(ref actualScore, allFileLines);

            /*  The next code is for update the player score
                            Alert !
                Change the next lines of code if the structcs remove
            */
            Player aux = new Player();
            aux.name = playersList[actualPlayer].name;
            aux.score = actualScore;
            playersList[actualPlayer] = aux;

            if (actualScore == 20)
            {
                Console.Clear();
                Console.WriteLine("{0} wins!",
                    playersList[actualPlayer].name);
                Console.WriteLine("Results: ");
                ShowScores(playersList);
                Console.WriteLine("Press Enter for exit");
                Console.ReadLine();
                exit = true;
            }

            actualPlayer++;
            if (actualPlayer == totalPlayers)
            {
                actualPlayer = 0;
            }
        } while (!exit);
    }

    public static Square[] CreateBoard()
    {
        String[] categories = { "SY", "WB", "PR", "SY", "DB", "WB", "PR",
            "DB", "PR", "SY", "DB", "PR", "WB", "DB",
            "PR", "DB", "SY", "WB", "DB", "PR", "SY",
            "WB", "SY", "WB", "DB", "SY", "PR", "WB"};
        const int ARRAYSIZE = 28;
        Square[] squareArray = new Square[ARRAYSIZE];
        int squareHeight = 4;
        int squareWidth = 10;
        int actualX = 0;
        int actualY = 0;

        for(int i = 0; i < ARRAYSIZE; i++)
        {
            // Create a aux Square for save all of the board squares
            Square aux = new Square();

            // up of the board
            if (i < ARRAYSIZE / 4)
            {
                aux.category = categories[i];
                aux.x = actualX;
                aux.y = actualY;
                actualX += squareWidth;
            }
            // right of the board
            else if (i < ARRAYSIZE / 2)
            {
                aux.category = categories[i];
                aux.x = actualX;
                aux.y = actualY;
                actualY += squareHeight;
            }
            // down of the board
            else if (i < (ARRAYSIZE - ARRAYSIZE / 4))
            {
                aux.category = categories[i];
                aux.x = actualX;
                aux.y = actualY;
                actualX -= squareWidth;
            }
            // left of the board
            else if (i < ARRAYSIZE)
            {
                aux.category = categories[i];
                aux.x = actualX;
                aux.y = actualY;
                actualY -= squareHeight;
            }

            squareArray[i] = aux;
        }
        return squareArray;
    }

    public static void DrawBoard(Square[] squareArray)
    {
        /*   ________ 
         *  |   DB   |
         *  |        |
         *  |________|
         * 
         */

        for (int i = 0; i < squareArray.Length; i++)
        {
            Square aux = squareArray[i];
            Console.SetCursorPosition(aux.x, aux.y);
            Console.Write(" ________ ");
            Console.SetCursorPosition(aux.x, aux.y+1);
            Console.Write("|   {0}   |", aux.category);
            Console.SetCursorPosition(aux.x, aux.y+2);
            Console.Write("|        |");
            Console.SetCursorPosition(aux.x, aux.y+3);
            Console.Write("|________|");
        }
    }

    public static int Dice()
    {
        Random rnd = new Random();
        return rnd.Next(1, 6);
    }

    public static void DrawPlayer(int x, int y)
    {
        Console.SetCursorPosition(x + 5, y + 2); // 5 and 2 for centrate the player
        Console.Write("X");
    }

    static void Main(string[] args)
    {
        bool exit = false;
        int actualPosition = 0;
        Square[] squareArray = CreateBoard();

        while(!exit)
        {
            DrawBoard(squareArray);
            DrawPlayer(squareArray[actualPosition].x,
                squareArray[actualPosition].y);
            Console.SetCursorPosition(0, 32);

            int dice = Dice();
            Console.WriteLine("Dice: {0}", dice);
            actualPosition += dice;
            if(actualPosition > 27) // 28 squares, start by 0
            {
                actualPosition = actualPosition - 28;
            }
            Console.WriteLine("Press Enter for another try");
            Console.ReadLine();
        }
    }
}

