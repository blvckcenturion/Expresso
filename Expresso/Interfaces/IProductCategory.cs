using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expresso.Model;

namespace Expresso.Interfaces
{
    public interface IProductCategory:IBaseInterface<ProductCategory>
    {
        ProductCategory Get(byte id);
        bool Exists(string productCategoryName);
        ProductCategory Get(string productCategoryName);
        DataTable Select(string productCategoryName);
    }
}
