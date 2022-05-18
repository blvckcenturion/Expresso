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
        public float BasePrice { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
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
        public Product(byte id, float basePrice, string productName, string productDescription, byte productCategoryId, byte status, DateTime registerDate, DateTime lastUpdate, int userID)
            : base(status, registerDate, lastUpdate, userID)
        {
            Id = id;
            ProductCategoryId = productCategoryId;
            BasePrice = basePrice;
            ProductName = productName;
            ProductDescription = productDescription;
        }

        /// <summary>
        /// INSERT
        /// </summary>
        /// <param name="basePrice"></param>
        /// <param name="productName"></param>
        /// <param name="productDescription"></param>
        /// <param name="productCategoryId"></param>
        public Product(float basePrice, string productName, string productDescription, byte productCategoryId)
        {
            ProductCategoryId = productCategoryId;
            BasePrice = basePrice;
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
