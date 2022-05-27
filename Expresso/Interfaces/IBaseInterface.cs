using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Expresso.Interfaces
{
    public interface IBaseInterface<T>
    {
        #region Methods
        DataTable Select();
        int Insert(T t);
        int Update(T t);
        int Delete(T t);
        T Get(byte id);
        #endregion
    }
}
