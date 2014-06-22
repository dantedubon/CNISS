using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.CommonDomain.Domain;
using NHibernate.Engine;

namespace CNISS.EnterpriseDomain.Domain.Entities
{
    public class FirmaAutorizada:Entity<Guid>
    {
        public virtual User user { get; set; }


        public virtual DateTime fechaCreacion { get; set; }

        public FirmaAutorizada(User user)
        {
            
            Id = Guid.NewGuid();
            fechaCreacion = DateTime.Now;
            this.user = user;

        }

        protected FirmaAutorizada()
        {
            
        }
    }
}