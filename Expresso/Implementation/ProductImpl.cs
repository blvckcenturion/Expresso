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
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método DELETE de la tabla Product - Usuario: " + SessionClass.sessionUserName + " - Id: " + t.Id));
            List<SqlCommand> commands = new List<SqlCommand>();
            string queryA = @"UPDATE Product SET status = 0, lastUpdate = CURRENT_TIMESTAMP, userID = @userID
                             WHERE id = @id";
            string queryB = @"UPDATE ProductSize SET status = 0, lastUpdate = CURRENT_TIMESTAMP, userID = @userID WHERE productID = @id";
            try
            {
                commands = CreateNBasicCommand(2);
                commands[0].CommandText = queryA;
                commands[0].Parameters.AddWithValue("@userID", SessionClass.sessionUserID);
                commands[0].Parameters.AddWithValue("@id", t.Id);
                commands[1].CommandText = queryB;
                commands[1].Parameters.AddWithValue("@userID", SessionClass.sessionUserID);
                commands[1].Parameters.AddWithValue("@id", t.Id);
                ExecuteNBasicCommand(commands);
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método DELETE de la tabla Product ejecutado exitosamente"));
                return 1;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | ERROR en el Método DELETE de la tabla Product  - ERROR: " + ex.Message));
                throw ex;
            }
        }

        public Product Get(byte id)
        {
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método GET de la tabla Product - Usuario: " + SessionClass.sessionUserName));
            Product t = null;
            string query = @"SELECT P.id, P.productName, P.productDescription, P.productCategoryID, P.status, PC.productCategoryName, P.registerDate, ISNULL(P.lastUpdate, CURRENT_TIMESTAMP), P.userID, P.photo
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
                        reader[1].ToString(), 
                        reader[2].ToString(),
                        byte.Parse(reader[3].ToString()),
                        byte.Parse(reader[4].ToString()),
                        reader[5].ToString(),
                        DateTime.Parse(reader[6].ToString()),
                        DateTime.Parse(reader[7].ToString()),
                        int.Parse(reader[8].ToString()),
                        reader[9].ToString()
                        );
                }
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método GET de la tabla Product ejecutado exitosamente"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | ERROR en el Método GET de la tabla Product  - ERROR: " + ex.Message));
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
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método Exists de la tabla Product - Usuario: " + SessionClass.sessionUserName));
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
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método Exists de la tabla Product ejecutado exitosamente"));
                return exists;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | ERROR en el Método Exists de la tabla Product  - ERROR: " + ex.Message));
                throw ex;
            }
            finally
            {
                command.Connection.Close();
                reader.Close();
            }
        }

        public int Insert(Product t, List<ProductSize> p)
        {
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método INSERT de la tabla Product - Usuario: " + SessionClass.sessionUserName));
            List<SqlCommand> commands = new List<SqlCommand>();
            ProductCategoryImpl productCategoryType = new ProductCategoryImpl();
            ProductCategory pc = productCategoryType.Get(t.ProductCategoryName);
            string queryT = @"INSERT INTO Product(productName, productCategoryID, productDescription, userID, photo)
                            VALUES(@productName, @productCategoryID, @productDescription, @userID, @productPhoto)";
            string queryP = @"INSERT INTO ProductSize(denomination, basePrice, userID, productID)
                              VALUES(@denomination, @basePrice, @userID, @productID)";
            try
            {
                commands = CreateNBasicCommand(1 + p.Count);
                int id = int.Parse(GetGenerateIDTable("Product"));
                for(int i = 0; i < commands.Count; i++)
                {
                    if(i == 0)
                    {
                        commands[i].CommandText = queryT;
                        commands[i].Parameters.AddWithValue("@productName", t.ProductName);
                        commands[i].Parameters.AddWithValue("@productCategoryId", pc.Id);
                        commands[i].Parameters.AddWithValue("@productDescription", t.ProductDescription);
                        commands[i].Parameters.AddWithValue("@productPhoto", t.Photo);
                        commands[i].Parameters.AddWithValue("@userID", SessionClass.sessionUserID);
                    } else
                    {
                        commands[i].CommandText = queryP;
                        commands[i].Parameters.AddWithValue("@denomination",p[i-1].Name);
                        commands[i].Parameters.AddWithValue("@basePrice", p[i - 1].BasePrice);
                        commands[i].Parameters.AddWithValue("@productID", id);
                        commands[i].Parameters.AddWithValue("@userID", SessionClass.sessionUserID);
                    }
                }
                ExecuteNBasicCommand(commands);
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método INSERT de la tabla Product ejecutado exitosamente"));
                return 1;
            } catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | ERROR en el Método INSERT de la tabla Product  - ERROR: " + ex.Message));
                throw ex;
            }
        }

        public DataTable Select()
        {
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método SELECT de la tabla Product - Usuario: " + SessionClass.sessionUserName));
            string query = @"SELECT P.photo as 'photo', P.id, P.productName AS 'Nombre del Producto', P.productDescription AS 'Descripcion del Producto', P.productCategoryID, PC.productCategoryName AS 'Nombre de la Categoria', P.registerDate AS 'Fecha de Creacion', 
							 (SELECT COUNT(*) FROM ProductSize Ps WHERE Ps.productID = P.id AND Ps.status=1) AS 'Variaciones de Producto',
							 (SELECT MIN(Ps.basePrice) FROM ProductSize Ps WHERE Ps.productID = P.id AND Ps.status=1) AS 'Precio Minimo',
							 (SELECT MAX(Ps.basePrice) FROM ProductSize Ps WHERE Ps.productID = P.id AND Ps.status=1) AS 'Precio Maximo'
                             FROM Product P 
							 INNER JOIN ProductCategory PC ON P.productCategoryID = PC.id
                             WHERE P.status=1";
            SqlCommand command = CreateBasicCommand(query);
            try
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método SELECT de la tabla Product ejecutado exitosamente"));
                return ExecuteDataTableCommand(command);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | ERROR en el Método SELECT de la tabla Product  - ERROR: " + ex.Message));
                throw ex;
            }
        }


        public DataTable Select(string productName) 
        {
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método SELECT de la tabla Employee - Usuario: " + SessionClass.sessionUserName));
            string query = @"SELECT P.photo as 'photo', P.id, P.productName AS 'Nombre del Producto', P.productDescription AS 'Descripcion del Producto', P.productCategoryID, PC.productCategoryName AS 'Nombre de la Categoria', P.registerDate AS 'Fecha de Creacion', 
							 (SELECT COUNT(*) FROM ProductSize Ps WHERE Ps.productID = P.id) AS 'Variaciones de Producto',
							 (SELECT MIN(Ps.basePrice) FROM ProductSize Ps WHERE Ps.productID = P.id) AS 'Precio Minimo',
							 (SELECT MAX(Ps.basePrice) FROM ProductSize Ps WHERE Ps.productID = P.id) AS 'Precio Maximo'
                             FROM Product P 
							 INNER JOIN ProductCategory PC ON P.productCategoryID = PC.id
                             WHERE P.status = 1 AND P.productName LIKE LOWER(@productName)";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@productName", "%"+productName+ "%");
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


        public int Update(Product t)
        {
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método UPDATE de la tabla Product - Usuario: " + SessionClass.sessionUserName));
            ProductCategoryImpl productCategoryType = new ProductCategoryImpl();
            ProductCategory pc = productCategoryType.Get(t.ProductCategoryName);
            string query = @"UPDATE Product
                             SET productName=@productName, productDescription=@productDescription,lastUpdate=CURRENT_TIMESTAMP, userID=@userID, productCategoryID=@productCategoryID, photo=@productPhoto
                             WHERE id=@id";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@productName", t.ProductName);
            command.Parameters.AddWithValue("@productDescription", t.ProductDescription);;
            command.Parameters.AddWithValue("@productCategoryID", pc.Id);
            command.Parameters.AddWithValue("@productPhoto", t.Photo);
            command.Parameters.AddWithValue("@id", t.Id);
            command.Parameters.AddWithValue("@userID", SessionClass.sessionUserID);

            try
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método UPDATE de la tabla Product ejecutado exitosamente"));
                return ExecuteBasicCommand(command);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | ERROR en el Método UPDATE de la tabla Product  - ERROR: " + ex.Message));
                throw ex;
            }
        }

        public int Insert(Product t)
        {
            throw new NotImplementedException();
        }
    }
}
