using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expresso.Interfaces;
using Expresso.Model;

namespace Expresso.Implementation
{
    public class ClientImpl : BaseImpl, IClient
    {
        public int Delete(Client t)
        {
            string query = @"UPDATE Client SET status = 0, lastUpdate = CURRENT_TIMESTAMP, userID = @userID
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

        public bool Exists(string nit)
        {
            bool exists = false;
            string query = @"SELECT COUNT(*) FROM Client WHERE LOWER(NIT)=LOWER(@NIT) AND status=1";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@NIT", nit);
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

        public Client Get(string name)
        {
            throw new NotImplementedException();
        }

        public Client Get(byte id)
        {
            Client t = null;
            string query = @"SELECT C.id, C.name, C.NIT, T.townName
                             FROM Client C
                             LEFT JOIN Town T ON T.id = C.townID
                             WHERE status = 1 AND C.id = @id";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = null;
            try
            {
                reader = ExecuteDataReaderCommand(command);
                while (reader.Read())
                {

                    t = new Client(
                        int.Parse(reader[0].ToString()),
                        reader[1].ToString(),
                        reader[2].ToString(),
                        reader[3].ToString());
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

        public int Insert(Client t)
        {
            TownImpl townImpl = new TownImpl();
            string query = @"INSERT INTO Client(NIT, name, townID, userID) VALUES(@NIT, @Name, @TownID, @UserID);";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@NIT", t.NIT);
            command.Parameters.AddWithValue("@Name", t.Name);
            command.Parameters.AddWithValue("@UserID", SessionClass.sessionUserID);
            command.Parameters.AddWithValue("@TownID", townImpl.Get(t.TownName).Id);

            try
            {
                return ExecuteBasicCommand(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Select()
        {
            string query = @"SELECT C.id, C.name AS 'Nombre\Razon Social', C.NIT AS 'NIT', T.townName AS 'Ciudad', (SELECT COUNT(*) FROM [DBExpresso].[dbo].[Order] O WHERE O.clientID = C.id) AS 'Cantidad de Compras'
                             FROM Client C
                             LEFT JOIN Town T ON T.id = C.townID
                             WHERE status=1;";
            SqlCommand command = CreateBasicCommand(query);
            try
            {
                return ExecuteDataTableCommand(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Select(string name)
        {
            string query = @"SELECT C.id, C.name AS 'Nombre\Razon Social', C.NIT AS 'NIT', T.townName AS 'Ciudad', (SELECT COUNT(*) FROM [DBExpresso].[dbo].[Order] O WHERE O.clientID = C.id) AS 'Cantidad de Compras'
                             FROM Client C
                             LEFT JOIN Town T ON T.id = C.townID
                             WHERE status=1 AND C.name LIKE @Name";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@Name", "%" + name + "%");
            try
            {
                return ExecuteDataTableCommand(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Update(Client t)
        {
            TownImpl townImpl = new TownImpl();
            string query = @"UPDATE Client
                             SET name=@Name, NIT=@NIT, townID=@TownID, lastUpdate=CURRENT_TIMESTAMP, userID=@userID
                             WHERE id=@id";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@UserID", SessionClass.sessionUserID);
            command.Parameters.AddWithValue("@Name", t.Name);
            command.Parameters.AddWithValue("@TownID", townImpl.Get(t.TownName).Id);
            command.Parameters.AddWithValue("@NIT", t.NIT);
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
    }
}
