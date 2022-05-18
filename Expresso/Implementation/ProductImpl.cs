using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expresso.Interfaces;
using Expresso.Model;
using System.Data.SqlClient;

namespace Expresso.Implementation
{
    public class ProductImpl : BaseImpl, IProduct
    {
        public int Delete(Product t)
        {
            string query = @"UPDATE Product SET status = 0, lastUpdate = CURRENT_TIMESTAMP, userID = @userID
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

        public Product Get(byte id)
        {
            Product t = null;
            string query = @"SELECT id, basePrice, productName, productDescription,productCategoryID, status, registerDate, ISNULL(lastUpdate, CURRENT_TIMESTAMP), userID
                             FROM Product
                             WHERE id=@id";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = null;
            try
            {
                reader = ExecuteDataReaderCommand(command);
                while (reader.Read())
                {
                    
                    t = new Product(
                        byte.Parse(reader[0].ToString()),
                        float.Parse(reader[1].ToString()),
                        reader[2].ToString(), 
                        reader[3].ToString(),
                        byte.Parse(reader[4].ToString()),
                        byte.Parse(reader[5].ToString()),
                        DateTime.Parse(reader[6].ToString()),
                        DateTime.Parse(reader[7].ToString()),
                        int.Parse(reader[8].ToString())
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

        public int Insert(Product t)
        {
            string query = @"INSERT INTO Product(productName, basePrice, productCategoryID, productDescription, userID)
                            VALUES(@productName, @basePrice, @productCategoryID, @productDescription, @userID)";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@productName", t.ProductName);
            command.Parameters.AddWithValue("@basePrice", t.BasePrice);
            command.Parameters.AddWithValue("@productCategoryId", t.ProductCategoryId);
            command.Parameters.AddWithValue("@productDescription", t.ProductDescription);
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

        public DataTable Select()
        {
            string query = @"SELECT id, productName AS 'Nombre del Producto', productDescription AS 'Descripcion del Producto', basePrice AS 'Precio Base', productCategoryID AS 'ID De la Categoria', registerDate AS 'Fecha de Creacion'
                             FROM Product
                             WHERE status=1";
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

        public int Update(Product t)
        {
            string query = @"UPDATE Product
                             SET productName=@productName, productDescription=@productDescription, basePrice=@basePrice,lastUpdate=CURRENT_TIMESTAMP, userID=@userID
                             WHERE id=@id";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@productName", t.ProductName);
            command.Parameters.AddWithValue("@productDescription", t.ProductDescription);
            command.Parameters.AddWithValue("@basePrice", t.BasePrice);
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
    }
}
