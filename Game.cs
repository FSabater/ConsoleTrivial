// Francisco Sabater Villora

using System;
using System.Collections.Generic;

public class Game
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

    public static void PlayersSelect(ref ushort totalPlayers,
    List<Player> playersList)
    {
        bool validNumPlayers = false;
        while (!validNumPlayers)
        {
            Console.Clear();
            Console.SetCursorPosition(30, 0);
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
            aux.Name = nameAux;
            playersList.Add(aux);
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
                p.Name);
            
            // Console.WriteLine("Score: {0}",
            //     p.score);
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

        for (int i = 0; i < ARRAYSIZE; i++)
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

        // reset console color to blue
        Console.BackgroundColor = ConsoleColor.Blue;

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
            else if (nextPosition == "2")
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

    public static void QuestionShow(Question question, int points)
    {
        Console.SetCursorPosition(0, 29);
        Console.WriteLine("Category: {0}", question.Category);
        Console.WriteLine(question.Text);
        Console.WriteLine(question.Answer1);
        Console.WriteLine(question.Answer2);
        Console.WriteLine(question.Answer3);
        Console.WriteLine(question.Answer4);
        Console.Write("Enter the answer number: ");
        string userInput = Console.ReadLine();

        if (userInput == Convert.ToString(question.Correct))
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

    public void Run()
    {
        Board board = new Board();
        ListOfQuestions questions = new ListOfQuestions();
        List<Player> playersList = new List<Player>();

        bool exit = false;
        ushort totalPlayers = 0;
        ushort actualPlayer = 0;
        int actualPosition = 0;
        Square[] squareArray = CreateBoard();

        // players select
        PlayersSelect(ref totalPlayers, playersList);

        // Game Loop
        while (!exit)
        {
            //clear and draw the board
            Console.Clear();
            ShowScores(playersList);
            board.Draw(squareArray);
            DrawPlayer(squareArray[actualPosition].x,
                squareArray[actualPosition].y);

            Console.SetCursorPosition(11, 10);
            Console.WriteLine("{0} playing... ",
                playersList[actualPlayer].Name);

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
                Question questionAux = questions.GetFromCategoryNR(
                    squareArray[actualPosition].category);
                if(questionAux == null)
                {
                    Console.WriteLine("No question avaliable");
                }
                else
                {
                    QuestionShow(questionAux, 0); // record score dont avaliable for now
                }
            }

            // Check the players score and see if the game end
            // ToDo

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
}
