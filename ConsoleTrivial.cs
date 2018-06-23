// Francisco Sabater Villora

// Version 0.01: Load and answer questions
// Version 0.02: Board and movement
// Version 0.03: Choosing a square
// Version 0.04: Real game, part 1

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
    public static bool ExitGame()
    {
        Console.SetCursorPosition(11, 20);
        Console.Write("Press Enter for exit, Escape for cancel");
        if (Console.ReadKey().Key == ConsoleKey.Enter)
        {
            return true;
        }
        else
        {
            Console.SetCursorPosition(11, 20);
            Console.Write("                                                 ");
            return false;
        }
    }

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
        List<Player> playersList)
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
                    Console.WriteLine("Invalid input, "
                        + "choose a valid option");
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
        string[] allFileLines, string category)
    {
        bool exitOfBucleSearch = false;
        bool found = false;
        int count = 0;
        int firstQuestionFound = 0;
        int maxQuestionsCategory = 0;
        while (!exitOfBucleSearch)
        {
            if(allFileLines[count][0] == category[0])
            {
                if(firstQuestionFound == 0)
                {
                    firstQuestionFound = count;
                }
                else
                {
                    maxQuestionsCategory = count;
                }
                found = true;
            }

            count++;
            if (count >= allFileLines.Length-1)
            {
                exitOfBucleSearch = true;
            }
        }

        if(found)
        {
            Random rnd = new Random();
            int questionNumber = rnd.Next(firstQuestionFound,
                maxQuestionsCategory);
            string line = allFileLines[questionNumber];
            Question actualQuestion = new Question();
            string[] questionArray = line.Split('Ç');
            actualQuestion.category = questionArray[0];
            actualQuestion.text = questionArray[1];
            actualQuestion.answer1 = questionArray[2];
            actualQuestion.answer2 = questionArray[3];
            actualQuestion.answer3 = questionArray[4];
            actualQuestion.answer4 = questionArray[5];
            actualQuestion.correct = Convert.ToUInt16(questionArray[6]);

            Console.SetCursorPosition(0, 29);
            Console.WriteLine("Category: {0}", actualQuestion.category);
            Console.WriteLine(actualQuestion.text);
            Console.WriteLine(actualQuestion.answer1);
            Console.WriteLine(actualQuestion.answer2);
            Console.WriteLine(actualQuestion.answer3);
            Console.WriteLine(actualQuestion.answer4);
            Console.Write("Enter the answer number: ");
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
        else
        {
            Console.WriteLine("Error: internal error with the category");
        }
    }

    public static void ShowScores(List<Player> playersList)
    {
        int x = 11;
        int y = 5;
        
        foreach (Player p in playersList)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("{0} - ",
                p.name);
            Console.WriteLine("Score: {0}",
                p.score);
            y++;
        }
    }

    public static Square[] CreateBoard()
    {
        String[] categories = { "SY", "WB", "PR", "SY", "DB", "WB", "PR",
            "DB", "PR", "SY", "DB", "PR", "WB", "DB",
            "PR", "DB", "SY", "WB", "DB", "PR", "SY",
            "WB", "SY", "WB", "DB", "SY", "PR", "WB"};
        const int ARRAYSIZE = 28;
        Square[] squareArray = new Square[ARRAYSIZE];
        int squareHeight = 3; // original 4
        int squareWidth = 9; // original 10
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

    public static void SelectNewPosition(ref int actualPosition, int dice,
    Square[] squareArray)
    {
        int redPosition = actualPosition - dice;
        int greenPosition = actualPosition + dice;
        if (redPosition < 0) // 28 squares, start by 0
        {
            redPosition = redPosition + 28;
        }
        if (greenPosition > 27) // 28 squares, start by 0
        {
            greenPosition = greenPosition - 28;
        }

        // Print the colors

        // Red
        Console.SetCursorPosition(squareArray[redPosition].x,
            squareArray[redPosition].y + 3);
        Console.BackgroundColor = ConsoleColor.Red;
        Console.Write("|________|");

        // Green
        Console.SetCursorPosition(squareArray[greenPosition].x,
            squareArray[greenPosition].y + 3);
        Console.BackgroundColor = ConsoleColor.Green;
        Console.Write("|________|");

        // reset console color
        Console.ResetColor();

        bool validOption = false;
        do
        {
            // Text and return for the new position
            Console.SetCursorPosition(0, 25);
            Console.Write("Write 1 for go to the red mark, 2 for the green: ");
            string nextPosition = Console.ReadLine();

            if (nextPosition == "1")
            {
                Console.SetCursorPosition(0, 28);
                Console.
                    WriteLine("Red selected, going to the position");
                validOption = true;
                actualPosition = redPosition;
            }
            else if(nextPosition == "2")
            {
                Console.SetCursorPosition(0, 28);
                Console.WriteLine("Green selected, going to the position");
                validOption = true;
                actualPosition = greenPosition;
            }
            else
            {
                Console.SetCursorPosition(0, 28);
                Console.WriteLine("No position selected, select one");
                validOption = false;
            }
        } while (!validOption);
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

    public static void GameStart(string[] allFileLines)
    {
        List<Player> playersList = new List<Player>();

        bool exit = false;
        ushort totalPlayers = 0;
        ushort actualPlayer = 0;
        int actualPosition = 0;
        Square[] squareArray = CreateBoard();
        ushort actualScore = 0;

        // players select
        PlayersSelect(ref totalPlayers, playersList);

        // Game Loop
        while (!exit)
        {
            //clear and draw the board
            Console.Clear();
            actualScore = playersList[actualPlayer].score;
            ShowScores(playersList);
            DrawBoard(squareArray);
            DrawPlayer(squareArray[actualPosition].x,
                squareArray[actualPosition].y);

            Console.SetCursorPosition(11, 10);
            Console.WriteLine("{0} playing... ",
                playersList[actualPlayer].name);

            // Enter for roll the dice and exit the game
            Console.SetCursorPosition(0, 27);
            Console.WriteLine("Press Enter for roll the dice");

            if (Console.ReadKey().Key == ConsoleKey.Escape)
            {
                exit = ExitGame();
            }

            if (!exit)
            {
                Console.SetCursorPosition(0, 26);
                int dice = Dice();
                Console.WriteLine("Dice: {0}", dice);
                // SelectNewPosition,set the console cursor at the top position 28
                SelectNewPosition(ref actualPosition, dice,
                    squareArray);
            }

            if (!exit)
            {
                QuestionShow(ref actualScore, allFileLines,
                squareArray[actualPosition].category);

                Player aux = new Player();
                aux.name = playersList[actualPlayer].name;
                aux.score = actualScore;
                playersList[actualPlayer] = aux;
            }

            // Check the players score and see if the game end
            if (actualScore >= 20)
            {
                Player highScorePlayer = playersList[actualPlayer];
                bool win = true;
                foreach (Player p in playersList)
                {
                    if (p.score == highScorePlayer.score)
                    {
                        //for ignore the same player
                    }
                    else if (p.score < highScorePlayer.score - 2)
                    {
                        // for ignore the lost players
                    }
                    else
                    {
                        win = false;
                    }
                }

                if (win)
                {
                    Console.SetCursorPosition(11, 18);
                    Console.Write("{0} wins!", playersList[actualPlayer].name);
                    Console.Write(" - Game Over");
                    exit = true;
                }
                else
                {
                    Console.SetCursorPosition(11, 18);
                    Console.Write("Nobody wins! New Round Starting, press Enter");
                    if (Console.ReadKey().Key == ConsoleKey.Escape)
                    {
                        exit = ExitGame();
                    }
                    else
                    {
                        for (int i = 0; i < playersList.Count; i++)
                        {
                            Player aux = playersList[i];
                            aux.score = 0;
                            playersList[i] = aux;
                        }
                    }
                }

            }

            // Next Player turn
            if (!exit)
            {
                actualPlayer++;
                if (actualPlayer == totalPlayers)
                {
                    actualPlayer = 0;
                }
            }
        }
    }

    static void Main(string[] args)
    {
        string fileName = "bin/debug/txt/questions.txt";
        string[] allFileLines = ReadFile(fileName);
        if(allFileLines[0].Split('Ç').Length == 7)
        {
            GameStart(allFileLines);
        }
        else
        {
            Console.WriteLine("Error: File error");
        }
        
    }
}

