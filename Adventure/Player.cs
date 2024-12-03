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
        public int Health { get; set; }

        public int MaxHealth { get; set; }
        public int Strength { get; set; }
        public int Cunning { get; set; }
        public int Level { get; set; }

        public int MaxLevel { get; set; }

        public int Stamina { get; set; }
        public int MaxStamina { get; set; }

        public List<NPC> _defeatedList;

        public Player(string name, int maxHealth, int strength, int cunning, int level, int maxLevel, int maxStamina)
        {
            Name = name;
            MaxHealth = maxHealth;
            Health = MaxHealth;
            Strength = strength;
            Cunning = cunning;
            Level = level;
            MaxLevel = maxLevel;
            MaxStamina = maxStamina;
            Stamina = MaxStamina;
            _defeatedList = new List<NPC>();

        }
        public void Attack(IGameCharacter target)
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

        public void Rest()
        {
            Stamina = MaxStamina;
            Health = Health + Level;

            Console.WriteLine($"{Name} rested and regained {Stamina} stamina and {Level} health");
        }

        public void LevelUp()
        {
            if (Level < 10)
            {
                Level++;
                MaxHealth = MaxHealth + 5;
                Strength = Strength + 5;
                MaxStamina++;
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
        }

        

        public void ShowFightOptions()
        {
            Console.WriteLine();
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("1. Attack");
            Console.WriteLine("2. Rest");
            Console.WriteLine("3. Check stats");
        }

        public void Show()
        {
            Console.WriteLine();
            Console.WriteLine(Name);
            Console.WriteLine($"Lvl: {Level}/{MaxLevel}");
            Console.WriteLine($"Health: {Health}/{MaxHealth}");
            Console.WriteLine($"Stamina: {Stamina}/{MaxStamina}");
        }
    }
}
