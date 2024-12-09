using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    internal abstract class Item
    {
        public string Id { get; set; }
        public string Name { get; set; }
 
        public int Price { get; set; }

        public abstract void UseItem(Player character);

        public abstract void Show();
    }
}
