﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expresso.Implementation
{
    public class ConfigImpl: BaseImpl
    {
        public DataTable Select()
        {
            string query = @"SELECT pathPhotoEmployee FROM Config";
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
    }
}