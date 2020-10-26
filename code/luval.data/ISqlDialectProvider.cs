using System;
using System.Data;

namespace Luval.Data
{
    public interface ISqlDialectProvider
    {
        EntitySchema Schema { get; }
        string GetCreateCommand(IDataRecord record);
        string GetReadCommand(IDataRecord record);
        string GetUpdateCommand(IDataRecord record);
        string GetDeleteCommand(IDataRecord record);
        string GetReadAllCommand();
        string GetSqlDataType(Type type, int size);
    }
}