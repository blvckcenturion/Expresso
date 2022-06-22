using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expresso.Model
{
    public class ProductSize : BaseTable
    {
        public byte Id { get; set; }
        public byte ProductId { get; set; }
        public string Name { get; set; }
        public float BasePrice { get; set; }

        public ProductSize(string name, float basePrice)
        {
            Name = name;
            BasePrice = basePrice;
        }

        public ProductSize(byte id, string name, float basePrice, byte productID)
        {
            Id = id;
            Name = name;
            BasePrice = basePrice;
            ProductId = productID;
        }

        public ProductSize(byte productId, string name, float basePrice)
        {
            ProductId = productId;
            Name = name;
            BasePrice = basePrice;
        }
    }
}
