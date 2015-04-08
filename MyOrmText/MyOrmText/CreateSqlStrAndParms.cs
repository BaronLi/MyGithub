using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Reflection;

namespace MyOrmText
{
    /// <summary>
    /// 负责反射创建各种T_Sql类
    /// </summary>
    public class CreateSqlStrAndParms
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public static string connectionstr;

        public CreateSqlStrAndParms(string conStr)
        {
            connectionstr = conStr;
        }

        #region AddModel
        /// <summary>
        /// 创建添加语句
        /// </summary>
        /// <param name="obj">实体对象</param>
        /// <returns></returns>
        public string CreateAddStr(object obj)
        {
            string str1 = "";
            string str2 = "";
            string tableName = ReturnTableName(obj);
            CreateAddStrList(obj, ref str1, ref str2);
            StringBuilder addStr = new StringBuilder();
            addStr.Append("insert into " + tableName + " (");
            addStr.Append(str1);
            addStr.Append(") values(");
            addStr.Append(str2);
            addStr.Append(")");
            return addStr.ToString();
        }

        /// <summary>
        /// 创建添加T_Sql语句参数
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="str1">语句1</param>
        /// <param name="str2">语句2</param>
        /// <returns></returns>
        public void CreateAddStrList(object obj, ref string str1, ref string str2)
        {
            Type type = obj.GetType();
            List<String> list1 = new List<string>();
            List<String> list2 = new List<string>();
            PropertyInfo[] pros = type.GetProperties();
            foreach (var item in pros)
            {
                foreach (var row in item.GetCustomAttributes(typeof(DataModelAttribute), true))
                {
                    DataModelAttribute attribute = (DataModelAttribute)row;
                    if (attribute.IsPrimaryKey == true)
                    {
                    }
                    else
                    {
                        list1.Add(attribute.ColumnName);
                        list2.Add("@" + attribute.ColumnName);
                    }
                }
            }
            str1 = string.Join(",", list1);
            str2 = string.Join(",", list2);
        }

        /// <summary>
        /// 创建添加语句参数
        /// </summary>
        /// <param name="obj">实体对象</param>
        /// <returns></returns>
        public SqlParameter[] CreateaAddParms(object obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] pros = type.GetProperties();
            List<SqlParameter> parms = new List<SqlParameter>();
            foreach (var item in pros)
            {

                foreach (var row in item.GetCustomAttributes(typeof(DataModelAttribute), true))
                {
                    DataModelAttribute attribute = (DataModelAttribute)row;
                    if (attribute.IsPrimaryKey == true)
                    {
                    }
                    else
                    {
                        parms.Add(new SqlParameter("@" + attribute.ColumnName, item.GetValue(obj, null)));
                    }
                }
            }
            return parms.ToArray();
        }
     
        #endregion

        #region Delete
        /// <summary>
        /// 创建删除语句
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public string CreateDeleteStr(object obj)
        {
            string str1 = "";
            string tableName = ReturnTableName(obj);
            CreateDeltetrList(obj, ref str1);
            StringBuilder deleteStr = new StringBuilder();
            deleteStr.Append("delete from "+tableName+" where ");
            deleteStr.Append(str1);
            return deleteStr.ToString();
        }

        /// <summary>
        /// 创建T_Sql语句参数
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="str1">语句1</param>
        /// <param name="str2">语句2</param>
        public void CreateDeltetrList(object obj, ref string str1)
        {
            Type type = obj.GetType();
            PropertyInfo[] pros = type.GetProperties();
            foreach(var item in pros)
            {
                foreach(var row in item.GetCustomAttributes(typeof(DataModelAttribute),true))
                {
                    DataModelAttribute attribute=(DataModelAttribute)row;
                    if (attribute.IsPrimaryKey==true)
                    {
                        str1=attribute.ColumnName + "=@" + attribute.ColumnName;
                    }
                }
            }
;
        }

        /// <summary>
        /// 创建参数
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public SqlParameter[] CreateaDeleteParms(object obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] pros = type.GetProperties();
            List<SqlParameter> parms = new List<SqlParameter>();
            foreach (var item in pros)
            {
                
                foreach (var row in item.GetCustomAttributes(typeof(DataModelAttribute), true))
                {
                    DataModelAttribute attribute = (DataModelAttribute)row;
                    if (attribute.IsPrimaryKey == true)
                    {
                        parms.Add(new SqlParameter("@" + attribute.ColumnName, item.GetValue(obj, null)));
                    }
                }
            }
            return parms.ToArray(); ;
        }
        #endregion

        #region Update
        /// <summary>
        /// 创建删除语句
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public string CreateUpdateStr(object obj)
        {
            string str1 = "";
            string str2 = "";
            string tableName = ReturnTableName(obj);
            CreatUpdateList(obj, ref str1, ref str2);
            StringBuilder updateStr = new StringBuilder();
            updateStr.Append("update "+tableName+" set ");
            updateStr.Append(str1);
            updateStr.Append(" where ");
            updateStr.Append(str2);
            return updateStr.ToString();
        }

        /// <summary>
        /// 创建T_Sql语句参数
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="str1">语句1</param>
        /// <param name="str2">语句2</param>
        public void CreatUpdateList(object obj, ref string str1, ref string str2)
        {
            List<string> list1 = new List<string>();
            Type type = obj.GetType();
            PropertyInfo[] pros = type.GetProperties();
            foreach(var item in pros)
            {
                foreach (var row in item.GetCustomAttributes(typeof(DataModelAttribute),true))
                {
                    DataModelAttribute attribute=(DataModelAttribute)row;
                    if(attribute.IsPrimaryKey==true)
                    {
                        str2 = attribute.ColumnName + "=@" + attribute.ColumnName;
                    }
                    else
                    {
                        list1.Add(attribute.ColumnName + "=@" + attribute.ColumnName);
                    }
                }
            }
            str1= string.Join(",",list1);
        }

        /// <summary>
        /// 创建参数
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public SqlParameter[] CreateaUpdateParms(object obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] pros = type.GetProperties();
            List<SqlParameter> parms = new List<SqlParameter>();
            foreach (var item in pros)
            {
                foreach (var row in item.GetCustomAttributes(typeof(DataModelAttribute), true))
                {
                    DataModelAttribute attribute = (DataModelAttribute)row;
                    parms.Add(new SqlParameter("@"+attribute.ColumnName,item.GetValue(obj,null)));
                }
            }
            return parms.ToArray();
        }
        #endregion

        #region GetModel
        /// <summary>
        /// 创建删除语句
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public string CreateGetModelStr(object obj)
        {
            string str1 = "";
            string str2 = "";
            string tableName = ReturnTableName(obj);
            CreatGetModelList(obj, ref str1, ref str2);
            StringBuilder getStr = new StringBuilder();
            getStr.Append("select "+str1);
            getStr.Append(" from "+tableName);
            getStr.Append(" where ");
            getStr.Append(str2);
            return getStr.ToString();
        }

        /// <summary>
        /// 创建T_Sql语句参数
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="str1">语句1</param>
        /// <param name="str2">语句2</param>
        public void CreatGetModelList(object obj, ref string str1, ref string str2)
        {
            Type type = obj.GetType();
            List<string> list1 = new List<string>();
            PropertyInfo[] prs = type.GetProperties();
            foreach (var item in prs)
            {
                foreach (var row in item.GetCustomAttributes(typeof(DataModelAttribute),true))
                {
                    DataModelAttribute attribute = (DataModelAttribute)row;
                    if (attribute.IsPrimaryKey == true)
                    {
                        str2 ="@" + attribute.ColumnName+"="+attribute.ColumnName ;
                    }
                    list1.Add(attribute.ColumnName);
                }
            }
            str1 = string.Join(",",list1);
        }

        /// <summary>
        /// 创建参数
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public SqlParameter[] CreateaGetModelParms(object obj)
        {
            Type type = obj.GetType();
            List<SqlParameter> parms = new List<SqlParameter>();
            PropertyInfo[] prs = type.GetProperties();
            foreach (var item in prs)
            {
                foreach (var row in item.GetCustomAttributes(typeof(DataModelAttribute), true))
                {
                    DataModelAttribute attribute = (DataModelAttribute)row;
                    if(attribute.IsPrimaryKey==true)
                    {
                        parms.Add(new SqlParameter("@" + attribute.ColumnName,item.GetValue(obj,null)));
                    }
                }
            }
            return parms.ToArray();
        }
        #endregion

        #region GetModels
        /// <summary>
        /// 创建返回集合T_Sql语句
        /// </summary>
        /// <param name="obj">对象实体</param>
        /// <returns></returns>
        public string CreateGetModeslStr(object obj)
        {
            string str1 = "";
            string tableName = ReturnTableName(obj);
            CreatGetModelsList(obj,ref str1);
            StringBuilder getModelsStr = new StringBuilder();
            getModelsStr.Append("select "+str1);
            getModelsStr.Append(" from "+tableName);
            return getModelsStr.ToString();
        }

        /// <summary>
        /// 创建返回集合T_Sql参数语句
        /// </summary>
        /// <param name="obj">实体</param>
        /// <param name="str1">返回语句</param>
        public void CreatGetModelsList(object obj, ref string str1)
        {
            Type type = obj.GetType();
            List<string> list1 = new List<string>();
            PropertyInfo[] prs = type.GetProperties();
            foreach (var item in prs)
            {
                foreach (var row in item.GetCustomAttributes(typeof(DataModelAttribute), true))
                {
                    DataModelAttribute attribute = (DataModelAttribute)row;
                    list1.Add(attribute.ColumnName);
                }
            }
            str1 = string.Join(",", list1);
        }
        #endregion

        #region PublicMethod
        /// <summary>
        /// 返回参数字符串
        /// </summary>
        /// <param name="obj">实体对象</param>
        /// <returns></returns>
        public IDictionary<string, string> ReturnParmsName(object obj, string tableName)
        {
            IDictionary<string, string> dictionary = null;
            string cmdtext = "select COLUMN_NAME,DATA_TYPE from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='" + tableName + "'";
            SqlDataReader reader = SqlHelper.ExecuteReader(connectionstr, System.Data.CommandType.Text, cmdtext);
            while (reader.Read())
            {
                dictionary.Add(reader.GetString(0), reader.GetString(1));
            }
            reader.Close();
            return dictionary;
        }

        /// <summary>
        /// 返回表名
        /// </summary>
        /// <param name="obj">实体对象</param>
        /// <returns></returns>
        public string ReturnTableName(object obj)
        {
            string tableName = "";
            try
            {
                Type type = obj.GetType();
                var data = type.GetCustomAttributes(typeof(DataModelAttribute), true);
                DataModelAttribute attribute = (DataModelAttribute)data[0];
                tableName = attribute.TableName;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return tableName;
        }
        #endregion

    }
}
