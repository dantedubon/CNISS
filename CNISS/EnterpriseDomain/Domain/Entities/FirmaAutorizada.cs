using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Domain;
using NHibernate.Engine;

namespace CNISS.EnterpriseDomain.Domain.Entities
{
    public class FirmaAutorizada:Entity<Guid>
    {
        public virtual String firstName { get; set; }
        public virtual String secondName { get; set; }
        public virtual DateTime fechaCreacion { get; set; }

    }
}