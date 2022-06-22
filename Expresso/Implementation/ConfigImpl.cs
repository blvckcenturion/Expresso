using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expresso.Interfaces;

namespace Expresso.Implementation
{
    public class ConfigImpl: BaseImpl, IConfig
    {
        public DataTable Select()
        {
            string query = @"SELECT pathPhotoEmployee, pathPhotoLocation, pathPhotoProduct FROM Config";
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
