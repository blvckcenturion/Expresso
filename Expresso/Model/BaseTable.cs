using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expresso.Model
{
    public class BaseTable
    {
        #region Properties
        public byte Status { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public int UserID { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// GET
        /// </summary>
        /// <param name="status"></param>
        /// <param name="registerDate"></param>
        /// <param name="lastUpdate"></param>
        /// <param name="userID"></param>
        public BaseTable(byte status, DateTime registerDate, DateTime lastUpdate, int userID)
        {
            Status = status;
            RegisterDate = registerDate;
            LastUpdate = lastUpdate;
            UserID = userID;
        }

        /// <summary>
        /// EMPTY
        /// </summary>
        public BaseTable()
        {
        }
        #endregion

    }
}
