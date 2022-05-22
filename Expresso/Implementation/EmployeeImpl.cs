using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Expresso.Interfaces;
using Expresso.Model;

namespace Expresso.Implementation
{
    public class EmployeeImpl : BaseImpl, IEmployee
    {
        public int Delete(Employee t)
        {
            throw new NotImplementedException();
        }

        public int Insert(Employee t)
        {
            throw new NotImplementedException();
        }

        public DataTable Login(string userName, string password)
        {
            string query = @"SELECT E.id, E.userName, E.firstName, E.lastName, ISNULL(E.secondLastName, ''), E.CI, E.phones, E.address, E.gender, E.birthDate, E.role, T.townName, E.email
                             FROM Employee E
                             LEFT JOIN Town T ON T.id = E.townID
                             LEFT JOIN Province P ON T.provinceID = P.id
                             WHERE status = 1 AND userName = @userName AND passwordHash = HASHBYTES('md5', @password)";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@userName", userName);
            command.Parameters.AddWithValue("@password", password).SqlDbType = SqlDbType.VarChar;
            try
            {
                return ExecuteDataTableCommand(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Select()
        {
            throw new NotImplementedException();
        }

        public int Update(Employee t)
        {
            throw new NotImplementedException();
        }
    }
}
