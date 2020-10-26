using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Luval.Data
{
    public class EntitySchema
    {
        public EntitySchema()
        {
            Fields = new List<FieldSchema>();
        }

        public static EntitySchema FromDictionary(IDictionary<string, object> record, string entityName)
        {
            var res = new EntitySchema() { Name = entityName };
            foreach (var key in record.Keys)
            {
                res.Fields.Add(new FieldSchema() { 
                    Name = key,
                    IsPrimaryKey = ("Id".Equals(key))
                });
            }
            return res;
        }

        public static EntitySchema FromDataRecord(IDataRecord record, string entityName)
        {
            return FromDictionary(new DictionaryDataRecord(record), entityName);
        }

        public string Name { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }

        public List<FieldSchema> Fields { get; set; }
    }
}
