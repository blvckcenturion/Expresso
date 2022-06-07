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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public int Update(Location t)
        {
            throw new NotImplementedException();
        }
    }
}
