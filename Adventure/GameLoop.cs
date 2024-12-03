using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adventure;

namespace Adventure
{
    internal class GameLoop
    {
        List<NPC> _availableNpcs = new List<NPC>();

        public void Loop(Player character)
        {
            bool playing = true;
            while (playing)
            {
                GenerateEnemies(character);
                Console.Clear();
                Random random = new Random();
                //int randomEvent = random.Next(0,5);

                switch (0)
                {
                    case 0:
                        EnemyEncounter(character);
                        break;
                    default:
                        break;
                }
            }
        }
        public void GenerateEnemies(Player character)
        {
            _availableNpcs.Add(new Goblin(character.Level));
            //_availableNpcs.Add(new NPC("Big orc", character.Level));
            //_availableNpcs.Add(new NPC("Smelly troll", character.Level));
        }

        private void EnemyEncounter(Player character)
        {
            Random rand = new Random();
            NPC encounter = _availableNpcs[rand.Next(0, _availableNpcs.Count)];

            Console.WriteLine();
            Console.WriteLine($"{character.Name} met {encounter.Name}.");
            character.ShowEncounterOptions();
            bool done = false;
            while (done == false)
            {
                Console.WriteLine("What do you want to do?");
                switch (Console.ReadLine())
                {
                    case "1":
                        done = true;
                        Battle(character, encounter);
                        break;
                    case "2":
                        done = true;
                        SneakAway(character, encounter);
                        break;
                    case "3":
                        character.Show();
                        break;
                }
            }
        }

        public void Battle(Player character, NPC encounter)
        {
            while (encounter.Health > 0)
            {
                Check(character);
                character.ShowFightOptions();
                bool done = false;

                while (done == false)
                {
                    switch (Console.ReadLine())
                    {
                        case "1":
                            character.Attack(encounter);
                            done = true;
                            break;
                        case "2":
                            character.Rest();
                            done = true;
                            break;
                        case "3":
                            character.Show();
                            break;
                        default:
                            Console.WriteLine("Not a valid command.");
                            break;
                    }
                }
                Thread.Sleep(500);
                if (encounter.Health > 0) { encounter.Action(character); }
                else
                {
                    character._defeatedList.Add(encounter);
                    Console.WriteLine($"{character.Name} has defeated {encounter.Name}.");
                    Console.WriteLine();
                    character.LevelUp();
                    Console.WriteLine("Press a button to continue");
                    Console.ReadLine();
                };
            }

        }

        public void SneakAway(Player character, NPC encounter)
        {
            Console.WriteLine();
            Console.WriteLine($"{character.Name} tries to sneak away.");
            Random sneak = new Random();
            int action = sneak.Next(character.Cunning, (character.Cunning + 5));
            if (action >= character.Cunning + 1)
            {
                Console.WriteLine($"{encounter.Name} has spotted {character.Name}!");
                Console.WriteLine("It charges and gets off a free attack!");
                encounter.Action(character);
                Battle(character, encounter);
                return;
            }
            else
            {
                character.Cunning++;
                Console.WriteLine($"{character.Name} managed to sneak away from the {encounter.Name}");
                Console.WriteLine($"{character.Name} has gained 1 point in cunning and is now {character.Cunning}.");
                Console.WriteLine("Press a button to continue");
                Console.ReadLine();
            }
        }

        public void Check(Player character)
        {
            if (character.Health <= 0)
            {
                Console.WriteLine();
                Console.WriteLine("-------Game over-------");
                Console.WriteLine("Press a button to go back to the main menu");
                Console.ReadLine();
                Console.Clear();
                Environment world = new Environment();
                world.MainMenu();
            }
        }

    }
}
