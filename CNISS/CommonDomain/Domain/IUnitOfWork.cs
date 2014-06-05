using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNISS.CommonDomain.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        void commit();
        void rollback();
    }
}
