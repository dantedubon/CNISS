using System.Data;
using CNISS.CommonDomain.Domain;
using NHibernate;

namespace CNISS.CommonDomain.Ports.Output.Database
{
    public class NHibernateUnitOfWork:IUnitOfWork
    {
        
        private readonly ITransaction _transaction;
        private ISession _session;


        public NHibernateUnitOfWork(ISession session)
        {

            _session = session;
            _session.FlushMode = FlushMode.Auto;
            _transaction = _session.BeginTransaction(IsolationLevel.ReadCommitted);


        }

        public ISession Session
        {
            get { return _session; }
            
        }

        public void Dispose()
        {
            if (_session == null) return;
            _session.Close();
            _session = null;
        }

        public void rollback()
        {
            if (_transaction.IsActive)
                _transaction.Rollback();
        }

        public void commit()
        {
            if (_transaction.IsActive)
                _transaction.Commit();
        }

        
    }
}