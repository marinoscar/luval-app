using System;
using System.Collections.Generic;
using System.Text;

namespace Luval.Data
{
    public abstract class NameBaseAttribute : Attribute
    {

        public NameBaseAttribute(string name) : this(name, name, null) { }

        public NameBaseAttribute(string name, string caption): this(name, caption, null) { }

        public NameBaseAttribute(string name, string caption, string description)
        {
            Name = name; Caption = caption; Description = description;
        }
        /// <summary>
        /// Name for the entity
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Caption to be used by the entity
        /// </summary>
        public string Caption { get; set; }
        /// <summary>
        /// Description to be used by the entity
        /// </summary>
        public string Description { get; set; }
    }
}
