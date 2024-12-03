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
        public Goblin(int level) : base(level)
        {
            Name = GetRandomName();
            Level = GetRandomLevel(level);
            MaxHealth = GetMaxHealth();
            MaxStamina = GetMaxStamina();
            Strength = GetStrength();
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
                    newLevel = level + 2;
                    break;
                default:
                    break;
            }
            return newLevel;
        }

        private int GetMaxHealth()
        {
            int maxHealth = 6 + (5 * Level);
            return maxHealth;
        }

        private int GetMaxStamina()
        {
            int maxStamina = 3 + Level;
            return maxStamina;
        }

        private int GetStrength()
        {
            int strength = 2+ (3 * Level);
            return strength;
        }
    }
}
