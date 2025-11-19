//Idea: make rng game that rolls dice that give you points. Get enough points and you can buy upgrades in a shop that allow you to get more points

using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;

class IDKgame
{


    static int points = 0, diceAmount = 1, diceRoll = 0, diceMin = 1, diceMax = 6;
    static string[] upgrade1 = { "upgrade1", "", "Increase dice amount by 2", "50" }; // [0] = {name}, [1] = {bought}, [2] = {description}, [3] = {cost}
    static string[] upgrade2 = { "upgrade2", "", "Increase minimum roll by 2", "25" };
    static string[] upgrade3 = { "upgrade3", "", "Increase maximum roll by 4", "25" };
    static string[] upgrade4 = { "upgrade4", "", "Increase dice amount by 1, minimum roll by 1, and maximum roll by 2", "100" };
    static string[] infupgrade = { "infinite upgrade", "", "Increases dice amount by 1 each purchase", "25", "0"}; //[4] = times purchased
    static int rowSelected = 0;
    static bool shopActive = false, statsActive = false;
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
                if (input == "shop" || input == "stats" || input == "" || input == "quit" || input == "minecraft creative mode")
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

    static int Movement(int row, int maxRow, int MovementType) //0 = shop, 1 = stats
    {
        string direction = "";
        if (MovementType == 0)
        {
            direction = PlayerInput("Type either 'w' or 's' to move up or down in the shop. Type 'Select' to select an upgrade and 'Exit' to leave.", 0);
            Console.Clear();
        }
        else if (MovementType == 1)
        {
            direction = PlayerInput("Type either 'w' or 's' to move up or down in the stats menu. Type 'Select' to learn about the stat and 'Exit' to leave.", 0);
            Console.Clear();
        }
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
                row = maxRow;
            }
        }
        else if (direction == "select")
        {
            return row + 100;
        }
        return row;
    }

    static void StatsMenu() //Complete
    {
        string[] statNames = { "Points", "Dice Amount", "Minimum Dice Roll", "Maximum Dice Roll" };
        if (rowSelected == 1000)
        {
            statsActive = false;
            return;
        }
        if (rowSelected >= 100)
        {
            rowSelected -= 100;
            if (rowSelected == 0)
            {
                Console.WriteLine($"You currently have {points} points, these points are used to buy upgrades that allow you to get even more points through increasing the other values in the stats menu.");
            }
            else if (rowSelected == 1)
            {
                Console.WriteLine($"You currently have {diceAmount} dice, these determine how many times you earn points each roll, incredibly increasing time saved.");
            }
            else if (rowSelected == 2)
            {
                Console.WriteLine($"Your current minimum dice roll is {diceMin}, this simply increase the bare minimum that you can roll, guaranteeing a higher success rate.");
            }
            else if (rowSelected == 3)
            {
                Console.WriteLine($"Your current maximum dice roll is {diceMax}, this increases the highest value you can roll, allowing for more points to be earned but no guaranteed.");
            }
            Console.WriteLine("\nEnter anything to go back to the stats menu.");
            Console.ReadLine();
            Console.Clear();
        }
        for (int i = 0; i < statNames.Length; i++)
        {
            string selection = (rowSelected == i) ? ">- " : "   ";
            Console.WriteLine($"{selection}{statNames[i]}");
        }
        rowSelected = Movement(rowSelected, 3, 1);
        

    }
    static void ShopMenu()
    {
        string[] upgradeNames = { upgrade1[0], upgrade2[0], upgrade3[0], upgrade4[0], infupgrade[0] };
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
            else if (i == 4 && infupgrade[1] == "Bought")
            {
                Console.Write("   (Bought " + infupgrade[4] + " times)");
            }
            Console.WriteLine();
        }
        rowSelected = Movement(rowSelected, 4, 0);

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
            string buyUpgrade = PlayerInput($"Do you want to buy {upgradeNames[selectedUpgrade]} for {(selectedUpgrade == 0 ? upgrade1[3] + " Points?\n" + upgrade1[2] : selectedUpgrade == 1 ? upgrade2[3] + " Points?\n" + upgrade2[2] : selectedUpgrade == 2 ? upgrade3[3] + " Points?\n" + upgrade3[2] : selectedUpgrade == 3 ? upgrade4[3] + " Points?\n" + upgrade4[2] : selectedUpgrade == 4 ? infupgrade[3] + " Points?\n" + infupgrade[2] : "")}?\n(y/n)", 1, "Please type either 'y' or 'n'.");
            if (buyUpgrade == "exit")
            {
                shopActive = false;
                return;
            }
            if (buyUpgrade == "n")
            {
                Console.Clear();
                Console.WriteLine("Upgrade not purchased.");
                Console.WriteLine("Enter anything to return to the shop menu.");
                Console.ReadLine();
                Console.Clear();
            }
            else if (buyUpgrade == "y")
            {
                Console.Clear();
                if (selectedUpgrade == 0)
                {
                    if (points >= 50)
                    {
                    Console.WriteLine("Upgrade purchased!");
                    upgrade1[1] = "Bought";
                    diceAmount += 2;
                    points -= 50;
                    }
                    else
                    {
                        Console.WriteLine("You do not have enough points to buy this upgrade.");
                        Console.WriteLine("Enter anything to return to the shop menu.");
                        Console.ReadLine();
                        Console.Clear();
                        return;
                    }
                }
                if (selectedUpgrade == 1)
                {
                    if (points >= 25)
                    {
                    Console.WriteLine("Upgrade purchased!");
                    upgrade2[1] = "Bought";
                    diceMin += 2;
                    points -= 25;
                    }
                    else
                    {
                        Console.WriteLine("You do not have enough points to buy this upgrade.");
                        Console.WriteLine("Enter anything to return to the shop menu.");
                        Console.ReadLine();
                        Console.Clear();
                        return;
                    }
                }
                if (selectedUpgrade == 2)
                {
                    if (points >= 25)
                    {
                    Console.WriteLine("Upgrade purchased!");
                    upgrade3[1] = "Bought";
                    diceMax += 4;
                    points -= 25;
                    }
                    else
                    {
                        Console.WriteLine("You do not have enough points to buy this upgrade.");
                        Console.WriteLine("Enter anything to return to the shop menu.");
                        Console.ReadLine();
                        Console.Clear();
                        return;
                    }
                }
                if (selectedUpgrade == 3)
                {
                    if (points >= 100)
                    {
                    Console.WriteLine("Upgrade purchased!");
                    upgrade4[1] = "Bought";
                    diceAmount += 1;
                    diceMin += 1;
                    diceMax += 2;
                    points -= 100;
                    }
                    else
                    {
                        Console.WriteLine("You do not have enough points to buy this upgrade.");
                        Console.WriteLine("Enter anything to return to the shop menu.");
                        Console.ReadLine();
                        Console.Clear();
                        return;
                    }
                }
                if (selectedUpgrade == 4)
                {
                    if (points >= int.Parse(infupgrade[3]))
                    {
                    Console.WriteLine("Upgrade purchased!");
                    infupgrade[1] = "Bought";
                    diceAmount += 1;
                    points -= int.Parse(infupgrade[3]);
                    infupgrade[3] = (int.Parse(infupgrade[3]) + 100).ToString();
                    infupgrade[4] = (int.Parse(infupgrade[4]) + 1).ToString();
                    }
                    else
                    {
                        Console.WriteLine("You do not have enough points to buy this upgrade.");
                        Console.WriteLine("Enter anything to return to the shop menu.");
                        Console.ReadLine();
                        Console.Clear();
                        return;
                    }
                }
                Console.WriteLine("Enter anything to return to the shop menu.");
                Console.ReadLine();
                Console.Clear();
            }

        }
    }


    public static void Main()
    {
        Random dice = new Random();
        int tempPoints = 0;

        Console.WriteLine("Welcome to the game! Don't type anything to roll and get points, type 'shop' to enter the shop, 'stats' to see your stats, or 'quit' to exit. \n Enter nothing to start rolling!");

        do
        {
            rowSelected = 0;
            string action = PlayerInput("", 2, "Please type either nothing, 'shop', 'stats', or 'quit'.");
            Console.Clear();
            if (action == "minecraft creative mode")
            {
                points += 1000;
                upgrade1[1] = "Bought";
                upgrade2[1] = "Bought";
                upgrade3[1] = "Bought";
                upgrade4[1] = "Bought";
                diceAmount += 3;
                diceMin += 3;
                diceMax += 6;
                Console.WriteLine("legit godmode lel");
                Console.ReadLine();
                Console.Clear();
            }
            if (action == "quit")
            {
                gameActive = false;
            }
            else if (action == "stats")
            {
                statsActive = true;
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
            while (statsActive)
            {
                StatsMenu();
            }
        } while (gameActive);


    }

}