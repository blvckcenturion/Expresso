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
    public class ProductSizeImpl : BaseImpl, IProductSize
    {
        public int Delete(ProductSize t)
        {
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método DELETE de la tabla ProductSize - Usuario: " + SessionClass.sessionUserName + " - Id: " + t.Id));
            string query = @"UPDATE ProductSize SET status = 0, lastUpdate = CURRENT_TIMESTAMP, userID = @userID
                             WHERE id = @id";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@id", t.Id);
            command.Parameters.AddWithValue("@userID", SessionClass.sessionUserID);
            try
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método DELETE de la tabla ProductSize ejecutado exitosamente"));
                return ExecuteBasicCommand(command);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | ERROR en el Método DELETE de la tabla ProductSize  - ERROR: " + ex.Message));
                throw ex;
            }
        }

        public ProductSize Get(byte id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string name, byte id)
        {
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método Exists de la tabla ProductSize - Usuario: " + SessionClass.sessionUserName));
            bool exists = false;
            string query = @"SELECT count(id) FROM ProductSize WHERE LOWER(denomination)=LOWER(@Name) AND status=1 AND productID=@ProductID";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@ProductID", id);
            SqlDataReader reader = null;
            try
            {
                reader = ExecuteDataReaderCommand(command);
                while (reader.Read())
                {
                    if (byte.Parse(reader[0].ToString()) != 0) exists = true;
                }
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método Exists de la tabla ProductSize ejecutado exitosamente"));
                return exists;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | ERROR en el Método Exists de la tabla ProductSize  - ERROR: " + ex.Message));
                throw ex;
            }
            finally
            {
                command.Connection.Close();
                reader.Close();
            }
        }

        public int Insert(ProductSize t)
        {
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método INSERT de la tabla ProductSize - Usuario: " + SessionClass.sessionUserName));
            string query = @"INSERT INTO ProductSize(denomination, basePrice, userID, productID)
                              VALUES(@denomination, @basePrice, @userID, @productID)";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@denomination", t.Name);
            command.Parameters.AddWithValue("@basePrice", t.BasePrice);
            command.Parameters.AddWithValue("@productID", t.ProductId);
            command.Parameters.AddWithValue("@userID", SessionClass.sessionUserID);
            try
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método INSERT de la tabla ProductSize ejecutado exitosamente"));
                return ExecuteBasicCommand(command);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | ERROR en el Método INSERT de la tabla ProductSize  - ERROR: " + ex.Message));
                throw ex;
            }
        }

        public DataTable Select()
        {
            throw new NotImplementedException();
        }

        public DataTable Select(byte id)
        {
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método SELECT de la tabla ProductSize - Usuario: " + SessionClass.sessionUserName));
            string query = @"SELECT id, denomination AS 'Denominacion', basePrice AS 'Precio'
                             FROM ProductSize
                             WHERE productID=@id AND status=1";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@id", id);
            try
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método SELECT de la tabla ProductSize ejecutado exitosamente"));
                return ExecuteDataTableCommand(command);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | ERROR en el Método SELECT de la tabla ProductSize  - ERROR: " + ex.Message));
                throw ex;
            }
        }

        public int Update(ProductSize t)
        {
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método UPDATE de la tabla ProductSize - Usuario: " + SessionClass.sessionUserName));
            string query = @"UPDATE ProductSize
                             SET denomination=@Denomination, basePrice=@BasePrice,userID=@userID, lastUpdate=CURRENT_TIMESTAMP
                             WHERE id = @id";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@UserID", SessionClass.sessionUserID);
            command.Parameters.AddWithValue("@Denomination", t.Name);
            command.Parameters.AddWithValue("@id", t.Id);
            command.Parameters.AddWithValue("@BasePrice", t.BasePrice);

            try
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método UPDATE de la tabla ProductSize ejecutado exitosamente"));
                return ExecuteBasicCommand(command);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | ERROR en el Método UPDATE de la tabla ProductSize  - ERROR: " + ex.Message));
                throw ex;
            }
        }
    }
}
