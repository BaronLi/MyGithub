using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace MyOrmText
{
    public class SqlDao<T>:IDao<T> where T:class
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public static string connectionstr;
        private CreateSqlStrAndParms createSqlStrAndParms = null;

        public SqlDao(string conStr)
        {
            connectionstr = conStr;
            createSqlStrAndParms = new CreateSqlStrAndParms(conStr);
        }

        /// <summary>
        /// 添加一个实体
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public int AddModel(T model)
        {
            string addStr = createSqlStrAndParms.CreateAddStr(model) ;
            SqlParameter[] parms = createSqlStrAndParms.CreateaAddParms(model);
            int i = SqlHelper.ExecuteNonQuery(connectionstr, CommandType.Text, addStr, parms);
            return i;
        }
        /// <summary>
        /// 修改操作
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public int Updata(T model)
        {
            string updatedStr = createSqlStrAndParms.CreateUpdateStr(model);
            SqlParameter[] parms = createSqlStrAndParms.CreateaUpdateParms(model);
            int i = SqlHelper.ExecuteNonQuery(connectionstr, CommandType.Text, updatedStr, parms);
            return i;
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public int Delete(T model)
        {
            string delStr = createSqlStrAndParms.CreateDeleteStr(model);
            SqlParameter[] parms = createSqlStrAndParms.CreateaDeleteParms(model);
            int i = SqlHelper.ExecuteNonQuery(connectionstr, CommandType.Text, delStr, parms);
            return i;
        }

        /// <summary>
        /// 获得一个实体
        /// </summary>
        /// <param name="ID">主键标识符</param>
        /// <returns></returns>
        public T GetModel(int ID)
        {
           Type type=typeof(T);
           T model = (T)Activator.CreateInstance(type);
           foreach(var pro in type.GetProperties())
           {
               foreach (var attribute in pro.GetCustomAttributes(typeof(DataModelAttribute), true))
               {
                   DataModelAttribute da = (DataModelAttribute)attribute;
                   if (da.IsPrimaryKey == true)
                   {
                       pro.SetValue(model,ID,null);
                   }
               }
           }
           string getStr = createSqlStrAndParms.CreateGetModelStr(model);
           SqlParameter[] parms = createSqlStrAndParms.CreateaGetModelParms(model);
           DataSet ds = SqlHelper.ExecuteDataset(connectionstr, CommandType.Text, getStr, parms);
           foreach(var item in type.GetProperties())
           {
               foreach(var row in item.GetCustomAttributes(typeof(DataModelAttribute),true))
               {
                   DataModelAttribute attribute = (DataModelAttribute)row;
                   //item.SetValue(model,()reader.ge)
                   foreach (DataRow dsrow in ds.Tables[0].Rows)
                   {
                       item.SetValue(model,dsrow[attribute.ColumnName], null);
                   }
               }
           }
           return model;
        }

        /// <summary>
        /// 获得多个实体
        /// </summary>
        /// <param name="strWhere">额外条件</param>
        /// <returns></returns>
        public IEnumerable<T> GetModels(string strWhere)
        {
            Type type = typeof(T);
            T model = (T)Activator.CreateInstance(type);
            List<T> list = new List<T>();
            string getModelsStr = createSqlStrAndParms.CreateGetModeslStr(model);
            if (strWhere!="")
            {
                getModelsStr = getModelsStr + " where " + strWhere;
            }
            DataSet ds = SqlHelper.ExecuteDataset(connectionstr, CommandType.Text,getModelsStr);
            foreach(DataRow row in ds.Tables[0].Rows)
            {
                T newmodel = (T)Activator.CreateInstance(type);
                foreach(var pros in type.GetProperties())
                {
                    foreach(var attribute in pros.GetCustomAttributes(typeof(DataModelAttribute),true))
                    {
                        DataModelAttribute da = (DataModelAttribute)attribute;
                        pros.SetValue(newmodel,row[da.ColumnName],null);
                    }
                }
                list.Add(newmodel);
            }
            return list;
        }
    }
}
