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
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método DELETE de la tabla Employee - Usuario: " + SessionClass.sessionUserName + " - Id: " + t.Id));
            string query = @"UPDATE Employee SET status = 0, lastUpdate = CURRENT_TIMESTAMP, userID = @userID
                             WHERE id = @id";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@id", t.Id);
            command.Parameters.AddWithValue("@userID", SessionClass.sessionUserID);
            try
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método DELETE de la tabla Employee ejecutado exitosamente"));
                return ExecuteBasicCommand(command);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | ERROR en el Método DELETE de la tabla Employee  - ERROR: " + ex.Message));
                throw ex;
            }
        }

        public int Insert(Employee t)
        {
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método INSERT de la tabla Employee - Usuario: " + SessionClass.sessionUserName));
            TownImpl townImpl = new TownImpl();
            string query = @"INSERT INTO Employee(username, passwordHash, firstName, lastName, secondLastName, CI, phones, address, gender, birthDate, userID, role, townID, email, changePassword, photo)
                             VALUES(@Username, HASHBYTES('md5', @Password), @FirstName, @LastName, @SecondLastName, @CI, @Phones, @Address, @Gender, @BirthDate, @UserID, @Role, @TownID, @Email, 1, @Photo);";
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
            command.Parameters.AddWithValue("@Photo", t.Photo);
            try
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método INSERT de la tabla Employee ejecutado exitosamente"));
                return ExecuteBasicCommand(command);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | ERROR en el Método INSERT de la tabla Employee  - ERROR: " + ex.Message));
                throw ex;
            }
        }

        public Employee Get(byte id)
        {
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método GET de la tabla Employee - Usuario: " + SessionClass.sessionUserName));
            Employee t = null;
            string query = @"SELECT E.id, E.phones, E.address, E.role, T.townName, E.email, E.photo
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
                        reader[5].ToString(),
                        reader[6].ToString()
                        );
                }
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método GET de la tabla Employee ejecutado exitosamente"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método GET de la tabla Employee ejecutado exitosamente"));
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
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método GET de la tabla Employee - Usuario: " + SessionClass.sessionUserName));
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
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método GET de la tabla Employee ejecutado exitosamente"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | ERROR en el Método GET de la tabla Employee  - ERROR: " + ex.Message));
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
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método Exists de la tabla Employee - Usuario: " + SessionClass.sessionUserName));
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
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método Exists de la tabla Employee ejecutado exitosamente"));
                return exists;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | ERROR en el Método Exists de la tabla Employee  - ERROR: " + ex.Message));
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
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método Login de la tabla Employee - Usuario: " + SessionClass.sessionUserName));
            string query = @"SELECT E.id, E.userName, E.firstName, E.lastName, ISNULL(E.secondLastName, ''), E.CI, E.phones, E.address, E.gender, E.birthDate, E.role, T.townName, E.email, E.changePassword, E.photo
                             FROM Employee E
                             LEFT JOIN Town T ON T.id = E.townID
                             LEFT JOIN Province P ON T.provinceID = P.id
                             WHERE status = 1 AND userName = @userName AND passwordHash = HASHBYTES('md5', @password)";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@userName", userName);
            command.Parameters.AddWithValue("@password", password).SqlDbType = SqlDbType.VarChar;
            try
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método Login de la tabla Employee ejecutado exitosamente"));
                return ExecuteDataTableCommand(command);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | ERROR en el Método Login de la tabla ReplacementBrand  - ERROR: " + ex.Message));
                throw ex;
            }
        }

        public bool MatchesPassword(int id, string password)
        {
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método MatchesPassword de la tabla Employee - Usuario: " + SessionClass.sessionUserName));
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
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método MatchesPassword de la tabla Employee ejecutado exitosamente"));
                return matches;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | ERROR en el Método MatchesPassword de la tabla Employee  - ERROR: " + ex.Message));
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
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método SELECT de la tabla Employee - Usuario: " + SessionClass.sessionUserName));
            string query = @"SELECT E.photo AS 'photo', E.id, E.userName AS 'Nombre de Usuario', E.firstName AS 'Nombre', E.lastName AS 'Primer Apellido', ISNULL(E.secondLastName, '') AS 'Segundo Apellido', E.email AS 'Email',
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
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método SELECT de la tabla Employee ejecutado exitosamente"));
                return ExecuteDataTableCommand(command);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | ERROR en el Método SELECT de la tabla Employee  - ERROR: " + ex.Message));
                throw ex;
            }
        }

        public DataTable Select(string employeeName)
        {
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método SELECT de la tabla Employee - Usuario: " + SessionClass.sessionUserName));
            string query = @"SELECT E.photo AS 'photo', E.id, E.userName AS 'Nombre de Usuario', E.firstName AS 'Nombre', E.lastName AS 'Primer Apellido', ISNULL(E.secondLastName, '') AS 'Segundo Apellido', E.email AS 'Email',
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
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método SELECT de la tabla Employee ejecutado exitosamente"));
                return ExecuteDataTableCommand(command);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | ERROR en el Método SELECT de la tabla Employee  - ERROR: " + ex.Message));
                throw ex;
            }
        }

        public int Update(Employee t)
        {
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método UPDATE de la tabla Employee - Usuario: " + SessionClass.sessionUserName));
            TownImpl townImpl = new TownImpl();
            string query = @"UPDATE Employee
                             SET role=@Role, email=@Email, townID=@TownID, address=@Address, phones=@Phones, lastUpdate=CURRENT_TIMESTAMP, userID=@userID, photo=@Photo
                             WHERE id=@id";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@UserID", SessionClass.sessionUserID);
            command.Parameters.AddWithValue("@Role", t.Role);
            command.Parameters.AddWithValue("@TownID", townImpl.Get(t.TownName).Id);
            command.Parameters.AddWithValue("@Email", t.Email);
            command.Parameters.AddWithValue("@Address", t.Address);
            command.Parameters.AddWithValue("@Phones", t.Phones);
            command.Parameters.AddWithValue("@id", t.Id);
            command.Parameters.AddWithValue("@Photo", t.Photo);
            try
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método UPDATE de la tabla Employee ejecutado exitosamente"));
                return ExecuteBasicCommand(command);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | ERROR en el Método UPDATE de la tabla Employee  - ERROR: " + ex.Message));
                throw ex;
            }
        }

        public int Update(int id, string password)
        {
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método UPDATE de la tabla Employee - Usuario: " + SessionClass.sessionUserName));
            string query = @"UPDATE Employee
                             SET passwordHash = HASHBYTES('md5',@Password), changePassword=0
                             WHERE id = @id";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@Password", password).SqlDbType = SqlDbType.VarChar;
            command.Parameters.AddWithValue("@id", id);

            try
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método UPDATE de la tabla Employee ejecutado exitosamente"));
                return ExecuteBasicCommand(command);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | ERROR en el Método UPDATE de la tabla Employee  - ERROR: " + ex.Message));
                throw ex;
            }
        }
        public int Update(string email, string password)
        {
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método UPDATE de la tabla Employee - Usuario: " + SessionClass.sessionUserName));
            string query = @"UPDATE Employee
                             SET passwordHash = HASHBYTES('md5',@Password), changePassword=1
                             WHERE email = @Email";

            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@Password", password).SqlDbType = SqlDbType.VarChar;
            command.Parameters.AddWithValue("@Email", email);

            try
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método UPDATE de la tabla ReplacementBrand ejecutado exitosamente"));
                return ExecuteBasicCommand(command);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | ERROR en el Método UPDATE de la tabla Employee  - ERROR: " + ex.Message));
                throw ex;
            }
        }
    }
}
