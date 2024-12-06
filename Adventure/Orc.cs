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
        }

        private string GetRandomName()
        {
            string orcName = null;
            Random rand = new Random();
            switch (rand.Next(0,4))
            {
                case 0:
                    orcName = "Big orc";
                    break;
                case 1:
                    orcName = "Drunk orc";
                    break;
                case 2:
                    orcName = "Injured orc";
                    break;
                case 3:
                    orcName = "Fat orc";
                    break;
            }

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
                default:
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

    }
}
