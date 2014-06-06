using System.Collections.Generic;

namespace CNISS.CommonDomain.Domain
{
    public interface IRepositoryReadOnly <out T, in TKey> 
    {
        T get(TKey id);
        IEnumerable<T> getAll();

    }
}
