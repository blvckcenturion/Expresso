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

        public int Insert(ProductCategory t)
        {
            string query = @"INSERT INTO ProductCategory(productCategoryName, productCategoryDescription, userID) 
                             VALUES(@productCategoryName, @productCategoryDescription, @userID)";
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
                             SET productCategoryName=@productCategoryName, productCategoryDescription=@productCategoryDescription, lastUpdate=CURRENT_TIMESTAMP, userID=@userID
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
