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
            CreatedBy = GetUserName();
            UpdatedBy = CreatedBy;
        }

        private static string GetUserName()
        {
            if (Thread.CurrentPrincipal == null || Thread.CurrentPrincipal.Identity == null) return "N/A";
            return string.IsNullOrWhiteSpace(Thread.CurrentPrincipal.Identity.Name) ? "N/A" : Thread.CurrentPrincipal.Identity.Name; 
        }

        [PrimaryKey]
        public string Id
        {
            get { return CastValue<string>("Id"); }
            set { this["Id"] = value; }
        }

        public DateTime UtcUpdatedOn
        {
            get { return CastValue<DateTime>("UtcUpdatedOn"); }
            set { this["UtcUpdatedOn"] = value; }
        }

        public DateTime UtcCreatedOn
        {
            get { return CastValue<DateTime>("UtcCreatedOn"); }
            set { this["UtcCreatedOn"] = value; }
        }

        public string CreatedBy
        {
            get { return CastValue<string>("CreatedBy"); }
            set { this["CreatedBy"] = value; }
        }

        public string UpdatedBy
        {
            get { return CastValue<string>("UpdatedBy"); }
            set { this["UpdatedBy"] = value; }
        }
    }
}
