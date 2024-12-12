using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    internal interface IGameCharacter
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Level { get; set; }

    }
}
