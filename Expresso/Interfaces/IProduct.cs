using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expresso.Model;

namespace Expresso.Interfaces
{
    public interface IProduct: IBaseInterface<Product>
    {
        int Insert(Product product, List<ProductSize> productSizes);
        bool Exists(string productName);
        DataTable Select(string productName);
    }
}
