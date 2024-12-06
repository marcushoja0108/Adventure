using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    internal class ItemMoney : Item
    {
        public int Amount { get; }

        public ItemMoney( string id, string name, int amount)
        {
            Id = id;
            Name = name;
            Amount = amount;
        }

        public override void UseItem(Player character)
        {
            Console.WriteLine();
            Console.WriteLine($"You open the container and find {Amount} gold pieces inside.");
            Console.WriteLine($"You scurry on like a loot goblin.");
            character.Gold += Amount;
            Console.WriteLine($"Gold: {character.Gold}");
        }

        public override void Show()
        {
            Console.WriteLine();
            Console.WriteLine($"Id: {Id}  {Name}");
        }
    }
}
