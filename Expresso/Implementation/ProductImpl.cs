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
            string query = @"SELECT P.id, P.basePrice, P.productName, P.productDescription, P.productCategoryID, P.status, PC.productCategoryName, P.registerDate, ISNULL(P.lastUpdate, CURRENT_TIMESTAMP), P.userID
                             FROM Product P
                             INNER JOIN ProductCategory PC on P.productCategoryID = PC.id
                             WHERE P.id=@id";
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
                        reader[6].ToString(),
                        DateTime.Parse(reader[7].ToString()),
                        DateTime.Parse(reader[8].ToString()),
                        int.Parse(reader[9].ToString())
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

        public bool Exists(string productName)
        {
            bool exists = false;
            string query = @"SELECT count(id) FROM Product WHERE LOWER(ProductName)=LOWER(@ProductName) AND status=1";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@productName", productName);
            SqlDataReader reader = null;
            try
            {
                reader = ExecuteDataReaderCommand(command);
                while (reader.Read())
                {
                    if(byte.Parse(reader[0].ToString()) != 0) exists = true;
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

        public int Insert(Product t)
        {
            ProductCategoryImpl productCategoryType = new ProductCategoryImpl();
            ProductCategory pc = productCategoryType.Get(t.ProductCategoryName);
            string query = @"INSERT INTO Product(productName, basePrice, productCategoryID, productDescription, userID)
                            VALUES(@productName, @basePrice, @productCategoryID, @productDescription, @userID)";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@productName", t.ProductName);
            command.Parameters.AddWithValue("@basePrice", t.BasePrice);
            command.Parameters.AddWithValue("@productCategoryId", pc.Id);
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
            string query = @"SELECT P.id, P.productName AS 'Nombre del Producto', P.productDescription AS 'Descripcion del Producto', P.basePrice AS 'Precio Base', P.productCategoryID, PC.productCategoryName AS 'Nombre de la Categoria', P.registerDate AS 'Fecha de Creacion'
                             FROM Product P 
							 INNER JOIN ProductCategory PC ON P.productCategoryID = PC.id
                             WHERE P.status=1";
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

        public DataTable Select(string productName) 
        {
            string query = @"SELECT P.id, P.productName AS 'Nombre del Producto', P.productDescription AS 'Descripcion del Producto', P.basePrice AS 'Precio Base', P.productCategoryID, PC.productCategoryName AS 'Nombre de la Categoria', P.registerDate AS 'Fecha de Creacion'
                             FROM Product P
                             INNER JOIN ProductCategory PC ON P.productCategoryID = PC.id
                             WHERE P.status = 1 AND P.productName LIKE LOWER(@productName)";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@productName", "%"+productName+ "%");
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
            ProductCategoryImpl productCategoryType = new ProductCategoryImpl();
            ProductCategory pc = productCategoryType.Get(t.ProductCategoryName);
            string query = @"UPDATE Product
                             SET productName=@productName, productDescription=@productDescription, basePrice=@basePrice,lastUpdate=CURRENT_TIMESTAMP, userID=@userID, productCategoryID=@productCategoryID
                             WHERE id=@id";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@productName", t.ProductName);
            command.Parameters.AddWithValue("@productDescription", t.ProductDescription);
            command.Parameters.AddWithValue("@basePrice", t.BasePrice);
            command.Parameters.AddWithValue("@productCategoryID", pc.Id);
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
