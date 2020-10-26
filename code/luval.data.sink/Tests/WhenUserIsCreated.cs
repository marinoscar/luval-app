using Luval.Data;
using Luval.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luval.data.sink.Tests
{
    public class WhenUserIsCreated
    {
        public void ItShouldWork()
        {
            var sqlAdapter = CreateAdapter(typeof(User));
            sqlAdapter.Insert(new User() { ProviderKey = Guid.NewGuid().ToString(), Email = "oscar@marin.cr" });
        }

        private static Database CreateDatabase()
        {
            return new SqlServerDatabase(System.Configuration.ConfigurationManager.ConnectionStrings[0].ConnectionString);
        }

        private static SqlEntityAdapter CreateAdapter(Type type)
        {
            return new SqlEntityAdapter(CreateDatabase(), new SqlServerDialectProvider(SqlTableSchema.Load(type)));
        }
    }
}
