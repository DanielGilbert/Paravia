using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paravia
{
    public record Player
    {
        public int Cathedral { get; internal set; }

        public string City { get; internal set; }
        public double LandPrice { get; internal set; }
        public double PublicWorks { get; internal set; }
        public int Clergy { get; internal set; }
        public int CustomsDuty { get; internal set; }
        public int Difficulty { get; internal set; }
        public int GrainPrice { get; internal set; }
        public int GrainReserve { get; internal set; }
        public int IncomeTax { get; internal set; }
        public bool IsDead { get; internal set; }
        public bool IsBankrupt { get; internal set; }
        public bool IWon { get; internal set; }
        public int Justice { get; internal set; }
        public int Land { get; internal set; }
        public bool MaleOrFemale { get; internal set; }
        public int Marketplaces { get; internal set; }
        public int Merchants { get; internal set; }
        public int Mills { get; internal set; }
        public string Name { get; internal set; }
        public int Nobles { get; internal set; }
        public int OldTitle { get; internal set; }
        public int Palace { get; internal set; }
        public int SalesTax { get; internal set; }
        public int Serfs { get; internal set; }
        public int Soldiers { get; internal set; }
        public int TitleNum { get; internal set; }
        public string Title { get; internal set; }
        public int Treasury { get; internal set; }
        public int WhichPlayer { get; internal set; }
        public int Year { get; internal set; }
        public int YearOfDeath { get; internal set; }
        public bool InvadeMe { get; internal set; }
        public int Harvest { get; internal set; }
        public int Rats { get; internal set; }
        public int GrainDemand { get; internal set; }
        public int RatsAte { get; internal set; }
        public int SoldierPay { get; internal set; }
        public int MarketRevenue { get; internal set; }
        public int NewSerfs { get; internal set; }
        public int DeadSerfs { get; internal set; }
        public int TransplantedSerfs { get; internal set; }
        public int FleeingSerfs { get; internal set; }
        public int JusticeRevenue { get; internal set; }
        public int MillRevenue { get; internal set; }
    }

    public class Game
    {
        private Random Randomizer;
        private List<Player> Players;
        private int NumberOfPlayers;
        private int gameLevel;
        private List<string> CityList;
        private List<string> MaleTitles;
        private List<string> FemaleTitles;

        public Game()
        {
            CityList = new()
            {
                "Santa Paravia",
                "Fiumaccio",
                "Torricella",
                "Molinetto",
                "Fontanile",
                "Romanga",
                "Monterana"
            };

            MaleTitles = new()
            {
                "Sir",
                "Baron",
                "Count",
                "Marquis",
                "Duke",
                "Grand Duke",
                "Prince",
                "* H.R.H. King"
            };

            FemaleTitles = new()
            {
                "Lady",
                "Baroness",
                "Countess",
                "Marquise",
                "Duchess",
                "Grand Duchess",
                "Princess",
                "* H.R.H. Queen"
            };

            Players = new List<Player>();
        }

        private int Random(int hi)
        {
            return Randomizer.Next(hi);
        }

        public void InitializePlayer(Player player, int year, int city, int level, string name, bool maleOrFemale)
        {
            player.Cathedral = 0;
            player.City = CityList[city];
            player.Clergy = 5;
            player.CustomsDuty = 25;
            player.Difficulty = level;
            player.GrainPrice = 25;
            player.GrainReserve = 5000;
            player.IncomeTax = 5;
            player.IsBankrupt = false;
            player.IsDead = false;
            player.InvadeMe = false;
            player.IWon = false;
            player.Justice = 2;
            player.Land = 10000;
            player.LandPrice = 10.0;
            player.MaleOrFemale = maleOrFemale;
            player.Marketplaces = 0;
            player.Merchants = 25;
            player.Mills = 0;
            player.Name = name;
            player.Nobles = 4;
            player.OldTitle = 1;
            player.Palace = 0;
            player.PublicWorks = 1.0;
            player.SalesTax = 10;
            player.Serfs = 2000;
            player.Soldiers = 25;
            player.TitleNum = 1;
            if (player.MaleOrFemale == true)
                player.Title = MaleTitles[0];
            else
                player.Title = FemaleTitles[0];
            if (city == 6)
                player.Title = "Baron";
            player.Treasury = 1000;
            player.WhichPlayer = city;
            player.Year = year;
            player.YearOfDeath = year + 20 + Random(35);
            return;

        }

        public void Run()
        {
            Randomizer = new Random();
            Console.WriteLine("Santa Paravia and Fiumaccio");
            Console.WriteLine();
            Console.WriteLine("Do you wish instructions (Y or N)?");
            string? response = Console.ReadLine();

            if (!String.IsNullOrWhiteSpace(response) && (response.ToLower()[0] == 'y'))
            {
                PrintInstructions();
            }

            Console.WriteLine("How many people want to play (1 to 6)?");
            response = Console.ReadLine();
            NumberOfPlayers = Convert.ToInt32(response);
            if (NumberOfPlayers < 1 || NumberOfPlayers > 6)
            {
                Console.WriteLine("Thanks for playing!");
                return;
            }

            Console.WriteLine("What will be the difficulty of this game:");
            Console.WriteLine("1. Apprentice");
            Console.WriteLine("2. Journeyman");
            Console.WriteLine("3. Master");
            Console.WriteLine("4. Grand Master");
            Console.WriteLine();
            Console.Write("Choose: ");
            gameLevel = Convert.ToInt32(Console.ReadLine());
            if (gameLevel < 1)
            {
                gameLevel = 1;
            }
            if (gameLevel > 4)
            {
                gameLevel = 4;
            }

            for(int i = 0; i < NumberOfPlayers; i++)
            {
                Console.WriteLine("Who is the ruler of {0}?", CityList[i]);
                string? rulersName = Console.ReadLine();

                if (String.IsNullOrWhiteSpace(rulersName))
                {
                    i--;
                    continue;
                }

                Console.WriteLine("Is {0} male or female (m or f)?", Players[i].Name);
                string? gender = Console.ReadLine()?.ToLower() ?? "f";
                bool isMale = (gender[0] == 'm');
                Players.Add(new Player());
                InitializePlayer(Players[i], 1400, i, gameLevel, rulersName, isMale);
            }

            PlayGame(Players, NumberOfPlayers);
        }

        private void PlayGame(List<Player> players, int numberOfPlayers)
        {
            bool AllDead, Winner;
            int WinningPlayer = 0;
            Player Baron;
            AllDead = false;
            Winner = false;
            Baron = new Player();
            InitializePlayer(Baron, 1400, 6, 4, "Peppone", true);

            while (AllDead is false && Winner is false)
            {
                for(int i = 0; i < NumberOfPlayers; i++)
                {
                    if (!players[i].IsDead)
                    {
                        NewTurn(players[i], NumberOfPlayers, players, Baron);
                    }
                }

                AllDead = true;

                for(int i = 0; i < NumberOfPlayers; i++)
                {
                    if (AllDead && players[i].IsDead is false)
                    {
                        AllDead = false;
                    }
                }

                for(int i = 0; i < NumberOfPlayers; i++)
                {
                    if (players[i].IWon is true)
                    {
                        Winner = true;
                        WinningPlayer = 1;
                    }
                }
            }

            if (AllDead is true)
            {
                Console.WriteLine("The game has ended.");
                return;
            }

            Console.WriteLine("Game Over. {0} {1} wins.", players[WinningPlayer].Title, players[WinningPlayer].Name);
        }

        private void NewTurn(Player player, int numberOfPlayers, List<Player> players, Player baron)
        {
            GenerateHarvest(player);
            NewLandAndGrainPrices(player);
            BuySellGrain(player);
            ReleaseGrain(player);

            if (player.InvadeMe is true)
            {
                int i = 0;
                for(i = 0; i < NumberOfPlayers; i++)
                {
                    if (i != player.WhichPlayer)
                    {
                        if (players[i].Soldiers > (player.Soldiers * 2.4))
                        {
                            //AttackNeighbor(players[i], player);
                            i = 9;
                        }
                    }
                }

                if (i != 9)
                {
                    //AttackNeighbor(baron, player);
                }

            }

            //AdjustTax(player);
            //DrawMap(player);
            //StatePurchases(player);
            //CheckNewTitle(player);

            player.Year++;
            if (player.Year == player.YearOfDeath)
            {
                //ImDead(player);
            }

            if (player.TitleNum >= 7)
            {
                player.IWon = true;
            }
        }

        private int ReleaseGrain(Player player)
        {
            double xp, zp;
            double x, z;
            string result;
            int howMuch, maximum, minimum;
            bool isOK;
            isOK = false;
            howMuch = 0;
            minimum = player.GrainReserve / 5;
            maximum = player.GrainReserve - minimum;

            while(isOK is false)
            {
                Console.WriteLine("How much grain will you release for consumption?");
                Console.WriteLine("1 = Minimum ({0})", minimum);
                Console.WriteLine("2 = Maximum ({1})", maximum);
                Console.Write("or enter a number: ");

                result = Console.ReadLine() ?? "0";

                howMuch = Convert.ToInt32(result);

                if (howMuch == 1)
                    howMuch = minimum;

                if (howMuch == 2)
                    howMuch = maximum;

                //Are we being Scrooge?
                if (howMuch < minimum)
                    Console.WriteLine("You must at least release 20% of your reserves.");
                else if (howMuch > maximum)
                    //Whoa! Slow down there son!
                    Console.WriteLine("You must keep at least 20%!");
                else
                    isOK = true;
            }

            player.SoldierPay = 0;
            player.MarketRevenue = 0;
            player.NewSerfs = 0;
            player.DeadSerfs = 0;
            player.TransplantedSerfs = 0;
            player.FleeingSerfs = 0;
            player.InvadeMe = false;
            player.GrainReserve -= howMuch;

            z = Convert.ToDouble(howMuch) / Convert.ToDouble(player.GrainDemand) - 1.0;

            if (z > 0.0)
            {
                z /= 2.0;
            }

            if (z > 0.25)
            {
                z = z / 10.0 + 0.25;
            }

            zp = 50.0 - Convert.ToDouble(player.CustomsDuty) - Convert.ToDouble(player.SalesTax) - Convert.ToDouble(player.IncomeTax);

            if (zp < 0.0)
            {
                zp *= Convert.ToDouble(player.Justice);
            }

            zp /= 10.0;

            if (zp > 0.0)
            {
                zp += (3.0 - Convert.ToDouble(player.Justice));
            }

            z += Convert.ToDouble(zp) / 10.0;

            if (z > 0.5)
                z = 0.5;

            if (howMuch < (player.GrainDemand - 1))
            {
                x = (Convert.ToDouble(player.GrainDemand) - Convert.ToDouble(howMuch)) / player.GrainDemand * 100.0 - 9.0;

                xp = Convert.ToDouble(x);

                if (x > 65.0)
                    x = 65.0;

                if (x < 0.0)
                {
                    xp = 0.0;
                    x = 0.0;
                }

                SerfsProcreating(player, 3.0);
                SerfsDecomposing(player, xp + 8.0);
            }
            else
            {
                SerfsProcreating(player, 7.0);
                SerfsDecomposing(player, 3.0);

                if ((player.CustomsDuty + player.SalesTax) < 35)
                    player.Merchants += Random(4);

                if (player.IncomeTax < Random(28))
                {
                    player.Nobles += Random(2);
                    player.Clergy += Random(3);
                }

                if (howMuch > Convert.ToInt32(Convert.ToDouble(player.GrainDemand) * 1.3))
                {
                    zp = Convert.ToDouble(player.Serfs) / 1000.0;
                    z = Convert.ToDouble(howMuch - player.GrainDemand) / Convert.ToDouble(player.GrainDemand) * 10.0;
                    z *= (zp * Convert.ToDouble(Random(25)));
                    z += Convert.ToDouble(Random(40));
                    player.TransplantedSerfs = Convert.ToInt32(z);
                    player.Serfs += player.TransplantedSerfs;
                    Console.WriteLine("{0} serfs move to the city.", player.TransplantedSerfs);
                    zp = Convert.ToDouble(z);
                    z = (Convert.ToDouble(zp) * Convert.ToDouble(Randomizer.Next(0, Int16.MaxValue)) / Convert.ToDouble(Int16.MaxValue)); //((float)zp * (float)rand()) / (float)RAND_MAX;
                    if (z > 50.0)
                        z = 50.0;
                    player.Merchants += Convert.ToInt32(z);
                    player.Nobles++;
                    player.Clergy += 2;
                }
            }

            if (player.Justice > 2)
            {
                player.JusticeRevenue = player.Serfs / 100 * (player.Justice - 2) * (player.Justice - 2);
                player.JusticeRevenue = Random(player.JusticeRevenue);

                player.Serfs -= player.JusticeRevenue;
                player.FleeingSerfs = player.JusticeRevenue;

                Console.WriteLine("{0} serfs flee harsh justice.", player.FleeingSerfs);
            }

            player.MarketRevenue = player.Marketplaces * 75;

            if (player.MarketRevenue > 0)
            {
                player.Treasury += player.MarketRevenue;
                Console.WriteLine("Your market earned {0} florines.", player.MarketRevenue);
            }

            player.MillRevenue = player.Mills * (55 + Random(250));

            if (player.MillRevenue > 0)
            {
                player.Treasury += player.MillRevenue;
                Console.WriteLine("Your woolen mill earned {0} florins.", player.MillRevenue);
            }

            player.SoldierPay = player.Soldiers * 3;
            player.Treasury -= player.SoldierPay;

            Console.WriteLine("You paid your soldiers {0} florins.", player.SoldierPay);
            Console.WriteLine("You have {0} serfs in your city.", player.Serfs);
            Console.WriteLine("(Press Enter)");

            _ = Console.ReadLine();

            if ((player.Land / 1000) > player.Soldiers)
            {
                player.InvadeMe = true;
                return 3;
            }

            if ((player.Land / 500) > player.Soldiers)
            {
                player.InvadeMe = true;
                return 3;
            }

            return 0;

        }

        private void SerfsProcreating(Player player, double myScale)
        {
            int absc = 0;
            double ord = 0.0;

            absc = Convert.ToInt32(myScale);
            ord = myScale - Convert.ToDouble(absc);

            player.NewSerfs = Convert.ToInt32((Random(absc) + ord) * Convert.ToDouble(player.Serfs) / 100.0);
            player.Serfs += player.NewSerfs;

            Console.WriteLine("{0} serfs born this year.", player.Serfs);
        }

        private void SerfsDecomposing(Player player, double myScale)
        {
            int absc = 0;
            double ord = 0.0;

            absc = Convert.ToInt32(myScale);
            ord = myScale - Convert.ToDouble(absc);

            player.DeadSerfs = Convert.ToInt32((Random(absc) + ord) * Convert.ToDouble(player.Serfs) / 100.0);
            player.Serfs += player.DeadSerfs;

            Console.WriteLine("{0} serfs born this year.", player.Serfs);
        }

        private void BuySellGrain(Player player)
        {
            bool finished = false;
            string result = string.Empty;

            while(finished is false)
            {
                Console.WriteLine("Year {0:d}", player.Year);
                Console.WriteLine();
                Console.WriteLine("{0} {1}", player.Title, player.Name);
                Console.WriteLine("Rats ate {0:d} of your grain reserves.", player.Rats);
                PrintGrain(player);
                Console.WriteLine("Grain\tGrain\tPrice of\tPrice of\tTreasury");
                Console.WriteLine("Reserve\tDemand\tGrain\t\tLand");
                Console.WriteLine("{0:d}\t{1:d}\t{2:d}\t\t{3}\t\t{4}", player.GrainReserve, player.GrainDemand, player.GrainPrice, player.LandPrice, player.Treasury);
                Console.WriteLine("steres\tsteres\t1000 st.\thectare\t\tgold florins");
                Console.WriteLine();
                Console.WriteLine("You have {0} hectares of land.", player.Land);
                Console.WriteLine();
                Console.WriteLine("1. Buy Grain");
                Console.WriteLine("2. Sell Grain");
                Console.WriteLine("3. Buy land");
                Console.WriteLine("4. Sell land");
                Console.Write("(Enter q to continue): ");

                result = Console.ReadLine()?.ToLower() ?? "q";
                if (result[0] == 'q')
                    finished = true;

                if (result[0] == '1')
                    BuyGrain(player);

                if (result[0] == '2')
                    SellGrain(player);

                if (result[0] == '3')
                    BuyLand(player);

                if (result[0] == '4')
                    SellLand(player);
            }

        }
        private void BuyGrain(Player player)
        {
            int howMuch;
            string result;

            Console.Write("How much grain do you want to buy (0 to specify a total)? ");

            result = Console.ReadLine() ?? "0";

            howMuch = Convert.ToInt32(result);

            if (howMuch == 0)
            {
                Console.Write("How much total grain do you wish? ");
                result = Console.ReadLine() ?? "0";
                howMuch = Convert.ToInt32(result);
                howMuch -= player.GrainReserve;
                if (howMuch < 0)
                {
                    Console.WriteLine("Invalid total amount.");
                    Console.WriteLine();
                    return;
                }
            }

            player.Treasury -= (howMuch * player.GrainPrice / 1000);
            player.GrainReserve += howMuch;
        }

        private void SellGrain(Player player)
        {
            int howMuch;
            string result;

            Console.Write("How much grain do you want to sell? ");

            result = Console.ReadLine() ?? "0";

            howMuch = Convert.ToInt32(result);

            if (howMuch > player.GrainReserve)
            {
                Console.WriteLine("You don't have it.");
                return;
            }

            player.Treasury += (howMuch * player.GrainPrice / 1000);
            player.GrainReserve -= howMuch;
        }
        private void BuyLand(Player player)
        {
            int howMuch;
            string result;

            Console.Write("How much land do you want to buy? ");

            result = Console.ReadLine() ?? "0";

            howMuch = Convert.ToInt32(result);

            player.Treasury -= Convert.ToInt32(howMuch * player.LandPrice);
            player.Land += howMuch;
        }

        private void SellLand(Player player)
        {
            int howMuch;
            string result;

            Console.Write("How much land do you want to sell? ");

            result = Console.ReadLine() ?? "0";

            howMuch = Convert.ToInt32(result);

            if (howMuch > (player.Land - 5000))
            {
                Console.WriteLine("You can't sell that much.");
                return;
            }

            player.Treasury += Convert.ToInt32(howMuch * player.LandPrice);
            player.Land -= howMuch;
        }

        private void PrintGrain(Player player)
        {
            switch (player.Harvest)
            {
                case 0:
                case 1:
                    Console.WriteLine("Drought. Famine Threatens. ");
                    break;
                case 2:
                    Console.WriteLine("Bad Weather. Poor Harvest. ");
                    break;
                case 3:
                    Console.WriteLine("Normal Weather. Average Harvest. ");
                    break;
                case 4:
                    Console.WriteLine("Good Weather. Fine Harvest. ");
                    break;
                case 5:
                    Console.WriteLine("Excellent Weather. Great Harvest! ");
                    break;
            }
        }

        private void NewLandAndGrainPrices(Player player)
        {
            double x, y, myRandom;
            int h;

            //Generate an offset for use in later int->float conversions.
            myRandom = Randomizer.NextDouble();

            /* If you think this C# code is ugly, you should see the original C. */
            x = player.Land;
            y = ((player.Serfs - player.Mills) * 100.0) * 5.0;
            if (y < 0.0)
            {
                y = 0.0;
            }

            if (y < x)
            {
                x = y;
            }

            y = player.GrainReserve * 2.0;

            if (y < x)
            {
                x = y;
            }

            y = player.Harvest + (myRandom + 0.5);
            h = Convert.ToInt32(x * y);

            player.GrainReserve += h;
            player.GrainDemand = (player.Nobles * 100) + (player.Cathedral * 40) + (player.Merchants * 30);
            player.GrainDemand += ((player.Soldiers * 10) + (player.Serfs * 5));
            player.LandPrice = (3.0 * player.Harvest + Convert.ToDouble(Random(6)) + 10.0) / 10.0;

            if (h < 0)
            {
                h *= -1;
            }

            if (h < 1)
            {
                y = 2.0;
            }
            else
            {
                y = Convert.ToDouble(player.GrainDemand / (double)h);
                if (y > 2.0)
                {
                    y = 2.0;
                }
            }

            if (y < 0.8)
            {
                y = 0.8;
            }

            player.LandPrice *= y;

            if(player.LandPrice < 1.0)
            {
                player.LandPrice = 1.0;
            }

            player.GrainPrice = Convert.ToInt32(((6.0 - player.Harvest) * 3.0 + Random(5) + Random(5)) * 4.0 * y);
            player.RatsAte = h;
        }

        private void GenerateHarvest(Player player)
        {
            player.Harvest = (Random(5) + Random(6)) / 2;
            player.Rats = Random(50);
            player.GrainReserve = ((player.GrainReserve * 100) - (player.GrainReserve * player.Rats)) / 100;
        }

        private void PrintInstructions()
        {
            Console.WriteLine("Santa Paravia and Fiumaccio");
            Console.WriteLine();
            Console.WriteLine("You are the ruler of a 15th century Italian city state.");
            Console.WriteLine("If you rule well, you will receive higher titles. The");
            Console.WriteLine("first player to become king or queen wins. Life expectancy");
            Console.WriteLine("then was brief, so you may not live long enough to win.");
            Console.WriteLine("The computer will draw a map of your state. The size");
            Console.WriteLine("of the area in the wall grows as you buy more land. The");
            Console.WriteLine("size of the guard tower in the upper left corner shows");
            Console.WriteLine("the adequacy of your defenses. If it shrinks, equip more");
            Console.WriteLine("soldiers! If the horse and plowman is touching the top of the wall,");
            Console.WriteLine("all your land is in production. Otherwise you need more");
            Console.WriteLine("serfs, who will migrate to your state if you distribute");
            Console.WriteLine("more grain than the minimum demand. If you distribute less");
            Console.WriteLine("grain, some of your people will starve, and you will have");
            Console.WriteLine("a high death rate. High taxes raise money, but slow down");
            Console.WriteLine("economic growth. (Press ENTER to begin game)");
            Console.ReadLine();
            return;
        }
    }
}
