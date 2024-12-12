using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    internal class Player : IGameCharacter
    {
        public string Name { get; set; }
        public string ClassName { get; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Strength { get; set; }
        public int Cunning { get; set; }
        public int Experience { get; set; }
        public int Level { get; set; }

        public int MaxLevel { get; set; }

        public int Stamina { get; set; }
        public int MaxStamina { get; set; }

        public List<NPC> DefeatedList;
        
        public List<Item> Inventory;

        public int Gold { get; set; }

        public ConsoleColor Color { get; }

        public Player(string name, string className, int maxHealth, int strength, int cunning, int level, int maxLevel, int maxStamina)
        {
            Name = name;
            ClassName = className;
            MaxHealth = maxHealth;
            Health = MaxHealth;
            Strength = strength;
            Cunning = cunning;
            Experience = 0;
            Level = level;
            MaxLevel = maxLevel;
            MaxStamina = maxStamina;
            Stamina = MaxStamina;
            DefeatedList = new List<NPC>();
            Inventory = new List<Item>();
            Gold = 500;
            Color = ConsoleColor.Green;

        }

        public string HpBar()
        {
            var hpBar = "";
            int blocks = Health * 10 / MaxHealth;
            for (var i = 0; i < blocks; i++)
            {
                if (i < blocks)
                {
                    hpBar += "\u258c";
                }
                else
                {
                    hpBar += "";
                }
            }

            if (Health < 0.2 * MaxHealth)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            else if (Health < 0.5 * MaxHealth)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
            return hpBar;
        }

        public void Attack(IGameCharacter target)
        {
            if (Stamina > 0)
            {
                double minDamage = Math.Round(Strength * 0.75);
                double maxDamage = Strength * 1.25;

                Random random = new Random();
                int damage = random.Next(Convert.ToInt32(minDamage), Convert.ToInt32(maxDamage)
                );
                target.Health = target.Health - damage;
                Stamina--;
                Console.WriteLine();
                Console.WriteLine($"{Name} hit {target.Name} for {damage} damage.");
                Console.WriteLine($"{target.Name} has {target.Health} health left.");
            }
            else
            {
                Console.WriteLine("No stamina left.");
                Rest();
            }
        }

        public void Rest()
        {
            if (Stamina != MaxStamina)
            {
                Console.WriteLine($"{Name} rested and regained {MaxStamina - Stamina} stamina");
                Stamina = MaxStamina;
            }
            else if (Stamina == MaxStamina && Health != MaxHealth)
            {
                int healPossible = MaxHealth - Health;
                if (healPossible < MaxStamina)
                {
                    Health = MaxHealth;
                    Console.WriteLine($"{Name} is already fully rested and treated their wounds for {healPossible} hp.");
                }
                else
                {
                    Health += MaxStamina;
                    Console.WriteLine($"{Name} is already fully rested and treated their wounds for {MaxStamina} hp.");
                }
            }
            else
            {
                Console.WriteLine($"{Name} is both rested and healed and spent some time training");
                ExperienceGain(10);
            }
        }

        public bool TryFlee(NPC encounter)
        {
            Console.WriteLine($"You try to run away from the {encounter.Name}");
            double baseChance = Convert.ToDouble(Cunning) / (Convert.ToDouble(encounter.Stamina) + 1) * 100;

            Random random = new Random();
            int modifier = random.Next(-10, 11);
            double chance = Math.Clamp(baseChance + modifier, 0, 100);

            int roll = random.Next(0, 101);
            bool success = roll < chance;
            if (success)
            {
                Console.WriteLine("You managed to get away!");
            }
            else
            {
                Console.WriteLine($"The {encounter.Name} catches up with you!");
            }
            return success;
        }

        public void ExperienceGain(int gain)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"You gained {gain} XP.");
            Experience += gain;
            if (Experience >= 100)
            {
                Experience = Experience - 100;
                LevelUp();
            }
            Console.ResetColor();
        }

        public void LevelUp()
        {
            if (Level < 10)
            {
                Level++;
                MaxHealth += 3;
                Strength += 2;
                MaxStamina++;
                Console.WriteLine("LEVEL UP");
                Console.WriteLine("Your stats have increased!");
                Console.WriteLine("Your health and stamina has also regenerated");
            }
            else
            {
                Console.WriteLine("You are already max level.");
            }

            Health = MaxHealth;
            Stamina = MaxStamina;
            Show();
        }

        public void ShowEncounterOptions()
        {
            Console.WriteLine();
            Console.WriteLine("1. Fight!");
            Console.WriteLine("2. Try to sneak away");
            Console.WriteLine("3. Show stats");
            Console.WriteLine("4. Show inventory");
        }

        

        public void ShowFightOptions()
        {
            Console.WriteLine();
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("1. Attack");
            Console.WriteLine("2. Rest");
            Console.WriteLine("3. Try to get away");
            Console.WriteLine("4. Check stats");
            Console.WriteLine("5. Show inventory");
        }

        public void Show()
        {
            Console.ForegroundColor = Color;
            Console.WriteLine();
            Console.WriteLine(Name);
            Console.WriteLine($"Gold: {Gold}");
            Console.WriteLine($"XP: {Experience}/100");
            Console.WriteLine($"Lvl: {Level}/{MaxLevel}");
            Console.WriteLine($"Health: {Health}/{MaxHealth}");
            Console.WriteLine($"Strength: {Strength}");
            Console.WriteLine($"Cunning: {Cunning}");
            Console.WriteLine($"Stamina: {Stamina}/{MaxStamina}");
            Console.WriteLine();
            Console.ResetColor();
        }

        public void ShowDefeatedEnemies()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Defeated enemies");
            Console.WriteLine("--------------------------------");
            foreach (var enemy in DefeatedList)
            {
                Console.ForegroundColor = enemy.Color;
                Console.WriteLine($"{enemy.Name}    Level: {enemy.Level}");
            }
            Console.ResetColor();
            Console.WriteLine("--------------------------------");
            Console.ResetColor();
        }

        public void ShowInventory()
        {
            bool done = false;
            while (done == false)
            {
                Console.ForegroundColor = Color;
                Console.WriteLine("Inventory");
                Console.WriteLine("---------------------------------");

                foreach (var item in Inventory)
                {
                    item.Show();
                }
                Console.WriteLine("---------------------------------");
                Console.ResetColor();

                Console.WriteLine("1. Use an item");
                Console.WriteLine("2. Back");
                switch (Console.ReadLine())
                {
                    case "1":
                        UseItem();
                        break;
                    case "2":
                        done = true;
                        break;
                    default:
                        Console.WriteLine("Not a valid command");
                        break;
                }
            }
        }

        public void UseItem()
        {
            Console.WriteLine("Type the ID of the item you want to use.");
            string inputId = Console.ReadLine();
            Item selectedItem = Inventory.FirstOrDefault(item => item.Id == inputId);

            if (selectedItem == null)
            {
                Console.WriteLine("No item with that ID found");
            }
            else
            {
                selectedItem.UseItem(this);
            }
        }

        public void Sell(Shop shop)
        {
            if (Inventory.Count <= 0)
            {
                Console.WriteLine("You have nothing to sell!");
                return;
            }

            foreach (var item in Inventory)
            {
                item.Show();
            }
            Console.WriteLine();
            Console.WriteLine("Type the ID of the Item you want to sell");
            string userInput = Console.ReadLine().ToLower();
            Item itemSelect = Inventory.First(item => item.Id == userInput);

            if (itemSelect != null)
            {
                SellItem(itemSelect, shop);
            }

            else
            {
                Console.WriteLine("Not a valid command.");
            }
        }

        public void SellItem(Item item, Shop shop)
        {
            if (shop.Gold > item.Price)
            {
                Inventory.Remove(item);
                shop.ShopInventory.Add(item);
                Gold += item.Price;
                shop.Gold -= item.Price;
                Console.WriteLine($"You have sold {item.Name} for {item.Price} gold.");
            }
            else
            {
                Console.WriteLine("trader does not have enough money");
            }
        }

        public void Buy(Shop shop)
        {
            Console.WriteLine("Type the id of the item you want to buy");
            string userInput = Console.ReadLine();
            Item itemSelect = shop.ShopInventory.FirstOrDefault(item => item.Id == userInput);

            if (itemSelect != null)
            {
                if (itemSelect.Price < Gold)
                {
                    Inventory.Add(itemSelect);
                    shop.ShopInventory.Remove(itemSelect);
                    shop.Gold += itemSelect.Price;
                    Gold -= itemSelect.Price;
                    Console.WriteLine($"You have bought {itemSelect.Name} for {itemSelect.Price}!");
                    double newPrice = Math.Round(itemSelect.Price * 0.9);
                    itemSelect.Price = Convert.ToInt32(newPrice);
                }
                else
                {
                    Console.WriteLine("You don't have enough money.");
                }
            }
            else
            {
                Console.WriteLine("No item with that ID");
            }
            Thread.Sleep(700);
        }

        public void Travelling()
        {
            Rest();
            bool done = false;
            while (done == false)
            {
                ShowTravellingOptions();
                switch (Console.ReadLine())
                {
                    case "1":
                        done = true;
                        break;
                    case "2":
                        ShowInventory();
                        break;
                    case "3":
                        Show();
                        ShowDefeatedEnemies();
                        break;
                }
            }
            Console.Clear();
        }

        public void ShowTravellingOptions()
        {
            Console.WriteLine("1. Continue on your journey");
            Console.WriteLine("2. Show inventory");
            Console.WriteLine("3. Stats");
        }
        public void Loot(Item LootItem)
        {
            Console.WriteLine($"Do you wish to pick up {LootItem.Name}?");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");
            bool done = false;
            while (done == false)
            {
                switch (Console.ReadLine())
                {
                    case "1":
                        Inventory.Add(LootItem);
                        Console.WriteLine($"{LootItem.Name} added to your inventory!");
                        done = true;
                        break;
                    case "2":
                        Console.WriteLine("You leave the item on the ground.");
                        done = true;
                        break;
                    default:
                        Console.WriteLine("Not a command");
                        break;
                }
            }
        }
    }
}
