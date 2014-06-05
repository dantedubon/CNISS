using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Domain;
using NHibernate;

namespace CNISS.CommonDomain.Ports.Output.Database
{
    public class NHibernateUnitOfWork:IUnitOfWork
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly ITransaction _transaction;
        private ISession _session;


        public NHibernateUnitOfWork(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
            _session = _sessionFactory.OpenSession();
            _transaction = _session.BeginTransaction();


        }

        public ISession Session
        {
            get { return _session; }
            
        }

        public void Dispose()
        {
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