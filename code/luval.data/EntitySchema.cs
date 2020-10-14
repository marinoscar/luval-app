using System;
using System.Collections.Generic;
using System.Text;

namespace Luval.Data
{
    public class EntitySchema
    {
        public EntitySchema()
        {
            Fields = new List<FieldSchema>();
        }
        public string Name { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }

        public List<FieldSchema> Fields { get; set; }
    }
}
