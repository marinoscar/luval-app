using Luval.Data;
using System;

namespace Luval.Data.Model
{
    public class User : DataEntity
    {
        public string ProviderKey 
        { 
            get { return CastValue<string>("ProviderKey"); }
            set { this["ProviderKey"] = value; }
        }

        public string Email
        {
            get { return CastValue<string>("Email"); }
            set { this["Email"] = value; }
        }

        public string Name
        {
            get { return CastValue<string>("Name"); }
            set { this["Name"] = value; }
        }
    }
}
