using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Domain.Entities
{
    public class FichaSupervisionEmpleo:Entity<Guid>
    {
        public virtual ContentFile fotografiaBeneficiario { get; set; }
        public virtual string posicionGPS { get; set; }
        public virtual string cargo { get; set; }
        public virtual string funciones { get; set; }
        public virtual string telefonoFijo { get; set; }
        public virtual string telefonoCelular { get; set; }
        public virtual FirmaAutorizada firma { get; set; }
        public virtual int desempeñoEmpleado { get; set; }
        public virtual Supervisor supervisor { get; set; }
        public virtual Auditoria auditoria { get; set; }

        protected FichaSupervisionEmpleo()
        {
            Id = Guid.NewGuid();
        }

        public FichaSupervisionEmpleo(Supervisor supervisor, FirmaAutorizada firma, string posicionGps, string cargo, 
            string funciones, string telefonoFijo, string telefonoCelular, int desempeñoEmpleado, 
            ContentFile fotografiaBeneficiario):this()
        {
            this.supervisor = supervisor;
            this.firma = firma;
            posicionGPS = posicionGps;
            this.cargo = cargo;
            this.funciones = funciones;
            this.telefonoFijo = telefonoFijo;
            this.telefonoCelular = telefonoCelular;
            this.desempeñoEmpleado = desempeñoEmpleado;
            this.fotografiaBeneficiario = fotografiaBeneficiario;
        }
    }
}