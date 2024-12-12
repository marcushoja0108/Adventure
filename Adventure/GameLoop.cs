using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Adventure;

namespace Adventure
{
    internal class GameLoop
    {
        List<NPC> _availableNpcs = new List<NPC>();
        List<Item> _availableItems = new List<Item>();
        Random _random = new Random();
        public void Loop(Player character)
        {
            bool playing = true;
            while (playing)
            {
                GenerateItems();
                GenerateEnemies(character);
                Console.Clear();
                int randomEvent = _random.Next(0, 4);
                character.Travelling();

                switch (randomEvent)
                {
                    case 0:
                        EnemyEncounter(character);
                        break;
                    case 1:
                        ItemEncounter(character);
                        break;
                    case 2:
                        CalmEncounter(character);
                        break;
                    case 3:
                        ShopEncounter(character);
                        break;
                }
            }
        }

        public void GenerateItems()
        {
            _availableItems.Add(new ItemHealing("1", "Lesser healing potion", 5, 30));
            _availableItems.Add(new ItemHealing("2", "Medium healing potion", 10, 60));
            _availableItems.Add(new ItemHealing("3", "Greater healing potion", 15, 90));
            _availableItems.Add(new ItemMoney("4", "Small purse", 50));
            _availableItems.Add(new ItemMoney("5", "Medium money bag", 125));
            _availableItems.Add(new ItemMoney("6", "Heavy chest", 400));
            _availableItems.Add(new ItemStats("7", "Big plate of bacon", "Health", 2, 75));
            _availableItems.Add(new ItemStats("8", "Tight V-neck T-shirt", "Strength", 1, 60));
            _availableItems.Add(new ItemStats("9", "Cool sneakers", "Cunning", 1, 100));
            _availableItems.Add(new ItemXP("10", "Small book", 20, 80));
            _availableItems.Add(new ItemXP("11", "Medium sized book", 40, 150));
            _availableItems.Add(new ItemXP("12", "Huge book", 80, 350));

        }
        public void GenerateEnemies(Player character)
        {
            _availableNpcs.Clear();
            _availableNpcs.Add(new Goblin(character.Level));
            _availableNpcs.Add(new Orc(character.Level));
            _availableNpcs.Add(new Troll(character.Level));
        }

        private void EnemyEncounter(Player character)
        {
            Random rand = new Random();
            NPC encounter = _availableNpcs[rand.Next(0, _availableNpcs.Count)];

            Console.WriteLine();
            Console.WriteLine($"{character.Name} met level {encounter.Level} {encounter.Name}.");
            bool done = false;
            while (done == false)
            {
                character.ShowEncounterOptions();
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
                    case "4":
                        character.ShowInventory();
                        break;
                    default:
                        Console.WriteLine("Not a valid command");
                        break;
                }
            }
        }


        public void Battle(Player character, NPC encounter)
        {
            bool fled = false;
            while (encounter.Health > 0 && fled == false)
            {
                Check(character);
                bool done = false;
                Console.ForegroundColor = character.Color;

                while (done == false)
                {
                    DisplayHealthBars(character, encounter);
                    character.ShowFightOptions();
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
                            fled = character.TryFlee(encounter);
                            done = true;
                            break;
                        case "4":
                            character.Show();
                            break;
                        case "5":
                            character.ShowInventory();
                            break;
                        default:
                            Console.WriteLine("Not a valid command.");
                            break;
                    }
                }
                Thread.Sleep(500);
                Console.ForegroundColor = encounter.Color;
                if (encounter.Health > 0 && fled == false) { encounter.Action(character); }
                else if (encounter.Health <= 0)
                {
                    Console.ForegroundColor = character.Color;
                    character.DefeatedList.Add(encounter);
                    Console.WriteLine($"{character.Name} has defeated {encounter.Name}.");
                    character.ExperienceGain(encounter.ExperienceGain);
                    encounter.LootNPC(character);
                }
                else 
                {
                    Console.WriteLine();
                    Console.ForegroundColor = character.Color;
                    character.ExperienceGain(encounter.ExperienceGain/2);
                };
            }
            Console.ResetColor();
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }
        public void DisplayHealthBars(Player character, NPC encounter)
        {
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine(character.Name);
            Console.WriteLine($"Health: {character.HpBar()}");
            Console.WriteLine();
            Console.ForegroundColor = encounter.Color;
            Console.WriteLine(encounter.Name);
            Console.WriteLine($"Health: {encounter.HpBar()}");
            Console.ForegroundColor = character.Color;
            Console.WriteLine("----------------------------------------------------------");
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
                encounter.Attack(character);
                Battle(character, encounter);
                return;
            }
            else
            {
                character.Cunning++;
                character.ExperienceGain(15);
                Console.WriteLine($"{character.Name} managed to sneak away from the {encounter.Name}");
                Console.WriteLine($"{character.Name} has gained 1 point in cunning and some XP.");
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
            }
        }

        public void Check(Player character)
        {
            if (character.Health <= 0)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("-------Game over-------");
                Console.WriteLine("Press enter to go back to the main menu");
                SaveGame(character);
                Console.ResetColor();
                Console.ReadLine();
                Console.Clear();
                Environment world = new Environment();
                world.MainMenu();
            }
        }

        public void ItemEncounter(Player character)
        {
            Console.WriteLine("You found something lying on the road.");
            Random randItem = new Random();
            Item itemEncounter = _availableItems[randItem.Next(0, _availableItems.Count)];
            Console.WriteLine($"It is a {itemEncounter.Name}. Pick it up?");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");

            bool done = false;
            while (done == false)
            {
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine($"You picked up {itemEncounter.Name}");
                        character.Inventory.Add(itemEncounter);
                        Console.WriteLine("Press enter to continue");
                        Console.ReadLine();
                        done = true;
                        break;
                    case "2":
                        Console.WriteLine("You continue walking.");
                        Console.WriteLine("Press enter to continue");
                        Console.ReadLine();
                        done = true;
                        break;
                    default:
                        Console.WriteLine("Not a valid command");
                        break;
                }

            }
        }

        public void CalmEncounter(Player character)
        {
            Console.WriteLine();
            Console.WriteLine("It is a calm day. Continuing on your journey without  much care");
            character.ExperienceGain(5);
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }

        public void ShopEncounter(Player character)
        {
            Console.WriteLine("You stumble upon a trader along your way and decide to take a look.");
            Shop shop = new Shop();
            Console.WriteLine($"Welcome to {shop.Name}!");
            bool done = false;
            while (done == false)
            {
                shop.Show();
                character.Show();

                switch (Console.ReadLine())
                {
                    case "1":
                        character.Buy(shop);
                        break;
                    case "2":
                        character.Sell(shop);
                        break;
                    case "3":
                        Console.WriteLine("Are you sure you want to leave?");
                        Console.WriteLine("1. Yes");
                        Console.WriteLine("2. No");
                        switch (Console.ReadLine())
                        {
                            case "1":
                                done = true;
                                break;
                        }
                        break;
                }
            }
        }

        public void SaveGame(Player character)
        {
            List<Player> characters = new List<Player>();
            if (File.Exists("deadCharacters.json"))
            {
                var json = File.ReadAllText("deadCharacters.json");
                characters = JsonSerializer.Deserialize<List<Player>>(json) ?? new List<Player>();
            }

            characters.Add(character);

            var updatedJson = JsonSerializer.Serialize(characters);
            File.WriteAllText("deadCharacters.json",updatedJson);
        }
    }
}
