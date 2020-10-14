using System;
using System.Collections.Generic;
using System.Text;

namespace Luval.Data
{
    public class EntityDatabaseSchema
    {
        public List<EntitySchema> Entities { get; set; }
    }

    public class EntityRelation
    {
        public string ParentEntity { get; set; }
        public string ChildEntity { get; set; }
        public string ChildEntityFieldName { get; set; }
    }
}
