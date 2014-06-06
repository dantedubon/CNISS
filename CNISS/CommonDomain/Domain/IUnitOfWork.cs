using System;

namespace CNISS.CommonDomain.Domain
{
    public interface IUnitOfWork : IDisposable
    {
       
        void commit();
        void rollback();
    }
}
