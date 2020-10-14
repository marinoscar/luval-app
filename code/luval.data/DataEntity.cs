using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Luval.Data
{
    public class DataEntity : DictionaryDataRecord
    {

        public DataEntity()
        {
            Id = Guid.NewGuid().ToString();
            UtcUpdatedOn = DateTime.UtcNow;
            UtcCreatedOn = UtcUpdatedOn;
            UserId = GetUserName();
        }

        private static string GetUserName()
        {
            if (Thread.CurrentPrincipal == null || Thread.CurrentPrincipal.Identity == null) return "N/A";
            return Thread.CurrentPrincipal.Identity.Name; 
        }

        [PrimaryKey]
        public string Id
        {
            get { return Convert.ToString(this["Id"]); }
            set { this["Id"] = value; }
        }

        public DateTime UtcUpdatedOn
        {
            get { return Convert.ToDateTime(this["UtcUpdatedOn"]); }
            set { this["UtcUpdatedOn"] = value; }
        }

        public DateTime UtcCreatedOn
        {
            get { return Convert.ToDateTime(this["UtcCreatedOn"]); }
            set { this["UtcCreatedOn"] = value; }
        }

        public string UserId
        {
            get { return Convert.ToString(this["UserId"]); }
            set { this["UserId"] = value; }
        }
    }
}
