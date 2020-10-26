using System;
using System.Collections.Generic;
using System.Text;

namespace Luval.Data
{
    public interface IEntityQuery
    {
        IEnumerable<IDictionary<string, object>> Execute();
    }
}
