using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNISS.CommonDomain.Domain
{
    public interface IRepositoryReadOnly <out T, in TKey> 
    {
        T get(TKey id);
        IEnumerable<T> getAll();

    }
}
