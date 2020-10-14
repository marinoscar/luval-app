using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Luval.Data
{
    /// <summary>
    /// Provides an abstraction for the schema of a sql column
    /// </summary>
    public class SqlColumnSchema : FieldSchema
    {
        public static FieldSchema Load(PropertyInfo property)
        {
            var att = GetColumnAttribute(property);
            return new SqlColumnSchema()
            {
                
                Name = att.Name,
                Caption = att.Caption,
                Description = att.Description,
                IsPrimaryKey = property.GetCustomAttribute<PrimaryKeyAttribute>() != null,
                IsIdentity = property.GetCustomAttribute<IdentityColumnAttribute>() != null
            };
        }

        internal static ColumnNameAttribute GetColumnAttribute(PropertyInfo property)
        {
            var att = property.GetCustomAttribute<ColumnNameAttribute>();
            if (att == null) return new ColumnNameAttribute(property.Name);
            return ((ColumnNameAttribute)att);
        }
    }
}
