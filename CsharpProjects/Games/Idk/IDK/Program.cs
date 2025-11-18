//Idea: make rng game that rolls dice that give you points. Get enough points and you can buy upgrades in a shop that allow you to get more points

using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;

class IDKgame
{

    static bool upgrade1 = false, upgrade2 = true, upgrade3 = false, upgrade4 = true;
    static int rowSelected = 0;
    static string PlayerInput(string instruction, int type, string error = "")
    {
        bool completetion = false;
        do
        {
            Console.WriteLine(instruction);
            string? input = Console.ReadLine();
            if (input == null)
            {
                input = "";
            }
            input = input.Trim().ToLower();
            if (type == 0) //Shop navigation
            {
                if (input == "w" || input == "s" || input == "select")
                {
                    return input;
                }
                else
                {
                    Console.WriteLine(error);
                    continue;
                }
            }
        } while (!completetion);


        return "error";
    }

    static int ShopMovement(int row)
    {
        string direction = PlayerInput("Type either 'w' or 's' to move up or down in the shop. Type 'Select' to select an upgrade.", 0);
        if (direction == "w")
        {
            row -= 1;
            if (row < 0)
            {
                row = 0;
            }
        }
        else if (direction == "s")
        {
            row += 1;
            if (row > 3) //change depending on amount of upgrades
            {
                row = 3;
            }
        }
        else if (direction == "select")
        {
            return row + 100;
        }
        return row;
    }

    static void ShopMenu()
    {
        rowSelected = ShopMovement(rowSelected);

        string[] upgradeNames = { "Upgrade One", "Upgrade Two", "Upgrade Three", "Upgrade Four" };
        for (int i = 0; i < upgradeNames.Length; i++)
        {
            string selection = (rowSelected == i) ? ">- " : "";
            Console.WriteLine($"{selection}{upgradeNames[i]}");
        }

        if (rowSelected >= 100)
        {
            int selectedUpgrade = rowSelected - 100;
            Console.WriteLine($"Upgrade {upgradeNames[selectedUpgrade]} selected!");
            if (selectedUpgrade == 0)
            {
                upgrade1 = true;
            }
            else if (selectedUpgrade == 1)
            {
                upgrade2 = true;
            }
            else if (selectedUpgrade == 2)
            {
                upgrade3 = true;
            }
            else if (selectedUpgrade == 3)
            {
                upgrade4 = true;
            }
        }
    }


    public static void Main()
    {
        do
        {
            ShopMenu();
            Thread.Sleep(1000);
            Console.Clear();
        } while (true); 


        /*
        int points = 0, diceAmount = 3, diceRoll = 0, diceMin = 1, diceMax = 6;
        Random dice = new Random();
        bool gamePlay = true;

        do
        {
            diceRoll = 0;
            for (int i = 0; i < diceAmount; i++)
            {
                diceRoll += dice.Next(diceMin, diceMax + 1);
            }
            points += diceRoll;
            Console.WriteLine(points);
            if (points > 100) { gamePlay = false; }
        } while (gamePlay);*/



    }

}