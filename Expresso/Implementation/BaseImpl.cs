using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Expresso.Implementation
{
    public class BaseImpl
    {
        static string connectionString = @"Server=DESKTOP-OQV4T2R\SQLEXPRESS;Database=DBExpresso;User Id=sa;Password=Santiago2001;";
        
        public SqlCommand CreateBasicCommand()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            return command;
        }
        public List<SqlCommand> CreateNBasicCommand(int n)
        {
            List<SqlCommand> res = new List<SqlCommand>();
            SqlConnection connection = new SqlConnection(connectionString);

            for (int i = 0; i < n; i++)
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                res.Add(command);
            }

            return res;
        }

        public SqlCommand CreateBasicCommand(string query)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);
            return command;
        }

        public int ExecuteBasicCommand(SqlCommand command)
        {
            try
            {
                command.Connection.Open();
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Connection.Close();
            }
        }

        public void ExecuteNBasicCommand(List<SqlCommand> commands)
        {
            SqlTransaction transaction = null;
            SqlCommand command1 = commands[0];
            try
            {
                command1.Connection.Open();
                transaction = command1.Connection.BeginTransaction();
                foreach (SqlCommand item in commands)
                {
                    item.Transaction = transaction;
                    item.ExecuteNonQuery();
                }
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                command1.Connection.Close();
            }
        }

        public  string GetIDGenerateTable(string tableName)
        {

            string query = "SELECT IDENT_CURRENT('" + tableName + "') + IDENT_INCR('" + tableName + "')";
            SqlCommand command = CreateBasicCommand(query);
            try
            {
                command.Connection.Open();
                return command.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Connection.Close();
            }

        }

        public SqlDataReader ExecuteDataReaderCommand(SqlCommand command)
        {
            SqlDataReader reader=null;
            try
            {
                command.Connection.Open();
                reader=command.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return reader;
        }

        public string GetGenerateIDTable(string tableName)
        {
            string query = @"SELECT IDENT_CURRENT('" + tableName +"') + IDENT_INCR('" + tableName + "')";
            SqlCommand command = CreateBasicCommand(query);
            try
            {
                command.Connection.Open();
                return command.ExecuteScalar().ToString();
            } catch(Exception ex)
            {
                throw ex;
            } finally
            {
                command.Connection.Close();
            }
        }


        public DataTable ExecuteDataTableCommand(SqlCommand command)
        {
            try
            {
                command.Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Connection.Close();
            }
        }

    }
}
