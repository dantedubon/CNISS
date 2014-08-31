using System;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.CommonDomain.Domain;

namespace CNISS.EnterpriseDomain.Domain.Entities
{
    public class FirmaAutorizada:Entity<Guid>
    {
        public virtual User User { get; set; }


        public virtual DateTime fechaCreacion { get; set; }

        public FirmaAutorizada(User user, DateTime fechaCreacion)
        {
            
            Id = Guid.NewGuid();
            this.fechaCreacion = fechaCreacion;
            this.User = user;

        }

        protected FirmaAutorizada()
        {
            Id = Guid.NewGuid();
        }
    }

    public class FirmaAutorizadaNull:FirmaAutorizada
    {
        public virtual User user { get{return new UserNull();} }
        public virtual DateTime fechaCreacion { get { return DateTime.MinValue; } }
    }


}