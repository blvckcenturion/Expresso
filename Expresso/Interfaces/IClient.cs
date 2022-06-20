using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expresso.Model;

namespace Expresso.Interfaces
{
    public interface IClient: IBaseInterface<Client>
    {
        Client Get(string name);
        bool Exists(string nit);

        DataTable Select(string name);
    }
}
