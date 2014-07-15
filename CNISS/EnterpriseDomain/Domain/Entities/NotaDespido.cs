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
        public virtual MotivoDespido motivoDespido { get; set; }
        public virtual DateTime fechaDespido { get; set; }
        public virtual ContentFile documentoDespido { get; set; }
        public virtual string posicionGPS { get; set; }
        public virtual Supervisor supervisor { get; set; }
        public virtual FirmaAutorizada firmaAutorizada { get; set; }

        protected NotaDespido()
        {
            Id = Guid.NewGuid();
        }

        public NotaDespido(MotivoDespido motivoDespido, DateTime fechaDespido, string posicionGps, Supervisor supervisor, FirmaAutorizada firmaAutorizada):this()
        {
            this.motivoDespido = motivoDespido;
            this.fechaDespido = fechaDespido;
            posicionGPS = posicionGps;
            this.supervisor = supervisor;
            this.firmaAutorizada = firmaAutorizada;
        }
    }
}