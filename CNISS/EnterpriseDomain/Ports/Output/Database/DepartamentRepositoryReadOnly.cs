﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using NHibernate;

namespace CNISS.EnterpriseDomain.Ports.Output.Database
{
    public class DepartamentRepositoryReadOnly:NHibernateReadOnlyRepository<Departamento,string>, IDepartamentRepositoryReadOnly
    {
        public DepartamentRepositoryReadOnly(ISession session) : base(session)
        {
           
        }

       
    }
}