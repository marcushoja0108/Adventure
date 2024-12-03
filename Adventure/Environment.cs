﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    internal class Environment
    {
        List<Player> characters = new List<Player>();


        public Environment()
        {
            List<Player> characters = new List<Player>();
        }

        public void MainMenu()
        {
            Console.WriteLine("Welcome to Adventure!");
            Console.WriteLine("1. Create new adventure");
            Console.WriteLine("2. View current characters");
            Console.WriteLine("3. Stats");
            switch (Console.ReadLine())
            {
                case "1":
                    CreateCharacterMenu();
                    break;
                case "2":
                    Console.WriteLine("Not ready");
                    break;
                case "3":
                    Console.WriteLine("Not ready");
                    break;
                default:
                    Console.WriteLine("Not a valid input");
                    break;
            }
        }

        public void CreateCharacterMenu()
        {
            Console.WriteLine("Select one of the following classes below");
            Console.WriteLine();
            Console.WriteLine("1. Warrior");
            Console.WriteLine("2. Mage");
            Console.WriteLine("3. Hunter");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("You have chosen warrior!");
                    CreateNewCharacter(1);
                    break;
                case "2":
                    Console.WriteLine("You have chosen mage!");
                    CreateNewCharacter(2);
                    break;
                case "3":
                    Console.WriteLine("You have chosen hunter!");
                    CreateNewCharacter(3);
                    break;
                default:
                    Console.WriteLine("That is not a valid class");
                    break;
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
                    Player newWarrior = new Player(newName, 20, 10, 1,1, 10, 3);
                    characters.Add(newWarrior);
                    newGame.Loop(newWarrior);
                    break;
                case 2:
                    Player newMage = new Player(newName, 10, 3,3, 1, 10, 5);
                    newGame.Loop(newMage);
                    break;
                case 3:
                    Player newHunter = new Player(newName, 15, 5,5, 1, 10, 7);
                    newGame.Loop(newHunter);
                    break;
            }
        }


    }
}
