using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using NHibernate;
using NHibernate.Linq;

namespace CNISS.EnterpriseDomain.Ports.Output.Database
{
    public class BeneficiarioRepositoryReadOnly:NHibernateReadOnlyRepository<Beneficiario,Identidad>,IBeneficiarioRepositoryReadOnly
    {
        public BeneficiarioRepositoryReadOnly(ISession session) : base(session)
        {
        }

        public bool exists(Identidad id)
        {
            return (Session.Query<Beneficiario>().Where(beneficiario => beneficiario.Id == id).Select(beneficiario => beneficiario.Id)).Any();
        }

      
    }
}