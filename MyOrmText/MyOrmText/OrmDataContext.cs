using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MyOrmText
{
    public class OrmDataContext<T> where T:class
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public static string connectionstr;
        /// <summary>
        /// SqlDao
        /// </summary>
        public static IDao<T> idao;
        /// <summary>
        /// 无参数构造函数
        /// </summary>
        public OrmDataContext()
        {
        }

        /// <summary>
        /// 构造函数默认dao是sqldao
        /// </summary>
        /// <param name="conStr"></param>
        public OrmDataContext(string conStr)
        {
            connectionstr = conStr;
            idao = new SqlDao<T>(conStr);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="conStr">连接字符串</param>
        public OrmDataContext(string conStr,IDao<T> dao)
        {
            connectionstr = conStr;
            idao = dao;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns>影响行数</returns>
        public int AddModel(T model)
        {
            int result=0;
            if (model != null)
            {
                result = idao.AddModel(model);
            }
            return result;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns>影响函数</returns>
        public int Updata(T model)
        {
            int result = 0;
            if (model != null)
            {
                result = idao.Updata(model);
            }
            return result;
        }
        /// <summary>
        /// 根据主键查找单个实体
        /// </summary>
        /// <param name="ID">主键标识符号</param>
        /// <returns></returns>
        public T GetModel(int ID)
        {
            T model = null;
            if(ID>0)
            {
                model = idao.GetModel(ID);
            }

            return model;
        }

        /// <summary>
        /// 返回实体集合
        /// </summary>
        /// <param name="strWhere">自定义条件</param>
        /// <returns></returns>
        public IEnumerable<T> GetModels(string strWhere)
        {
            IEnumerable<T> list = idao.GetModels(strWhere);
            return list;
        }

        /// <summary>
        /// 根据实体删除
        /// </summary>
        /// <param name="Model">要删除实体</param>
        /// <returns>受影响函数</returns>
        public int Delete(T Model)
        {
            int result = 0;
            if (Model != null)
            {
                result = idao.Delete(Model);
            }
            return result;
        }

        /// <summary>
        /// 删除多个实体
        /// </summary>
        /// <param name="models">多个实体</param>
        public void DeleteAll(IEnumerable<T> models)
        {
            if (models != null)
            {
                foreach(var model in models)
                {
                    idao.Delete(model);
                }
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <returns></returns>
        public object RunProc()
        {
            return null;
        }
        
    }
}
