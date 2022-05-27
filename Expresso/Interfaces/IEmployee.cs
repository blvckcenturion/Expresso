using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expresso.Model;
using System.Data;

namespace Expresso.Interfaces
{
    public interface IEmployee: IBaseInterface<Employee>
    {
        DataTable Login(string username, string password);
        Employee Get(string email);
        bool Exists(string email);
        bool MatchesPassword(int id, string password);
        int Update(int id, string password);
        int Update(string email, string password);
    }
}