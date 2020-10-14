using System;
using System.Collections.Generic;
using System.Text;

namespace Luval.Data
{
    /// <summary>
    /// Specifies the name of a column for an entity
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnNameAttribute : NameBaseAttribute
    {
        public ColumnNameAttribute(string name):base(name) { }
        public ColumnNameAttribute(string name, string caption) : base(name, caption) { }
        public ColumnNameAttribute(string name, string caption, string description) : base(name, caption, description) { }
    }
}
