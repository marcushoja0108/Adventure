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
        private List<Player> Characters { get; }


        public Environment()
        {
            Characters = LoadDeadCharacters();
        }

        public void MainMenu()
        {
            while (true)
            {
                Console.WriteLine("Welcome to Adventure!");
                Console.WriteLine("1. Create new adventure");
                Console.WriteLine("2. View past characters");
                Console.WriteLine("3. Stats");
                switch (Console.ReadLine())
                {
                    case "1":
                        CreateCharacterMenu();
                        break;
                    case "2":
                        ViewPastCharacters();
                        break;
                    case "3":
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
            Console.Write("Name: ");
            string newName = Console.ReadLine();
            GameLoop newGame = new GameLoop();
            switch (playerClass)
            {
                case 1:
                    Player newWarrior = new Player(newName, "Warrior",20, 10, 1,1, 10, 3);
                    newGame.Loop(newWarrior);
                    break;
                case 2:
                    Player newMage = new Player(newName, "Mage", 10, 3,3, 1, 10, 5);
                    newGame.Loop(newMage);
                    break;
                case 3:
                    Player newHunter = new Player(newName, "Hunter", 15, 5,5, 1, 10, 7);
                    newGame.Loop(newHunter);
                    break;
            }
        }

        private List<Player> LoadDeadCharacters()
        {
            List<Player> characters;
            if (File.Exists("deadCharacters.json"))
            {
                var json = File.ReadAllText("deadCharacters.json");
                characters = JsonSerializer.Deserialize<List<Player>>(json);
                characters.Reverse();
            }
            else
            {
                characters = new List<Player>();
            }
            return characters;
        }

        public void ViewPastCharacters()
        {
            Console.WriteLine("Most recent characters first");
            if (Characters.Count <= 0)
            {
                Console.WriteLine("No past characters yet");
            }
            else
            {
                foreach (var character in Characters)
                {
                    Console.WriteLine();
                    Console.ForegroundColor = character.Color;
                    Console.WriteLine($"Name:       {character.Name}");
                    Console.WriteLine($"Class:      {character.ClassName}");
                    Console.WriteLine($"Level:      {character.Level}");
                    Console.WriteLine($"Gold:       {character.Gold}");
                    Console.WriteLine($"Max Health  {character.MaxHealth}");
                    Console.WriteLine($"Strength    {character.Strength}");
                    Console.WriteLine($"Max Stamina {character.MaxStamina}");
                    character.ShowDefeatedEnemies();
                }
            }
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
