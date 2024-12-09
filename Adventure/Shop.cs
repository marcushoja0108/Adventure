using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    internal class Shop
    {
        public string Name { get; }
        public int Gold { get; set; }
        public int ItemsAmount { get; }
        public List<Item> ShopInventory { get; set; }
        public List<Item> PossibleShopItems { get; set; }
        public ConsoleColor Color { get; }

        public Shop()
        {
            Name = GetRandomName();
            Gold = GetRandomGold();
            ItemsAmount = GetRandomItemAmount();
            Color = ConsoleColor.Yellow;
            PossibleShopItems = new List<Item>
         {
             new ItemHealing("1", "Lesser healing potion", 5, 30),
             new ItemHealing("2", "Medium healing potion", 10, 60),
             new ItemHealing("3", "Greater healing potion", 15, 90),
             new ItemStats("7", "Big plate of bacon", "Health", 2, 75),
             new ItemStats("8", "Tight V-neck T-shirt", "Strength", 1, 60),
             new ItemStats("9", "Cool sneakers", "Cunning", 1, 100),
             new ItemXP("10", "Small book", 20, 80),
             new ItemXP("11", "Medium sized book", 40, 150),
             new ItemXP("12", "Huge book", 80, 350)
         };
            ShopInventory = GetRandomItems();
        }

        public string GetRandomName()
        {
            Random rand = new Random();
            string name = "Bill";
            switch (rand.Next(0,4))
            {
                case 0:
                    name = "Swindle Sam's";
                    break;
                case 1:
                    name = "Griftin Greg's";
                    break;
                case 2:
                    name = "Discount Daisy's";
                    break;
                case 3:
                    name = "Bargain Betty's";
                    break;
            }
            return name;
        }

        public int GetRandomItemAmount()
        {
            Random rand = new Random();
            return rand.Next(3, 7);
        }

        public int GetRandomGold()
        {
            Random rand = new Random();
            return rand.Next(200, 500);
        }

        public List<Item> GetRandomItems()
        {
            List<Item> shopInventory = new List<Item>();
            Random rand = new Random();
            while (ItemsAmount > shopInventory.Count)
            {
                shopInventory.Add(PossibleShopItems[rand.Next(0, PossibleShopItems.Count)]);
            }
            return shopInventory;
        }

        public void Show()
        {
            Console.ForegroundColor = Color;
            Console.WriteLine();
            Console.WriteLine($"{Name}          Gold: {Gold}");
            Console.WriteLine("-----------------------------------------------------");
            foreach (var item in ShopInventory)
            {
                item.Show();
            }
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("1. Buy");
            Console.WriteLine("2. Sell");
            Console.WriteLine("3. Leave");
            Console.ResetColor();
        }
    }
}
