using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using NHibernate;
using NHibernate.Linq;

namespace CNISS.EnterpriseDomain.Ports.Output.Database
{
    public class EmpleoRepositoryCommands:NHibernateCommandRepository<Empleo,Guid>,IEmpleoRepositoryCommands
    {
        public EmpleoRepositoryCommands(ISession session) : base(session)
        {
        }

        public void save(Empleo entity)
        {
            base.save(entity);
           
        }

        
    }
}