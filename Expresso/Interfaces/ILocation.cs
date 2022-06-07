using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expresso.Model;

namespace Expresso.Interfaces
{
    public interface ILocation: IBaseInterface<Location>
    {
        bool Exists(string name);
    }
}
