using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyOrmText
{
    [DataModelAttribute(TableName="T_text")]
    public class MyModel
    {
        [DataModelAttribute(ColumnName="ID",DBType="int",IsPrimaryKey=true)]
        public int ID { get; set; }
        [DataModelAttribute(ColumnName = "Name", DBType = "nvarchar")]
        public string Name { get; set; }
        [DataModelAttribute(ColumnName = "Age", DBType = "int")]
        public int Age { get; set; }
    }
}
