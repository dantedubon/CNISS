﻿using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using NHibernate;

namespace CNISS.EnterpriseDomain.Ports.Output.Database
{
    public class RepresentanteLegalRepositoryReadOnly:NHibernateReadOnlyRepository<RepresentanteLegal,Identidad>,
        IRepresentanteLegalRepositoryReadOnly
    {
        public RepresentanteLegalRepositoryReadOnly(ISession session) : base(session)
        {
        }
    }
}