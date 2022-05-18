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
