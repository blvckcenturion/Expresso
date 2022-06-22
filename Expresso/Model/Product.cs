using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expresso.Model
{
    public class Product: BaseTable
    {
        #region Properties
        public byte Id { get; set; }
        public byte ProductCategoryId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductCategoryName { get; set; }
        public string Photo { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// SELECT
        /// </summary>
        /// <param name="id"></param>
        /// <param name="basePrice"></param>
        /// <param name="productName"></param>
        /// <param name="productDescription"></param>
        /// <param name="productCategoryId"></param>
        /// <param name="status"></param>
        /// <param name="registerDate"></param>
        /// <param name="lastUpdate"></param>
        /// <param name="userID"></param>
        public Product(byte id, string productName, string productDescription, byte productCategoryId, byte status, string productCategoryName, DateTime registerDate, DateTime lastUpdate, int userID, string photo)
            : base(status, registerDate, lastUpdate, userID)
        {
            Id = id;
            ProductCategoryId = productCategoryId;
            ProductName = productName;
            ProductDescription = productDescription;
            ProductCategoryName = productCategoryName;
            Photo = photo;
        }

        /// <summary>
        /// INSERT
        /// </summary>
        /// <param name="basePrice"></param>
        /// <param name="productName"></param>
        /// <param name="productDescription"></param>
        /// <param name="productCategoryId"></param>
        public Product(string productName, string productDescription, string productCategoryName)
        {
            ProductCategoryName = productCategoryName;
            ProductName = productName;
            ProductDescription = productDescription;
        }

        public Product(string productName)
        {
            ProductName = productName;
        }
        #endregion

    }
}
