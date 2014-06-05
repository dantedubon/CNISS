using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNISS.CommonDomain.Domain
{
    public interface IRepositoryCommands <in T, TKey>
    {
        void save(T entity);
        void delete(T entity);
        void update(T entity);
       
    }
}