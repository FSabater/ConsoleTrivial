// Francisco Sabater Villora

using System;

public class Board
{
    public void Draw(Square[] squareArray)
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
            Console.SetCursorPosition(aux.x, aux.y + 1);
            Console.Write("|   {0}   |", aux.category);
            Console.SetCursorPosition(aux.x, aux.y + 2);
            Console.Write("|        |");
            Console.SetCursorPosition(aux.x, aux.y + 3);
            Console.Write("|________|");
        }
        
    }
}

