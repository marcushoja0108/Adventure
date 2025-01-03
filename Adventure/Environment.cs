using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Adventure
{
    internal class Environment
    {
        private List<Player> DeadCharacters { get; }
        private List<Player> Characters { get; }


        public Environment()
        {
            DeadCharacters = LoadCharacters("deadCharacters.json");
            Characters = LoadCharacters("Characters.json");
        }

        public void MainMenu()
        {
            while (true)
            {
                Console.WriteLine("Welcome to Adventure!");
                Console.WriteLine("1. Create new adventure");
                Console.WriteLine("2. View current characters");
                Console.WriteLine("3. View dead characters");
                Console.WriteLine("4. Stats");
                switch (Console.ReadLine())
                {
                    case "1":
                        CreateCharacterMenu();
                        break;
                    case "2":
                        ViewCurrentCharacters();
                        break;
                    case "3":
                        ViewPastCharacters();
                        break;
                    case "4":
                        Console.WriteLine("Not ready");
                        break;
                    default:
                        Console.WriteLine("Not a valid input");
                        break;
                }
            }
        }

        public void CreateCharacterMenu()
        {
            Console.WriteLine("Select one of the following classes below");
            Console.WriteLine();
            Console.WriteLine("1. Warrior");
            Console.WriteLine("2. Mage");
            Console.WriteLine("3. Hunter");
            bool done = false;
            while (done == false)
            {
                switch (Console.ReadLine())
                {
                    case "1":
                        done = true;
                        Console.WriteLine("You have chosen warrior!");
                        CreateNewCharacter(1);
                        break;
                    case "2":
                        done = true;
                        Console.WriteLine("You have chosen mage!");
                        CreateNewCharacter(2);
                        break;
                    case "3":
                        done = true;
                        Console.WriteLine("You have chosen hunter!");
                        CreateNewCharacter(3);
                        break;
                    default:
                        Console.WriteLine("That is not a valid class");
                        break;
                }
            }
        }

        private void CreateNewCharacter(int playerClass)
        {
            string newName = GetNewName();
            GameLoop newGame = new GameLoop();
            switch (playerClass)
            {
                case 1:
                    Player newWarrior = new Player(Characters.Count, newName, "Warrior",20, 10, 1,1, 10, 3, ConsoleColor.Blue);
                    newGame.Loop(newWarrior);
                    break;
                case 2:
                    Player newMage = new Player(Characters.Count, newName, "Mage", 10, 3,3, 1, 10, 5, ConsoleColor.Magenta);
                    newGame.Loop(newMage);
                    break;
                case 3:
                    Player newHunter = new Player(Characters.Count, newName, "Hunter", 15, 5,5, 1, 10, 7, ConsoleColor.Green);
                    newGame.Loop(newHunter);
                    break;
            }
        }

        private string GetNewName()
        {
            while (true)
            {
            Console.Write("Name: ");
            string newName = Console.ReadLine();
                switch (NameCheck(newName))
                {
                    case true:
                        Console.WriteLine("That name is already in use");
                        break;
                    case false:
                        return newName;
                }
            }
        }

        private bool NameCheck(string name)
        {

            List<Player> allCharacters = DeadCharacters.Concat(Characters).ToList();
            bool exists = false;
            foreach(var character in allCharacters)
            {
                if(character.Name == name)
                {
                    exists = true;
                }
            }
            return exists;
        }

        private List<Player> LoadCharacters(string jsonCharacters)
        {
            List<Player> characters;
            if (File.Exists(jsonCharacters))
            {
                var json = File.ReadAllText(jsonCharacters);
                characters = JsonSerializer.Deserialize<List<Player>>(json) ?? new List<Player>();
            }
            else
            {
                characters = new List<Player>();
            }
            return characters;
        }

        private void ViewCurrentCharacters()
        {
            foreach(var character in Characters)
            {
                character.ShowSimple();
            }
            ViewCurrentCharactersMenu();
        }

        private void ViewCurrentCharactersMenu()
        {
            Console.WriteLine("1. Continue an adventure");
            Console.WriteLine("2. Back");
            switch (Console.ReadLine())
            {
                case "1":
                    if(Characters.Count > 0)
                    {
                    ChooseCurrentCharacter();
                    }
                    else
                    {
                        Console.WriteLine("No available characters");
                    }
                    break;
                case "2":
                    return;
            }
        }

        private void ChooseCurrentCharacter()
        {
            Console.WriteLine("Type the name of your selected character");
            Console.Write("Character Name:");
            string userInput = Console.ReadLine();
            Player select = null;
            foreach (var character in Characters)
            {
                if(character.Name == userInput)
                {
                    select = character;
                }
            }
            if (select != null) 
            {
                GameLoop gameLoop = new GameLoop();
                LoadingDots();
                gameLoop.Loop(select);
            }
            else
            {
                Console.WriteLine("No character with that name");
            }
        }

        public void ViewPastCharacters()
        {
            List<Player> characters = Characters;
            characters.Reverse();
            Console.WriteLine("Most recent characters first");
            if (DeadCharacters.Count <= 0)
            {
                Console.WriteLine("No past characters yet");
            }
            else
            {
                foreach (var character in characters)
                {
                    character.ShowSimple();
                    character.ShowDefeatedEnemies();
                }
            }
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
            Console.Clear();
        }

        public void LoadingDots()
        {
            string dots = new string('.', 4);
            foreach (var dot in dots)
            {
                Console.Write($"{dot}");
                Thread.Sleep(700);
            }
            Console.WriteLine();
        }
    }
}
