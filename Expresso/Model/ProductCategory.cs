using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expresso.Model
{
    public class ProductCategory : BaseTable
    {
        #region Properties
        public byte Id { get; set; }
        public string ProductCategoryName { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// SELECT
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productCategoryName"></param>
        /// <param name="status"></param>
        /// <param name="registerDate"></param>
        /// <param name="lastUpdate"></param>
        /// <param name="userID"></param>
        public ProductCategory(byte id, string productCategoryName, byte status, DateTime registerDate, DateTime lastUpdate, int userID)
            :base(status, registerDate, lastUpdate, userID)
        {
            Id = id;
            ProductCategoryName = productCategoryName;
        }

        public ProductCategory(byte id)
        {
            Id = id;
        }
        
        /// <summary>
        /// INSERT
        /// </summary>
        /// <param name="productCategoryName"></param>
        public ProductCategory(string productCategoryName)
        {
            ProductCategoryName = productCategoryName;
        }
        #endregion

    }
}
