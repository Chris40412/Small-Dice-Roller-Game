//Plan: Make 2 player Connect 4 (because idk how I would make a bot)
//Make the grid 7/6 (Done), pieces (x & o) (Done), add drop effect, both players, then win conditions.

using System;
using System.Xml;

class Connect4Game
{
    static int[,] connect4Board = new int[7, 6];

    static int DropEffect(int collumn) //It will enter which collumn (i, 0), then find the lowest spot in said collumn
    {
        bool dropSpotFound = false;
        int dropSpot = 0;
        for (int i = 0; i < 6; i++) //Check to see if this is correct later on
        {
            switch (connect4Board[collumn, i]) //a this is my making my head hurt trying to visualize
            {
                case 0: //Empty spot
                    {
                        break;
                    }
                case 1 or 2: //Occupied Spot
                    {
                        dropSpotFound = true;
                        dropSpot = i - 1;
                        break;
                    }
                default: { Console.WriteLine("EEEEEEEEERRRRRRRROOOORRRRRR"); break; }
            }
            if (dropSpotFound)
            {
                break;
            }
        }
        return dropSpot; //I hope this works
    }

    static int PlayerInput(string instructions, string error, string extra = "")
    {
        bool validResponse = false;
        string? elPlayerInput = "";
        int integerValue = 0;
        Console.WriteLine(instructions);
        do
        {
            elPlayerInput = Console.ReadLine();
            if (int.TryParse(elPlayerInput, out integerValue))
            {
                validResponse = true;
            }
            else
            {
                Console.WriteLine(error);
            }
        } while (!validResponse);

        return 0;
    }













    static void Main() //Game start
    {
        int gameCondition = 0; //Condition of Game, 0 = ongoing, 1 = p1 win, 2 = p2 win, 3 = draw




        do
        {




            int i = 0;
            foreach (int spot in Connect4Game.connect4Board) //Displays board
            {
                string piece;
                if (spot == 0)
                {
                    piece = "0";
                }
                else if (spot == 1)
                {
                    piece = "o";
                }
                else if (spot == 2)
                {
                    piece = "x";
                }
                else
                {
                    piece = "error";
                }
                Console.Write(piece + "\t");
                i++;
                if (i % 7 == 0)
                {
                    Console.WriteLine();
                }
            }
        } while (gameCondition == 0);
    }



}

