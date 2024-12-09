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

        }
    }

