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
            string query = @"UPDATE Location SET status = 0, lastUpdate = CURRENT_TIMESTAMP, userID = @userID
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

        public bool Exists(string name)
        {
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

        public Location Get(byte id)
        {
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

        public int Insert(Location t)
        {
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
                return ExecuteBasicCommand(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Select()
        {
            string query = @"SELECT L.photo AS 'photo', L.id, L.locationName AS 'Nombre de la Ubicacion', L.locationAddress AS 'Detalles de la Ubicacion', L.latitude AS 'Latitud', L.longitude AS 'Longitud', L.phoneNumber AS 'Numero de Contacto'
                             FROM Location L
                             LEFT JOIN Town T ON T.id = L.townID
                             LEFT JOIN Province P ON T.provinceID = P.id
                             WHERE L.status=1";
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

        public DataTable Select(string name)
        {
            string query = @"SELECT L.photo AS 'photo', L.id, L.locationName AS 'Nombre de la Ubicacion', L.locationAddress AS 'Detalles de la Ubicacion', L.latitude AS 'Latitud', L.longitude AS 'Longitud', L.phoneNumber AS 'Numero de Contacto'
                             FROM Location L
                             LEFT JOIN Town T ON T.id = L.townID
                             LEFT JOIN Province P ON T.provinceID = P.id
							 WHERE status=1 AND L.locationName LIKE @LocationName";
            SqlCommand command = CreateBasicCommand(query);
            command.Parameters.AddWithValue("@LocationName", "%" + name + "%");
            try
            {
                return ExecuteDataTableCommand(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Update(Location t)
        {
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
                return ExecuteBasicCommand(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
