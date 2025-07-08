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

                Console.WriteLine("Is {0} male or female (m or f)?");
                string? gender = Console.ReadLine()?.ToLower() ?? "f";
                bool isMale = (gender[0] == 'm');
                Players.Add(new Player());
                InitializePlayer(Players[i], 1400, i, gameLevel, rulersName, isMale);
            }

            PlayGame(Players, NumberOfPlayers);
        }

        private void PlayGame(List<Player> players, int numberOfPlayers)
        {
            throw new NotImplementedException();
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
