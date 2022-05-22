﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expresso.Model;
using System.Data;

namespace Expresso.Interfaces
{
    public interface IEmployee: IBaseInterface<Employee>
    {
        DataTable Login(string username, string password);
    }
}
