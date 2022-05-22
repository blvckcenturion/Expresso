using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expresso.Model
{
    public class Town
    {
        public byte Id { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// GET
        /// </summary>
        /// <param name="id"></param>
        public Town(byte id)
        {
            Id = id;
        }
    }
}
