using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Luval.Data
{
    public interface IEntityAdapter
    {
        int Insert(object entity);
        int Insert(IDataRecord record);
        int Update(object entity);
        int Update(IDataRecord record);
        int Delete(IDataRecord record);
        int Delete(object entity);
        TEntity GetEntityByKey<TEntity, TKey>(TKey key);
    }
}
