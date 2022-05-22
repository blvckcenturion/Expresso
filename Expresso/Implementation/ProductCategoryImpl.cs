using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expresso.Model;
using Expresso.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace Expresso.Implementation
{
    public class ProductCategoryImpl : BaseImpl, IProductCategory
    {
        public int Delete(ProductCategory t)
        {
            string query = @"UPDATE ProductCategory SET status = 0, lastUpdate = CURRENT_TIMESTAMP, userID = @userID
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

        public ProductCategory Get(byte id)
        {
            ProductCategory t = null;
            string query = @"SELECT id, productCategoryName, productCategoryDescription, status, registerDate, ISNULL(lastUpdate, CURRENT_TIMESTAMP), userID
                             FROM ProductCategory
                             WHERE id=@id";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = null;
            try
            {
                reader = ExecuteDataReaderCommand(command);
                while (reader.Read())
                {
                    t = new ProductCategory(byte.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), byte.Parse(reader[3].ToString()), DateTime.Parse(reader[4].ToString()), DateTime.Parse(reader[5].ToString()), int.Parse(reader[6].ToString()));
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

        public ProductCategory Get(string categoryName)
        {
            ProductCategory t = null;
            string query = @"SELECT id
                             FROM ProductCategory
                             WHERE productCategoryName=@ProductCategoryName";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@ProductCategoryName", categoryName);
            SqlDataReader reader = null;
            try
            {
                reader = ExecuteDataReaderCommand(command);
                while (reader.Read())
                {
                    t = new ProductCategory(byte.Parse(reader[0].ToString()));
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

        public int Insert(ProductCategory t)
        {
            string query = @"INSERT INTO ProductCategory(productCategoryName, userID) 
                             VALUES(@productCategoryName, @userID)";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@productCategoryName", t.ProductCategoryName);
            command.Parameters.AddWithValue("@productCategoryDescription", t.ProductCategoryDescription);
            command.Parameters.AddWithValue("@userID", SessionClass.sessionUserID);
            try
            {
                return ExecuteBasicCommand(command);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Select()
        {
            string query = @"SELECT id, productCategoryName AS 'Nombre de la Categoria', productCategoryDescription AS 'Descripcion de la Categoria', registerDate AS 'Fecha de Creacion'
                             FROM ProductCategory
                             WHERE status=1";
            SqlCommand command = CreateBasicCommand(query);
            try
            {
                return ExecuteDataTableCommand(command);
            } catch(Exception ex)
            {
                throw ex;
            }
        }

        public int Update(ProductCategory t)
        {
            string query = @"UPDATE ProductCategory
                             SET productCategoryName=@productCategoryName, lastUpdate=CURRENT_TIMESTAMP, userID=@userID
                             WHERE id=@id";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@productCategoryName", t.ProductCategoryName);
            command.Parameters.AddWithValue("@productCategoryDescription", t.ProductCategoryDescription);
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
