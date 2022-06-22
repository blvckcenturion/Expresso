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
    public class LocationImpl : BaseImpl, ILocation
    {
        public int Delete(Location t)
        {
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método DELETE de la tabla Location - Usuario: " + SessionClass.sessionUserName + " - Id: " + t.Id));
            string query = @"UPDATE Location SET status = 0, lastUpdate = CURRENT_TIMESTAMP, userID = @userID
                             WHERE id = @id";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@id", t.Id);
            command.Parameters.AddWithValue("@userID", SessionClass.sessionUserID);
            try
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método DELETE de la tabla Location ejecutado exitosamente"));
                return ExecuteBasicCommand(command);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | ERROR en el Método DELETE de la tabla Location  - ERROR: " + ex.Message));
                throw ex;

            }
        }

        public bool Exists(string name)
        {
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método Exists de la tabla Location - Usuario: " + SessionClass.sessionUserName + " - Id: " + name));
            bool exists = false;
            string query = @"SELECT count(id) FROM Location WHERE LOWER(locationName)=LOWER(@Name) AND status=1";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@Name", name);
            SqlDataReader reader = null;
            try
            {
                reader = ExecuteDataReaderCommand(command);
                while (reader.Read())
                {
                    if (byte.Parse(reader[0].ToString()) != 0) exists = true;
                }
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método Exists de la tabla Location ejecutado exitosamente"));
                return exists;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | ERROR en el Método Exists de la tabla Location  - ERROR: " + ex.Message));
                throw ex;
            }
            finally
            {
                command.Connection.Close();
                reader.Close();
            }
        }

        public Location Get(byte id)
        {
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método GET de la tabla Location - Usuario: " + SessionClass.sessionUserName));
            Location t = null;
            string query = @"SELECT L.id, L.photo, L.locationName, L.locationAddress, L.latitude, L.longitude, L.phoneNumber, T.townName
                             FROM Location L
                             LEFT JOIN Town T ON T.id = L.townID
                             LEFT JOIN Province P ON T.provinceID = P.id
                             WHERE L.status = 1 AND L.id = @id";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = null;
            try
            {
                reader = ExecuteDataReaderCommand(command);
                while (reader.Read())
                {

                    t = new Location(
                        int.Parse(reader[0].ToString()),
                        reader[2].ToString(),
                        reader[3].ToString(),
                        reader[6].ToString(),
                        reader[1].ToString(),
                        double.Parse(reader[4].ToString()),
                        double.Parse(reader[5].ToString()),
                        reader[7].ToString()
                        );
                }
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método GET de la tabla Location ejecutado exitosamente"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método GET de la tabla Location ejecutado exitosamente"));
                throw ex;
            }
            finally
            {
                command.Connection.Close();
                reader.Close();
            }
            return t;
        }

        public int Insert(Location t)
        {
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método INSERT de la tabla Location - Usuario: " + SessionClass.sessionUserName));
            TownImpl townImpl = new TownImpl();
            string query = @"INSERT INTO Location(locationName, locationAddress, phoneNumber, photo, townID, longitude, latitude, userID)
                             VALUES(@LocationName, @LocationAddress, @PhoneNumber, @Photo, @TownID, @Longitude, @Latitude, @UserID)";
            SqlCommand command = CreateBasicCommand(query);

            command.Parameters.AddWithValue("@LocationName", t.LocationName);
            command.Parameters.AddWithValue("@LocationAddress", t.LocationAddress);
            command.Parameters.AddWithValue("@PhoneNumber", t.PhoneNumber);
            command.Parameters.AddWithValue("@Photo", t.Photo);
            command.Parameters.AddWithValue("@Longitude", t.Longitude);
            command.Parameters.AddWithValue("@Latitude", t.Latitude);
            command.Parameters.AddWithValue("@UserID", SessionClass.sessionUserID);
            command.Parameters.AddWithValue("@TownID", townImpl.Get(t.TownName).Id);
            
            try
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método INSERT de la tabla Location ejecutado exitosamente"));
                return ExecuteBasicCommand(command);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | ERROR en el Método INSERT de la tabla Location  - ERROR: " + ex.Message));
                throw ex;
            }
        }

        public DataTable Select()
        {
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método SELECT de la tabla Location - Usuario: " + SessionClass.sessionUserName));
            string query = @"SELECT L.photo AS 'photo', L.id, L.locationName AS 'Nombre de la Ubicacion', L.locationAddress AS 'Detalles de la Ubicacion', L.latitude AS 'Latitud', L.longitude AS 'Longitud', L.phoneNumber AS 'Numero de Contacto'
                             FROM Location L
                             LEFT JOIN Town T ON T.id = L.townID
                             LEFT JOIN Province P ON T.provinceID = P.id
                             WHERE L.status=1";
            SqlCommand command = CreateBasicCommand(query);
            try
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método SELECT de la tabla Location ejecutado exitosamente"));
                return ExecuteDataTableCommand(command);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | ERROR en el Método SELECT de la tabla Location  - ERROR: " + ex.Message));
                throw ex;
            }
        }

        public DataTable Select(string name)
        {
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método SELECT de la tabla Location - Usuario: " + SessionClass.sessionUserName));
            string query = @"SELECT L.photo AS 'photo', L.id, L.locationName AS 'Nombre de la Ubicacion', L.locationAddress AS 'Detalles de la Ubicacion', L.latitude AS 'Latitud', L.longitude AS 'Longitud', L.phoneNumber AS 'Numero de Contacto'
                             FROM Location L
                             LEFT JOIN Town T ON T.id = L.townID
                             LEFT JOIN Province P ON T.provinceID = P.id
							 WHERE status=1 AND L.locationName LIKE @LocationName";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@LocationName", "%" + name + "%");
            try
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método SELECT de la tabla Location ejecutado exitosamente"));
                return ExecuteDataTableCommand(command);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | ERROR en el Método SELECT de la tabla Location  - ERROR: " + ex.Message));
                throw ex;
            }
        }

        public int Update(Location t)
        {
            System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Iniciando el método UPDATE de la tabla Location - Usuario: " + SessionClass.sessionUserName));
            TownImpl townImpl = new TownImpl();
            string query = @"UPDATE Location
                             SET locationName=@LocationName, locationAddress=@LocationAddress, latitude=@Latitude, 
                             longitude=@Longitude, townID=@TownID, phoneNumber=@PhoneNumber, userID=@userID, photo=@Photo, lastUpdate=CURRENT_TIMESTAMP
                             WHERE id = @id";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@UserID", SessionClass.sessionUserID);
            command.Parameters.AddWithValue("@TownID", townImpl.Get(t.TownName).Id);
            command.Parameters.AddWithValue("@LocationName", t.LocationName);
            command.Parameters.AddWithValue("@LocationAddress", t.LocationAddress);
            command.Parameters.AddWithValue("@Latitude", t.Latitude);
            command.Parameters.AddWithValue("@Longitude", t.Longitude);
            command.Parameters.AddWithValue("@PhoneNumber", t.PhoneNumber);
            command.Parameters.AddWithValue("@id", t.Id);
            command.Parameters.AddWithValue("@Photo", t.Photo);
            try
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | Método UPDATE de la tabla Location ejecutado exitosamente"));
                return ExecuteBasicCommand(command);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(DateTime.Now + " | ERROR en el Método UPDATE de la tabla Location  - ERROR: " + ex.Message));
                throw ex;
            }
        }
    }
}
