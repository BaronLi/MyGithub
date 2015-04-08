using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyOrmText
{
    public interface IDao<T> where T:class
    {
         int AddModel(T model);

         int Updata(T model);

         int Delete(T model);

         T GetModel(int ID);

         IEnumerable<T> GetModels(string strWhere);
    }
}
