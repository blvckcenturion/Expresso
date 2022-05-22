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
        public string ProductCategoryDescription { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// SELECT
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productCategoryName"></param>
        /// <param name="productCategoryDescription"></param>
        /// <param name="status"></param>
        /// <param name="registerDate"></param>
        /// <param name="lastUpdate"></param>
        /// <param name="userID"></param>
        public ProductCategory(byte id, string productCategoryName, string productCategoryDescription, byte status, DateTime registerDate, DateTime lastUpdate, int userID)
            :base(status, registerDate, lastUpdate, userID)
        {
            Id = id;
            ProductCategoryName = productCategoryName;
            ProductCategoryDescription = productCategoryDescription;
        }

        public ProductCategory(byte id)
        {
            Id = id;
        }
        
        /// <summary>
        /// INSERT
        /// </summary>
        /// <param name="productCategoryName"></param>
        /// <param name="productCategoryDescription"></param>
        public ProductCategory(string productCategoryName, string productCategoryDescription)
        {
            ProductCategoryName = productCategoryName;
            ProductCategoryDescription = productCategoryDescription;
        }
        #endregion

    }
}
