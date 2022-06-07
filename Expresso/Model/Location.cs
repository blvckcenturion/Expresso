using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expresso.Model
{
    public class Location : BaseTable
    {
        #region Properties
        public int Id { get; set; }
        public string LocationName { get; set; }
        public string LocationAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Photo { get; set; }        
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public byte TownID { get; set; }
        public string TownName { get; set; }

        /// <summary>
        /// INSERT
        /// </summary>
        /// <param name="locationName"></param>
        /// <param name="locationAddress"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="photo"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="townID"></param>
        public Location(string locationName, string locationAddress, string phoneNumber, string photo, double latitude, double longitude, string townName)
        {
            LocationName = locationName;
            LocationAddress = locationAddress;
            PhoneNumber = phoneNumber;
            Photo = photo;
            Latitude = latitude;
            Longitude = longitude;
            TownName = townName;
        }
        #endregion


    }
}
