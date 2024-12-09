using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    internal class Goblin : NPC
    {
        public Goblin(int level)
        {
            Name = GetRandomName();
            Level = GetRandomLevel(level);
            MaxHealth = GetMaxHealth();
            Health = MaxHealth;
            MaxStamina = GetMaxStamina();
            Stamina = MaxStamina;
            Strength = GetStrength();
            ExperienceGain = 20;
            Color = ConsoleColor.Red;
            LootGold = GetLootGold();
            LootItem = GetLootItem();
            PossibleLootItems = new List<Item>
            {
                new ItemMoney("4", "Small purse", 50),
                new ItemHealing("1", "Lesser healing potion", 5, 30),
                new ItemStats("9", "Cool sneakers", "Cunning", 1, 100),
                new ItemXP("10", "Small book", 20, 80),
                new ItemMoney("6", "Heavy chest", 400),
            };
        }


        private string GetRandomName()
        {
            string goblinName = null;
            Random rand = new Random();
            switch (rand.Next(0, 4))
            {
                case 0:
                    goblinName = "Angry Goblin";
                    break;
                case 1:
                    goblinName = "Hungry Goblin";
                    break;
                case 2:
                    goblinName = "Blind Goblin";
                    break;
                case 3:
                    goblinName = "Burning Goblin";
                    break;
            }
            return goblinName;
        }

        private int GetRandomLevel(int level)
        {
            int newLevel = level;
            Random rand = new Random();
            switch (rand.Next(0,3))
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
            int maxHealth = 6 + (3 * Level);
            return maxHealth;
        }

        private int GetMaxStamina()
        {
            int maxStamina = 3 + Level;
            return maxStamina;
        }

        private int GetStrength()
        {
            int strength = 2+ (2 * Level);
            return strength;
        }

        private int GetLootGold()
        {
            Random gold = new Random();

            return gold.Next(5, 25);
        }

        private Item GetLootItem()
        {
            Random rand = new Random();
            Item lootItem = PossibleLootItems[rand.Next(0, PossibleLootItems.Count)];
            return lootItem;
        }

    }
}
