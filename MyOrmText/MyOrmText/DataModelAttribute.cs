using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyOrmText
{
    public class DataModelAttribute:Attribute
    {
        public DataModelAttribute()
        {
        }

        public string Constr { get; set; }
        public bool IsPrimaryKey { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string DBType { get; set; }
    }
}
