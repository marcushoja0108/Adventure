using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    internal class Orc :NPC
    {
        public Orc(int level)
        {
            Name = GetRandomName();
            Level = GetRandomLevel(level);
            MaxHealth = GetMaxHealth();
            Health = MaxHealth;
            MaxStamina = GetMaxStamina();
            Stamina = MaxStamina;
            Strength = GetStrength();
            ExperienceGain = 35;
            Color = ConsoleColor.Red;
            PossibleLootItems = new List<Item>
            {
                new ItemHealing("2", "Medium healing potion", 10, 60),
                new ItemXP("11", "Medium sized book", 40, 150),
                new ItemHealing("1", "Lesser healing potion", 5, 30),
                new ItemStats("7", "Big plate of bacon", "Health", 2, 75),
                new ItemXP("10", "Small book", 20, 80),
            };
            LootGold = GetLootGold();
            LootItem = GetLootItem();
        }


        private string GetRandomName()
        {
            string[] orcNames = { "Big orc", "Drunk orc", "Injured orc", "Fat orc" };
            Random rand = new Random();
            string orcName = orcNames[rand.Next(0, orcNames.Length)];
            return orcName;
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
            }
            return newLevel;
        }

        private int GetMaxHealth()
        {
            int maxHealth = 8 + (4 * Level);
            return maxHealth;
        }

        private int GetMaxStamina()
        {
            int maxStamina = 2 + Level;
            return maxStamina;
        }

        private int GetStrength()
        {
            int strength = 3 + (3 * Level);
            return strength;
        }

        private int GetLootGold()
        {
            Random gold = new Random();

            return gold.Next(10, 50);
        }

        private Item GetLootItem()
        {
            Random rand = new Random();
            Item lootItem = PossibleLootItems[rand.Next(0, PossibleLootItems.Count)];
            return lootItem;
        }

    }
}
