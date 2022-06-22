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
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método DELETE de la tabla ProductCategory - Usuario: " + SessionClass.sessionUserName + " - Id: " + t.Id));
            string query = @"UPDATE ProductCategory SET status = 0, lastUpdate = CURRENT_TIMESTAMP, userID = @userID
                             WHERE id = @id";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@id", t.Id);
            command.Parameters.AddWithValue("@userID", SessionClass.sessionUserID);
            try
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método DELETE de la tabla ProductCategory ejecutado exitosamente"));
                return ExecuteBasicCommand(command);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | ERROR en el Método DELETE de la tabla ProductCategory  - ERROR: " + ex.Message));
                throw ex;
            }
        }

        public bool Exists(string productCategoryName)
        {
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método Exists de la tabla ProductCategory - Usuario: " + SessionClass.sessionUserName));
            bool exists = false;
            string query = @"SELECT count(id) FROM ProductCategory WHERE LOWER(productCategoryName)=LOWER(@ProductCategoryName) AND status=1";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@ProductCategoryName", productCategoryName);
            SqlDataReader reader = null;
            try
            {
                reader = ExecuteDataReaderCommand(command);
                while (reader.Read())
                {
                    if (byte.Parse(reader[0].ToString()) != 0) exists = true;
                }
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método Exists de la tabla ProductCategory ejecutado exitosamente"));
                return exists;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | ERROR en el Método Exists de la tabla ProductCategory  - ERROR: " + ex.Message));
                throw ex;
            }
            finally
            {
                command.Connection.Close();
                reader.Close();
            }
        }

        public ProductCategory Get(byte id)
        {
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método GET de la tabla ProductCategory - Usuario: " + SessionClass.sessionUserName));
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
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método GET de la tabla ProductCategory ejecutado exitosamente"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método GET de la tabla ProductCategory ejecutado exitosamente"));
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
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método GET de la tabla ProductCategory - Usuario: " + SessionClass.sessionUserName));
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
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método GET de la tabla ProductCategory ejecutado exitosamente"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método GET de la tabla ProductCategory ejecutado exitosamente"));
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
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método INSERT de la tabla ProductCategory - Usuario: " + SessionClass.sessionUserName));
            string query = @"INSERT INTO ProductCategory(productCategoryName,productCategoryDescription, userID) 
                             VALUES(@productCategoryName,@productCategoryDescription, @userID)";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@productCategoryName", t.ProductCategoryName);
            command.Parameters.AddWithValue("@productCategoryDescription", t.ProductCategoryDescription);
            command.Parameters.AddWithValue("@userID", SessionClass.sessionUserID);
            try
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método INSERT de la tabla ProductCategory ejecutado exitosamente"));
                return ExecuteBasicCommand(command);
            } catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | ERROR en el Método INSERT de la tabla ProductCategory  - ERROR: " + ex.Message));
                throw ex;
            }
        }

        public DataTable Select()
        {
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método SELECT de la tabla ProductCategory - Usuario: " + SessionClass.sessionUserName));
            string query = @"SELECT id, productCategoryName AS 'Nombre de la Categoria', productCategoryDescription AS 'Descripcion de la Categoria', registerDate AS 'Fecha de Creacion'
                             FROM ProductCategory
                             WHERE status=1";
            SqlCommand command = CreateBasicCommand(query);
            try
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método SELECT de la tabla ProductCategory ejecutado exitosamente"));
                return ExecuteDataTableCommand(command);
            } catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | ERROR en el Método SELECT de la tabla ProductCategory  - ERROR: " + ex.Message));
                throw ex;
            }
        }

        public DataTable Select(string productCategoryName)
        {
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método SELECT de la tabla ProductCategory - Usuario: " + SessionClass.sessionUserName));
            string query = @"SELECT P.id, P.productCategoryName AS 'Nombre del Producto', P.productCategoryDescription AS 'Descripcion del Producto', P.registerDate AS 'Fecha de Creacion'
                             FROM ProductCategory P 
                             WHERE P.status = 1 AND P.productCategoryName LIKE LOWER(@ProductCategoryName)";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@ProductCategoryName", "%" + productCategoryName + "%");
            try
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método SELECT de la tabla ProductCategory ejecutado exitosamente"));
                return ExecuteDataTableCommand(command);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | ERROR en el Método SELECT de la tabla ProductCategory  - ERROR: " + ex.Message));
                throw ex;
            }
        }

        public int Update(ProductCategory t)
        {
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método UPDATE de la tabla ProductCategory - Usuario: " + SessionClass.sessionUserName));
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
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método UPDATE de la tabla ProductCategory ejecutado exitosamente"));
                return ExecuteBasicCommand(command);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | ERROR en el Método UPDATE de la tabla ProductCategory  - ERROR: " + ex.Message));
                throw ex;
            }
        }
    }
}
