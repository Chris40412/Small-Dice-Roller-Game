//Idea: make rng game that rolls dice that give you points. Get enough points and you can buy upgrades in a shop that allow you to get more points

using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;

class IDKgame
{


    static int points = 0, diceAmount = 1, diceRoll = 0, diceMin = 1, diceMax = 6;
    static string[] upgrade1 = { "upgrade1", "", "Increase dice amount by 2", "1 gazillion" }; // [0] = {name}, [1] = {bought}, [2] = {description}, [3] = {cost}
    static string[] upgrade2 = { "upgrade2", "", "Increase minimum roll by 2", "2 gazillion" };
    static string[] upgrade3 = { "upgrade3", "", "Increase maximum roll by 4", "3.004 gazillion" };
    static string[] upgrade4 = { "upgrade4", "", "Increase dice amount by 1, minimum roll by 1, and maximum roll by 2", "idk gazillion" };
    static int rowSelected = 0;
    static bool shopActive = false;
    static bool gameActive = true;
    static string PlayerInput(string instruction, int type, string error = "") //0 = menu navigation, 1 = y/n response, 2 = game action
    {
        bool completetion = false;
        Console.WriteLine(instruction);
        do
        {
            string? input = Console.ReadLine();
            if (input == null)
            {
                input = "";
            }
            input = input.Trim().ToLower();
            if (input == "exit")
            {
                shopActive = false;
                return input;
            }
            if (input == "quit")
            {
                Environment.Exit(0);
            }
            if (type == 0) //Menu navigation
            {
                if (input == "w" || input == "s" || input == "select")
                {
                    return input;
                }
                else
                {
                    Console.WriteLine(error);
                    Console.WriteLine(instruction);
                    continue;
                }
            }
            if (type == 1) //Y/N response
            {
                if (input == "y" || input == "n")
                {
                    return input;
                }
                else
                {
                    Console.WriteLine(error);
                    Console.WriteLine(instruction);
                    continue;
                }
            }
            if (type == 2) //Game Action
            {
                if (input == "shop" || input == "stats" || input == "" || input == "quit")
                {
                    return input;
                }
                else
                {
                    Console.WriteLine(error);
                    Console.WriteLine(instruction);
                    continue;
                }
            }
        } while (!completetion);


        return "error";
    }

    static int Movement(int row, int maxRow)
    {
        string direction = PlayerInput("Type either 'w' or 's' to move up or down in the shop. Type 'Select' to select an upgrade and 'Exit' to leave.", 0);
        if (direction == "exit")
        {
            return 1000;
        }
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
            if (row > maxRow) //change depending on the row
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

    static void StatsMenu()
    {
        Console.Clear();
        Console.WriteLine("Stats are currently unavailable."); //Add later on
    }
    static void ShopMenu()
    {
        rowSelected = Movement(rowSelected, 3);
        Console.Clear();
        string[] upgradeNames = { upgrade1[0], upgrade2[0], upgrade3[0], upgrade4[0] };
        for (int i = 0; i < upgradeNames.Length; i++)
        {
            string selection = (rowSelected == i) ? ">- " : "   ";
            Console.Write($"{selection}{upgradeNames[i]}");
            if (i == 0 && upgrade1[1] == "Bought")
            {
                Console.Write("   (Bought)");
            }
            else if (i == 1 && upgrade2[1] == "Bought")
            {
                Console.Write("   (Bought)");
            }
            else if (i == 2 && upgrade3[1] == "Bought")
            {
                Console.Write("   (Bought)");
            }
            else if (i == 3 && upgrade4[1] == "Bought")
            {
                Console.Write("   (Bought)");
            }
            Console.WriteLine();
        }

        Console.WriteLine();
        if (rowSelected == 1000)
        {
            shopActive = false;
            return;
        }
        if (rowSelected >= 100)
        {
            int selectedUpgrade = rowSelected - 100;
            Console.WriteLine($"Upgrade {upgradeNames[selectedUpgrade]} selected!");
            rowSelected -= 100;
            if (upgrade1[1] == "Bought" && selectedUpgrade == 0 ||
               upgrade2[1] == "Bought" && selectedUpgrade == 1 ||
               upgrade3[1] == "Bought" && selectedUpgrade == 2 ||
               upgrade4[1] == "Bought" && selectedUpgrade == 3)
            {
                Console.WriteLine("You have already bought this upgrade.");
                return;
            }
            string buyUpgrade = PlayerInput($"Do you want to buy {upgradeNames[selectedUpgrade]} for {(selectedUpgrade == 0 ? upgrade1[3] + " Points?\n" + upgrade1[2] : selectedUpgrade == 1 ? upgrade2[3] + " Points?\n" + upgrade2[2] : selectedUpgrade == 2 ? upgrade3[3] + " Points?\n" + upgrade3[2] : upgrade4[3] + " Points?\n" + upgrade4[2])}?\n(y/n)", 1, "Please type either 'y' or 'n'.");
            if (buyUpgrade == "exit")
            {
                shopActive = false;
                return;
            }
            if (buyUpgrade == "n")
            {
                Console.WriteLine("Upgrade not purchased.");
            }
            else if (buyUpgrade == "y")
            {
                Console.WriteLine("Upgrade purchased!");
                if (selectedUpgrade == 0)
                {
                    upgrade1[1] = "Bought";
                    diceAmount += 2;
                }
                else if (selectedUpgrade == 1)
                {
                    upgrade2[1] = "Bought";
                    diceMin += 2;
                }
                else if (selectedUpgrade == 2)
                {
                    upgrade3[1] = "Bought";
                    diceMax += 4;
                }
                else if (selectedUpgrade == 3)
                {
                    upgrade4[1] = "Bought";
                    diceAmount += 1;
                    diceMin += 1;
                    diceMax += 2;
                }
            }

        }
        Console.WriteLine();
    }


    public static void Main()
    {
        Random dice = new Random();
        int tempPoints = 0;

        Console.WriteLine("Welcome to the game! Don't type anything to roll and get points, type 'shop' to enter the shop, 'stats' to see your stats, or 'quit' to exit. \nEnter anything to move onto the game!");

        do
        {
            string action = PlayerInput("", 2, "Please type either nothing, 'shop', 'stats', or 'quit'.");
            Console.Clear();
            if (action == "quit")
            {
                gameActive = false;
            }
            else if (action == "stats")
            {
                StatsMenu();
            }
            else if (action == "shop")
            {
                shopActive = true;
            }
            else if (action == "")
            {
                tempPoints = points;
                for (int i = 0; i < diceAmount; i++)
                {
                    diceRoll = dice.Next(diceMin, diceMax + 1);
                    Console.Write($"{diceRoll}   ");
                    points += diceRoll;
                }
                Console.WriteLine($"You got {points - tempPoints}! Total Points: {points}"); //Add later on
            }
            while (shopActive)
            {
                ShopMenu();
            }

        } while (gameActive);


    }

}