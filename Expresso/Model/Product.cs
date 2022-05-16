﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expresso.Model
{
    public class Product: BaseTable
    {
        #region Properties
        public int Id { get; set; }
        public int ProductCategoryId { get; set; }
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
        public Product(int id, float basePrice, string productName, string productDescription, int productCategoryId, byte status, DateTime registerDate, DateTime lastUpdate, int userID)
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
        public Product(float basePrice, string productName, string productDescription, int productCategoryId)
        {
            ProductCategoryId = productCategoryId;
            BasePrice = basePrice;
            ProductName = productName;
            ProductDescription = productDescription;
        }
        #endregion

    }
}
