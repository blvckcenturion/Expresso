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
            string query = @"UPDATE Employee SET status = 0, lastUpdate = CURRENT_TIMESTAMP, userID = @userID
                             WHERE id = @id";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@id", t.Id);
            command.Parameters.AddWithValue("@userID", SessionClass.sessionUserID);
            try
            {
                return ExecuteBasicCommand(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Insert(Employee t)
        {
            TownImpl townImpl = new TownImpl();
            string query = @"INSERT INTO Employee(username, passwordHash, firstName, lastName, secondLastName, CI, phones, address, gender, birthDate, userID, role, townID, email)
                             VALUES(@Username, HASHBYTES('md5', @Password), @FirstName, @LastName, @SecondLastName, @CI, @Phones, @Address, @Gender, @BirthDate, @UserID, @Role, @TownID, @Email);";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@Username", t.UserName);
            command.Parameters.AddWithValue("@Password", t.Password).SqlDbType = SqlDbType.VarChar;
            command.Parameters.AddWithValue("@FirstName", t.FirstName);
            command.Parameters.AddWithValue("@LastName", t.LastName);
            command.Parameters.AddWithValue("@SecondLastName", t.SecondLastName);
            command.Parameters.AddWithValue("@CI", t.CI);
            command.Parameters.AddWithValue("@Phones", t.Phones);
            command.Parameters.AddWithValue("@Gender", t.Gender);
            command.Parameters.AddWithValue("@BirthDate", t.BirthDate.Year + "-" + t.BirthDate.Month + "-" + t.BirthDate.Day);
            command.Parameters.AddWithValue("@UserID", SessionClass.sessionUserID);
            command.Parameters.AddWithValue("@Role", t.Role);
            command.Parameters.AddWithValue("@TownID", townImpl.Get(t.TownName).Id);
            command.Parameters.AddWithValue("@Email", t.Email);
            command.Parameters.AddWithValue("@Address", t.Address);
            try
            {
                return ExecuteBasicCommand(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Employee Get(byte id)
        {
            Employee t = null;
            string query = @"SELECT E.id, E.phones, E.address, E.role, T.townName, E.email
                             FROM Employee E
                             LEFT JOIN Town T ON T.id = E.townID
                             LEFT JOIN Province P ON T.provinceID = P.id
                             WHERE status = 1 AND E.id = @id";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = null;
            try
            {
                reader = ExecuteDataReaderCommand(command);
                while (reader.Read())
                {

                    t = new Employee(
                        byte.Parse(reader[0].ToString()),
                        reader[1].ToString(),
                        reader[2].ToString(),
                        reader[3].ToString(),
                        reader[4].ToString(),
                        reader[5].ToString()
                        );
                }
            }
            catch (Exception ex)
            {
                //Log
                throw ex;
            }
            finally
            {
                command.Connection.Close();
                reader.Close();
            }
            return t;
        }

        public Employee Get(string email)
        {
            Employee t = null;
            string query = @"SELECT E.firstName, E.lastName, e.birthDate, e.role, e.username
                             FROM Employee E
                             WHERE status = 1 AND E.email = @email";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@email", email);
            SqlDataReader reader = null;
            try
            {
                reader = ExecuteDataReaderCommand(command);
                while (reader.Read())
                {
                    t = new Employee(
                        reader[0].ToString(),
                        reader[1].ToString(),
                        DateTime.Parse(reader[2].ToString()),
                        reader[3].ToString(),
                        reader[4].ToString()
                        );
                }
            }
            catch (Exception ex)
            {
                //Log
                throw ex;
            }
            finally
            {
                command.Connection.Close();
                reader.Close();
            }
            return t;
        }



        public bool Exists(string email)
        {
            bool exists = false;
            string query = @"SELECT count(id) FROM Employee WHERE LOWER(email)=LOWER(@Email) AND status=1";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@Email", email);
            SqlDataReader reader = null;
            try
            {
                reader = ExecuteDataReaderCommand(command);
                while (reader.Read())
                {
                    if (byte.Parse(reader[0].ToString()) != 0) exists = true;
                }
                return exists;
            }
            catch (Exception ex)
            {
                //Log
                throw ex;
            }
            finally
            {
                command.Connection.Close();
                reader.Close();
            }
        }


        public DataTable Login(string userName, string password)
        {
            string query = @"SELECT E.id, E.userName, E.firstName, E.lastName, ISNULL(E.secondLastName, ''), E.CI, E.phones, E.address, E.gender, E.birthDate, E.role, T.townName, E.email, E.changePassword
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

        public bool MatchesPassword(int id, string password)
        {
            bool matches = false;
            string query = @"SELECT count(id) FROM Employee WHERE passwordHash = HASHBYTES('md5', @Password) AND status=1 AND id = @Id";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@Password", password).SqlDbType = SqlDbType.VarChar;
            command.Parameters.AddWithValue("@Id", id);
            SqlDataReader reader = null;
            try
            {
                reader = ExecuteDataReaderCommand(command);
                while (reader.Read())
                {
                    if (byte.Parse(reader[0].ToString()) != 0) matches = true;
                }
                return matches;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Connection.Close();
                reader.Close();
            }
        }

        public DataTable Select()
        {
            string query = @"SELECT E.id, E.userName AS 'Nombre de Usuario', E.firstName AS 'Nombre', E.lastName AS 'Primer Apellido', ISNULL(E.secondLastName, '') AS 'Segundo Apellido', E.email AS 'Email',
                             E.CI AS 'Numero de Identificacion', E.phones AS 'Telefono', E.address AS 'Direccion', E.gender AS 'Genero', E.birthDate AS 'Fecha de Nacimiento',
                             E.role AS 'Rol de Usuario', P.provinceName AS 'Nombre de Provincia', T.townName AS 'Nombre de Ciudad'
                             FROM Employee E
                             LEFT JOIN Town T ON T.id = E.townID
                             LEFT JOIN Province P ON T.provinceID = P.id
							 WHERE E.id != @id AND status=1";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@id", SessionClass.sessionUserID);
            try
            {
                return ExecuteDataTableCommand(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Select(string employeeName)
        {
            string query = @"SELECT E.id, E.userName AS 'Nombre de Usuario', E.firstName AS 'Nombre', E.lastName AS 'Primer Apellido', ISNULL(E.secondLastName, '') AS 'Segundo Apellido', E.email AS 'Email',
                             E.CI AS 'Numero de Identificacion', E.phones AS 'Telefono', E.address AS 'Direccion', E.gender AS 'Genero', E.birthDate AS 'Fecha de Nacimiento',
                             E.role AS 'Rol de Usuario', P.provinceName AS 'Nombre de Provincia', T.townName AS 'Nombre de Ciudad'
                             FROM Employee E
                             LEFT JOIN Town T ON T.id = E.townID
                             LEFT JOIN Province P ON T.provinceID = P.id
							 WHERE E.id != @id AND status=1 AND E.firstName LIKE @EmployeeName";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@EmployeeName", "%" + employeeName + "%");
            command.Parameters.AddWithValue("@id", SessionClass.sessionUserID);
            try
            {
                return ExecuteDataTableCommand(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Update(Employee t)
        {
            TownImpl townImpl = new TownImpl();
            string query = @"UPDATE Employee
                             SET role=@Role, email=@Email, townID=@TownID, address=@Address, phones=@Phones, lastUpdate=CURRENT_TIMESTAMP, userID=@userID
                             WHERE id=@id";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@UserID", SessionClass.sessionUserID);
            command.Parameters.AddWithValue("@Role", t.Role);
            command.Parameters.AddWithValue("@TownID", townImpl.Get(t.TownName).Id);
            command.Parameters.AddWithValue("@Email", t.Email);
            command.Parameters.AddWithValue("@Address", t.Address);
            command.Parameters.AddWithValue("@Phones", t.Phones);
            command.Parameters.AddWithValue("@id", t.Id);
            try
            {
                return ExecuteBasicCommand(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Update(int id, string password)
        {
            string query = @"UPDATE Employee
                             SET passwordHash = HASHBYTES('md5',@Password), changePassword=0
                             WHERE id = @id";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@Password", password).SqlDbType = SqlDbType.VarChar;
            command.Parameters.AddWithValue("@id", id);

            try
            {
                return ExecuteBasicCommand(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Update(string email, string password)
        {
            string query = @"UPDATE Employee
                             SET passwordHash = HASHBYTES('md5',@Password), changePassword=1
                             WHERE email = @Email";

            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@Password", password).SqlDbType = SqlDbType.VarChar;
            command.Parameters.AddWithValue("@Email", email);

            try
            {
                return ExecuteBasicCommand(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
