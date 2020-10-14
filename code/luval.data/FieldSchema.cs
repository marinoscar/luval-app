using System;
using System.Collections.Generic;
using System.Text;

namespace Luval.Data
{
    public class FieldSchema
    {
        public string Name { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsIdentity { get; set; }
        public bool IsRequired { get; set; }
    }
}
