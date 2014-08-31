using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Domain.Entities
{
    public class NotaDespido:Entity<Guid>
    {
        public virtual MotivoDespido MotivoDespido { get; set; }
        public virtual DateTime FechaDespido { get; set; }
        public virtual ContentFile DocumentoDespido { get; set; }
        public virtual string PosicionGps { get; set; }
        public virtual Supervisor Supervisor { get; set; }
        public virtual FirmaAutorizada FirmaAutorizada { get; set; }
        public virtual Auditoria Auditoria { get; set; }

        protected NotaDespido()
        {
            Id = Guid.NewGuid();
        }

        public NotaDespido(MotivoDespido motivoDespido, DateTime fechaDespido, string posicionGps, Supervisor supervisor, FirmaAutorizada firmaAutorizada):this()
        {
            this.MotivoDespido = motivoDespido;
            this.FechaDespido = fechaDespido;
            PosicionGps = posicionGps;
            this.Supervisor = supervisor;
            this.FirmaAutorizada = firmaAutorizada;
        }
    }
}