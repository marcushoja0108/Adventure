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
            PossibleLootItems = new List<Item>
            {
                new ItemMoney("4", "Small purse", 50),
                new ItemHealing("1", "Lesser healing potion", 5, 30),
                new ItemStats("9", "Cool sneakers", "Cunning", 1, 100),
                new ItemXP("10", "Small book", 20, 80),
                new ItemMoney("6", "Heavy chest", 400),
            };
            LootItem = GetLootItem();
        }


        private string GetRandomName()
        {
            string[] goblinNames = {"Angry Goblin", "Hungry Goblin", "Blind Goblin", "Burning Goblin" };
            Random rand = new Random();
            string goblinName = goblinNames[rand.Next(0, goblinNames.Length)];
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
            var lootItem = PossibleLootItems[rand.Next(0, PossibleLootItems.Count)];
            return lootItem;
        }

    }
}
