using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Reflection;
using Luval.Core;
using System.Collections;

namespace Luval.Data
{
    public class DictionaryDataRecord : IDataRecord, IDictionary<string, object>
    {
        private readonly IDictionary<string, object> _record;

        public DictionaryDataRecord()
        {
            _record = new Dictionary<string, object>();
        }

        public DictionaryDataRecord(IDictionary<string, object> record)
        {
            _record = record;
        }

        public DictionaryDataRecord(IDataRecord dataRecord) : base()
        {
            for (int i = 0; i < dataRecord.FieldCount; i++)
            {
                _record[dataRecord.GetName(i)] = dataRecord.GetValue(i);
            }
        }

        public DictionaryDataRecord(object entity):this(FromEntityToDictionary(entity))
        {

        }

        protected virtual T CastValue<T>(int index)
        {
            return CastValue<T>(GetName(index));
        }

        protected virtual T CastValue<T>(string name)
        {
            if (!_record.ContainsKey(name)) return default(T);
            return (T)Convert.ChangeType(_record[name], typeof(T));
        }

        [NotMapped]
        public object this[int i] => GetValue(i);

        [NotMapped]
        public object this[string name] { get => CastValue<object>(name); set => _record[name] = value;  }

        [NotMapped]
        public int FieldCount => _record.Keys.Count;

        [NotMapped]
        public ICollection<string> Keys => _record.Keys;

        [NotMapped]
        public ICollection<object> Values => _record.Values;

        [NotMapped]
        public int Count => _record.Count;

        [NotMapped]
        public bool IsReadOnly => false;

        public bool GetBoolean(int i)
        {
            return Convert.ToBoolean(GetValue(i));
        }

        public byte GetByte(int i)
        {
            return Convert.ToByte(GetValue(i));
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            buffer = UTF8Encoding.UTF8.GetBytes(GetString(i)).Skip(bufferoffset).Take(length).ToArray();
            return buffer.LongLength;
        }

        public char GetChar(int i)
        {
            return Convert.ToChar(GetValue(i));
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            buffer = GetString(i).ToCharArray().Skip(bufferoffset).Take(length).ToArray();
            return buffer.LongLength;
        }

        public IDataReader GetData(int i)
        {
            throw new NotImplementedException();
        }

        public string GetDataTypeName(int i)
        {
            return GetFieldType(i).Name;
        }

        public DateTime GetDateTime(int i)
        {
            return Convert.ToDateTime(GetValue(i));
        }

        public decimal GetDecimal(int i)
        {
            return Convert.ToDecimal(GetValue(i));
        }

        public double GetDouble(int i)
        {
            return Convert.ToDouble(GetValue(i));
        }

        public Type GetFieldType(int i)
        {
            return GetValue(i).GetType();
        }

        public float GetFloat(int i)
        {
            return Convert.ToSingle(GetValue(i));
        }

        public Guid GetGuid(int i)
        {
            return Guid.Parse(GetString(i));
        }

        public short GetInt16(int i)
        {
            return Convert.ToInt16(GetValue(i));
        }

        public int GetInt32(int i)
        {
            return Convert.ToInt32(GetValue(i));
        }

        public long GetInt64(int i)
        {
            return Convert.ToInt64(GetValue(i));
        }

        public string GetName(int i)
        {
            return _record.Keys.ToList()[i];
        }

        public int GetOrdinal(string name)
        {
            return _record.Keys.ToList().IndexOf(name);
        }

        public string GetString(int i)
        {
            return Convert.ToString(GetValue(i));
        }

        public object GetValue(int i)
        {
            return this[GetName(i)];
        }

        public int GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        public bool IsDBNull(int i)
        {
            return GetValue(i).IsNullOrDbNull();
        }

        public static IDataRecord FromEntity(object o)
        {
            return new DictionaryDataRecord(FromEntityToDictionary(o));
        }

        private static Dictionary<string, object> FromEntityToDictionary(object o)
        {
            var record = new Dictionary<string, object>();
            foreach (var property in o.GetType().GetProperties())
            {
                if (property.GetCustomAttribute<NotMappedAttribute>() != null) continue;
                var colAtt = property.GetCustomAttribute<ColumnNameAttribute>();
                var name = colAtt != null ? colAtt.Name : property.Name;
                record[name] = property.GetValue(o, null);
            }
            return record;
        }

        public void Add(string key, object value)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(string key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(string key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(string key, out object value)
        {
            throw new NotImplementedException();
        }

        public void Add(KeyValuePair<string, object> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
