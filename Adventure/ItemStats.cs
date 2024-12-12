using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    internal class ItemStats : Item
    {
        public string StatToIncrease { get;}
        public int StatIncrease { get; }
        public int VendorPrice { get; }

        public ItemStats(string id, string name, string statToIncrease, int statIncrease, int price)
        {
            Id = id;
            Name = name;
            StatToIncrease = statToIncrease;
            StatIncrease = statIncrease;
            Price = price;
        }
        public override void UseItem(Player character)
        {
            Console.WriteLine();
            Console.WriteLine($"Are you sure you want to use {Name} to permanently increase {StatToIncrease} by {StatIncrease}?");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");
            string userInput = Console.ReadLine();
            if (userInput == "1")
            {
                if (StatToIncrease == "Health")
                {
                    character.MaxHealth += StatIncrease;
                    character.Health += StatIncrease;
                }
                else if (StatToIncrease == "Strength")
                {
                    character.Strength += StatIncrease;
                }
                else if (StatToIncrease == "Cunning")
                {
                    character.Cunning += StatIncrease;
                }
                Console.WriteLine($"{StatToIncrease} has increased by {StatIncrease}!");
                character.Inventory.Remove(this);
            }
            else if (userInput == "2")
            {
                return;
            }
            else
            {
                Console.WriteLine("Not a valid command");    
            }
        }

        public override void Show()
        {
            Console.WriteLine();
            Console.WriteLine($"Id: {Id}    Name: {Name}    Price: {Price} gold    Increases {StatToIncrease} by {StatIncrease}");
        }
    }
}
