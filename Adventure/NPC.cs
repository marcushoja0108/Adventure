using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    internal class NPC : IGameCharacter
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Strength { get; set; }
        public int ExperienceGain { get; set; }
        public int Level { get; set; }

        public int Stamina { get; set; }
        public int MaxStamina { get; set; }

        public ConsoleColor Color { get; set; }
        public Item LootItem { get; set; }
        public int LootGold { get; set; }

        public List<Item> PossibleLootItems { get; set; }

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

        public void Action(IGameCharacter target)
        {
            Console.ForegroundColor = Color;
            if (Stamina > 0)
            {
                Attack(target);
            }
            else if (Stamina <= 0)
            {
                Rest();
            }
            Console.ResetColor();
        }

        private void Rest()
        {
            Stamina = MaxStamina;
            Health = Health + Level;

            Console.WriteLine($"{Name} rested and regained {Stamina} stamina and {Level} health.");
        }

        public void LootNPC(Player character)
        {
            Random loot = new Random();

            switch (loot.Next(0, 3))
            {
                case 0:
                    Console.WriteLine($"You found {LootItem} on {Name}");
                    character.Loot(LootItem);
                    break;
                case 1:
                    Console.WriteLine($"You found {LootGold} gold on the {Name}");
                    break;
                case 2:
                    Console.WriteLine($"You found nothing of value on {Name}");
                    break;
            }
        }
    }
}
