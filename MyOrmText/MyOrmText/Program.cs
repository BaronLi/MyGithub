using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data.SqlClient;

namespace MyOrmText
{
    class Program
    {
        static void Main(string[] args)
        {
            #region MyRegion
            //object ojb = "123";
            //Console.Write(ojb.GetType().Name);
            //MyModel model = new MyModel();
            //model.ID = 1;
            //model.Name = "123";
            //Type type = model.GetType();
            //var da= type.GetCustomAttributes(true);
            //DataModelAttribute daa = (DataModelAttribute)da[0];
            //Console.WriteLine(daa.TableName);
            //PropertyInfo[] pros = type.GetProperties();
            //foreach (var item in pros)
            //{
            //    //Console.WriteLine(item.Name);
            //    foreach(var item2 in item.GetCustomAttributes(true))
            //    {
            //        DataModelAttribute da = (DataModelAttribute)item2;
            //        Console.WriteLine(da.DBType);
            //    }
            //} 
            #endregion

            #region MyRegion
            //string connectionstr = "Data Source=.;Initial Catalog=Exam;Integrated Security=True";
            //string cmdtext = "select COLUMN_NAME,DATA_TYPE from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='"+"T_text"+"'";
            //SqlDataReader reader = SqlHelper.ExecuteReader(connectionstr, System.Data.CommandType.Text, cmdtext);
            //while (reader.Read())
            //{
            //    Console.WriteLine("name:"+reader.GetString(0)+" type:"+reader.GetString(1));
            //}
            //reader.Close();
            #endregion

            #region MyRegion
            //MyModel model = new MyModel();
            //model.ID = 2;
            //model.Name = "123";
            //CreateSqlStrAndParms cc = new CreateSqlStrAndParms("Data Source=.;Initial Catalog=Exam;Integrated Security=True");
            //string val = cc.CreateAddStr(model);
            //Console.WriteLine(val);
            //SqlParameter[] parms = cc.CreateaAddParms(model);
            //Console.WriteLine("123");
            //string str1="";
            //string str2="";
            //cc.CreateAddStrList(model, ref str1, ref str2);
            //Console.WriteLine(str1);
            //Console.WriteLine(str2);
            #endregion

            #region MyRegion
            //MyModel model = new MyModel();
            //model.Age = 12;
            //model.ID = 1;
            //model.Name = "myorm";
            //SqlDao<MyModel> dao = new SqlDao<MyModel>("Data Source=.;Initial Catalog=Exam;Integrated Security=True");
            //int i=dao.AddModel(model);
            //Console.WriteLine(i);

            //MyModel model = new MyModel();
            //model.ID = 17;
            //SqlDao<MyModel> dao = new SqlDao<MyModel>("Data Source=.;Initial Catalog=Exam;Integrated Security=True");
            //int i = dao.Delete(model);
            //Console.WriteLine(i);
            #endregion

            #region MyRegion
            //MyModel model = new MyModel();
            //model.Age = 12;
            //model.ID = 19;
            //model.Name = "myorm update";
            //SqlDao<MyModel> dao = new SqlDao<MyModel>("Data Source=.;Initial Catalog=Exam;Integrated Security=True");
            //int i = dao.Updata(model);
            //MyModel model = dao.GetModel(18);
            //Console.WriteLine(model.Name);
            #endregion

            #region MyRegion
            //SqlDao<MyModel> dao = new SqlDao<MyModel>("Data Source=.;Initial Catalog=Exam;Integrated Security=True");
            //IEnumerable<MyModel> list = dao.GetModels("");
            //foreach(var item in list)
            //{
            //    Console.WriteLine("ID:"+item.ID+" Name:"+item.Name+" Age:"+item.Age);
            //}
            //OrmDataContext<MyModel> context = new OrmDataContext<MyModel>("Data Source=.;Initial Catalog=Exam;Integrated Security=True");
            //MyModel Model = new MyModel();
            //Model.Age = 11;
            //Model.ID = 12;
            //Model.Name = "context add";
            //int i = context.AddModel(Model);
            //Console.WriteLine(i);
            #endregion
        }
    }
}
