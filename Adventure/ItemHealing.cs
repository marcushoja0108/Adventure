using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    internal class ItemHealing : Item
    {
        public int HealAmount { get; }
        public ItemHealing(string id, string name, int healAmount, int price)
        {
            Id = id;
            Name = name;
            HealAmount = healAmount;
            Price = price;
        }

        public override void UseItem(Player character)
        {
            Console.WriteLine();
            Console.WriteLine($"Are you sure you want to use {Name} to heal for {HealAmount} hp?");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");
            var userinput = Console.ReadLine();
            if (userinput == "1")
            {
                Heal(character);
                character.Inventory.Remove(this);
                character.ShowInventory();
            }
            else if (userinput == "2")
            {
                return;
            }
            else
            {
                Console.WriteLine("Not a valid input.");
            }
        }

        public void Heal(Player character)
        {
            int healPossible = character.MaxHealth - character.Health;
            if (healPossible < HealAmount)
            {
                character.Health = character.MaxHealth;
                Console.WriteLine($"You have healed for {healPossible} hp!");
            }
            else
            {
                character.Health += HealAmount;
                Console.WriteLine($"You have healed for {HealAmount} hp!");
            }
        }

        public override void Show()
        {
            Console.WriteLine();
            Console.WriteLine($"Id: {Id}    Name: {Name}    Price: {Price} gold    Heals for {HealAmount} hp");
        }
    }
}
