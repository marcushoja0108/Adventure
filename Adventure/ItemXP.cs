using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    internal class ItemXP : Item
    {
        public int ExperienceAmount { get; }
        public int Price { get; }

        public ItemXP(string id, string name, int experienceAmount, int price)
        {
            Id = id;
            Name = name;
            ExperienceAmount = experienceAmount;
            Price = price;
        }
        public override void UseItem(Player character)
        {
            Console.WriteLine();
            Console.WriteLine($"Are you sure you want to use {Name} to increase XP by {ExperienceAmount}?");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");
            string userInput = Console.ReadLine();
            if (userInput == "1")
            {
                Console.WriteLine($"You have gained {ExperienceAmount} XP!");
                character.ExperienceGain(ExperienceAmount);
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
            Console.WriteLine($"Id: {Id}    Name: {Name}    Price: {Price} gold    Increases XP by {ExperienceAmount}");
        }
    }
}
