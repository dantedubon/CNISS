using System.Collections.Generic;

namespace CNISS.CommonDomain.Domain
{
    public interface IRepositoryReadOnlyCompoundKey<out T, in TKey1, in TKey2>
    {
        T get(TKey1 id1, TKey2 id2);
        IEnumerable<T> getAll();
        bool exists(TKey1 id1, TKey2 id2);

    }
}