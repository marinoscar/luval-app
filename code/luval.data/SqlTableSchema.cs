using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Luval.Data
{
    public class SqlTableSchema : EntitySchema
    {
        public static SqlTableSchema Load(Type type)
        {
            var columns = new List<FieldSchema>();
            foreach (var prop in type.GetProperties())
            {
                if (prop.GetCustomAttribute<NotMappedAttribute>() != null) continue;
                columns.Add(SqlColumnSchema.Load(prop));
            }
            return new SqlTableSchema() { Name = GetTableName(type), Fields = columns };
        }

        private static string GetTableName(Type type)
        {
            var att = type.GetCustomAttribute<TableNameAttribute>();
            if (att == null) return type.Name;
            return att.Name;
        }
    }
}
