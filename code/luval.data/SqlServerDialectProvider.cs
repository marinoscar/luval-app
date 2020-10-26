using Luval.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Luval.Data
{
    public class SqlServerDialectProvider : ISqlDialectProvider
    {
        public SqlServerDialectProvider(EntitySchema schema)
        {
            Schema = schema;
        }

        public EntitySchema Schema { get; private set; }

        public string GetCreateCommand(IDataRecord record)
        {
            var sw = new StringWriter();
            sw.WriteLine("INSERT INTO {1} ({0}) VALUES ({2});",
                string.Join(", ", GetSqlFormattedColumnNames((i) => !i.IsIdentity)),
                GetSqlFormattedTableName(),
                string.Join(", ", GetSqlInserValues(record)));
            return sw.ToString();
        }

        public string GetDeleteCommand(IDataRecord record)
        {
            var sw = new StringWriter();
            sw.WriteLine("DELETE FROM {0} WHERE {1};", GetSqlFormattedTableName(),
                string.Join(" AND ", GetKeyWhereStatement(record)));
            return sw.ToString();
        }

        public string GetUpdateCommand(IDataRecord record)
        {
            var sw = new StringWriter();
            sw.WriteLine("UPDATE {0} SET {1} WHERE {2};", GetSqlFormattedTableName(),
                string.Join(", ", GetUpdateValueStatement(record)),
                string.Join(" AND ", GetKeyWhereStatement(record)));
            return sw.ToString();
        }

        public string GetReadCommand(IDataRecord record)
        {
            var sw = new StringWriter();
            sw.WriteLine("SELECT {0} FROM {1} WHERE {2};",
                string.Join(", ", GetSqlFormattedColumnNames((i) => true)),
                GetSqlFormattedTableName(),
                string.Join(" AND ", GetKeyWhereStatement(record)));
            return sw.ToString();
        }

        public string GetReadAllCommand()
        {
            var sw = new StringWriter();
            sw.WriteLine("SELECT {0} FROM {1};",
                string.Join(", ", GetSqlFormattedColumnNames((i) => true)),
                GetSqlFormattedTableName());
            return sw.ToString();
        }

        private IEnumerable<string> GetUpdateValueStatement(IDataRecord record)
        {
            return GetColumnValuePair(record, i => !i.IsPrimaryKey && !i.IsIdentity).Select(i => {
                if (i.Contains("IS NULL"))
                    i = i.Replace("IS NULL", "= NULL");
                return i;
            });
        }

        private IEnumerable<string> GetKeyWhereStatement(IDataRecord record)
        {
            if (!Schema.Fields.Any(i => i.IsPrimaryKey))
                throw new InvalidDataException("At least one primary key column is required");
            return GetColumnValuePair(record, i => i.IsPrimaryKey);
        }

        private IEnumerable<string> GetColumnValuePair(IDataRecord record, Func<FieldSchema, bool> predicate)
        {
            return Schema.Fields.Where(predicate)
                .Select(i =>
                {
                    var val = record[i.Name];
                    var res = string.Format("{0} = {1}", GetSqlFormattedColumnName(i), val.ToSql());
                    if (val.IsNullOrDbNull()) res = string.Format("{0} IS NULL", GetSqlFormattedColumnName(i));
                    return res;
                }).ToList();
        }

        private IEnumerable<string> GetSqlInserValues(IDataRecord record)
        {
            return GetEntityValues(record, i => !i.IsIdentity).Select(i => i.ToSql());
        }

        private IEnumerable<object> GetEntityValues(IDataRecord record, Func<FieldSchema, bool> predicate)
        {
            var res = new List<object>();
            Schema.Fields.Where(predicate).ToList().ForEach(i => res.Add(record[i.Name]));
            return res;
        }

        private string GetSqlFormattedTableName()
        {
            return string.Format("[{0}]", Schema.Name);
        }

        private string GetSqlFormattedColumnName(FieldSchema columnSchema)
        {
            return string.Format("[{0}]", columnSchema.Name);
        }

        private IEnumerable<string> GetSqlFormattedColumnNames(Func<FieldSchema, bool> predicate)
        {
            return Schema.Fields.Where(predicate).Select(GetSqlFormattedColumnName);
        }

        public string GetSqlDataType(Type type, int size)
        {
            if (type == null) throw new ArgumentNullException("type cannot be null");
            var sizeValue = size <= 0 ? "MAX" : size.ToString();
            if (typeof(bool) == type) return "bit";
            if (typeof(string) == type) return "nvarchar({0})".Fi(sizeValue);
            if (typeof(int) == type) return "int";
            if (typeof(float) == type) return "decimal";
            if (typeof(decimal) == type) return "decimal";
            if (typeof(double) == type) return "decimal";
            if (typeof(long) == type) return "decimal";
            if (typeof(short) == type) return "decimal";
            if (typeof(DateTime) == type) return "datetime";
            if (typeof(byte) == type) return "int";
            if (typeof(byte[]) == type) return "varbinary[{0}]".Fi(sizeValue);
            throw new NotImplementedException("Data type {0} is not supported".Fi(type.Name));
        }
    }
}
