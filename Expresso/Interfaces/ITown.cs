using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expresso.Model;
using System.Data;
namespace Expresso.Interfaces
{
    public interface ITown
    {
        DataTable Select();
        Town Get(string townName);
    }
}
