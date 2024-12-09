using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    internal class Troll : NPC
    {
            public Troll(int level)
            {
                Name = GetRandomName();
                Level = GetRandomLevel(level);
                MaxHealth = GetMaxHealth();
                Health = MaxHealth;
                MaxStamina = GetMaxStamina();
                Stamina = MaxStamina;
                Strength = GetStrength();
                ExperienceGain = 50;
                Color = ConsoleColor.Red;
                LootGold = GetLootGold();
                LootItem = GetLootItem();
                PossibleLootItems = new List<Item>
                {
                    new ItemHealing("2", "Medium healing potion", 10, 60),
                    new ItemXP("11", "Medium sized book", 40, 150),
                    new ItemStats("7", "Big plate of bacon", "Health", 2, 75),
                    new ItemXP("12", "Huge book", 80, 350),
                    new ItemStats("8", "Tight V-neck T-shirt", "Strength", 1, 60),
                    new ItemHealing("3", "Greater healing potion", 15, 90),
                    new ItemMoney("6", "Heavy chest", 400),
                    new ItemMoney("5", "Medium money bag", 125),

                };

        }

        private string GetRandomName()
            {
                string trollName = null;
                Random rand = new Random();
                switch (rand.Next(0, 4))
                {
                    case 0:
                        trollName = "Smelly troll";
                        break;
                    case 1:
                        trollName = "Tall troll";
                        break;
                    case 2:
                        trollName = "One eyed troll";
                        break;
                    case 3:
                        trollName = "Troll with a club";
                        break;
                }
                return trollName;
            }

            private int GetRandomLevel(int level)
            {
                int newLevel = level;
                Random rand = new Random();
                switch (rand.Next(0, 3))
                {
                    case 0:
                        newLevel = level + 1;
                        break;
                    case 1:
                        newLevel = level - 1;
                        break;
                    default:
                        break;
                }
                return newLevel;
            }

            private int GetMaxHealth()
            {
                int maxHealth = 10 + (6 * Level);
                return maxHealth;
            }

            private int GetMaxStamina()
            {
                int maxStamina = 1 + Level;
                return maxStamina;
            }

            private int GetStrength()
            {
                int strength = 4 + (5 * Level);
                return strength;
            }

            private int GetLootGold()
            {
                Random gold = new Random();

                return gold.Next(20, 90);
            }

            private Item GetLootItem()
            {
                Random rand = new Random();
                Item lootItem = PossibleLootItems[rand.Next(0, PossibleLootItems.Count)];
                return lootItem;
            }

    }
}

