using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expresso.Model
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public string NIT { get; set; }
        public byte TownID { get; set; }
        public string TownName { get; set; }

        public Client(string name, string nIT, string townName)
        {
            Name = name;
            NIT = nIT;
            TownName = townName;
        }

        public Client(int id, string name, string nIT, string townName)
        {
            Id = id;
            Name = name;
            NIT = nIT;
            TownName = townName;
        }
    }
}
