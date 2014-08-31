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
        public virtual ContentFile FotografiaBeneficiario { get; set; }
        public virtual string PosicionGps { get; set; }
        public virtual string Cargo { get; set; }
        public virtual string Funciones { get; set; }
        public virtual string TelefonoFijo { get; set; }
        public virtual string TelefonoCelular { get; set; }
        public virtual FirmaAutorizada Firma { get; set; }
        public virtual int DesempeñoEmpleado { get; set; }
        public virtual Supervisor Supervisor { get; set; }
        public virtual Auditoria Auditoria { get; set; }

        protected FichaSupervisionEmpleo()
        {
            Id = Guid.NewGuid();
        }

        public FichaSupervisionEmpleo(Supervisor supervisor, FirmaAutorizada firma, string posicionGps, string cargo, 
            string funciones, string telefonoFijo, string telefonoCelular, int desempeñoEmpleado, 
            ContentFile fotografiaBeneficiario):this()
        {
            this.Supervisor = supervisor;
            this.Firma = firma;
            PosicionGps = posicionGps;
            this.Cargo = cargo;
            this.Funciones = funciones;
            this.TelefonoFijo = telefonoFijo;
            this.TelefonoCelular = telefonoCelular;
            this.DesempeñoEmpleado = desempeñoEmpleado;
            this.FotografiaBeneficiario = fotografiaBeneficiario;
        }
    }
}