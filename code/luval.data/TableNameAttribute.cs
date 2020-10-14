using System;
using System.Collections.Generic;
using System.Text;

namespace Luval.Data
{
    /// <summary>
    ///  Specifies the name of a table for an entity
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TableNameAttribute : NameBaseAttribute
    {
        public TableNameAttribute(string name) : base(name) { }
        public TableNameAttribute(string name, string caption) : base(name, caption) { }
        public TableNameAttribute(string name, string caption, string description) : base(name, caption, description) { }
    }
}
