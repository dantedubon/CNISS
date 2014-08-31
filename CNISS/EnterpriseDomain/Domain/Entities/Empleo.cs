using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Domain.Entities
{
    public class Empleo:Entity<Guid>
    {
        public virtual Empresa Empresa { get;  set; }
        public virtual Sucursal Sucursal { get;  set; }
        public virtual Beneficiario Beneficiario { get;  set; }
        public virtual HorarioLaboral HorarioLaboral { get;  set; }
        public virtual string Cargo { get;  set; }
        public virtual decimal Sueldo { get;  set; }
        public virtual TipoEmpleo TipoEmpleo { get;  set; }
        public virtual ContentFile Contrato { get;  set; }
        public virtual DateTime FechaDeInicio { get;  set; }
        public virtual IList<ComprobantePago>  ComprobantesPago { get; protected set; }
        public virtual IList<FichaSupervisionEmpleo> FichasSupervisionEmpleos { get; set; }
        public virtual Auditoria Auditoria { get; set; }
        public virtual NotaDespido NotaDespido { get; set; }
        public virtual bool Supervisado { get; set; }

        public Empleo(Empresa empresa, Sucursal sucursal, Beneficiario beneficiario, HorarioLaboral horarioLaboral,
            string cargo, decimal sueldo, TipoEmpleo tipoEmpleo, DateTime fechaDeInicio)
        {
            this.Empresa = empresa;
            this.Sucursal = sucursal;
            this.Beneficiario = beneficiario;
            this.HorarioLaboral = horarioLaboral;
            this.Cargo = cargo;
            this.Sueldo = sueldo;
            this.TipoEmpleo = tipoEmpleo;
            this.Contrato = Contrato;
            this.FechaDeInicio = fechaDeInicio;
            ComprobantesPago = new List<ComprobantePago>();
            FichasSupervisionEmpleos = new List<FichaSupervisionEmpleo>();
            Id = Guid.NewGuid();
            Supervisado = false;
        }

        public virtual void addComprobante(ComprobantePago comprobantePago)
        {
            ComprobantesPago.Add(comprobantePago);
            
        }

        public virtual void addFichaSupervision(FichaSupervisionEmpleo fichaSupervision)
        {
            FichasSupervisionEmpleos.Add(fichaSupervision);
        }


        public virtual void removeComprobante(ComprobantePago comprobantePago)
        {
            ComprobantesPago.Remove(comprobantePago);
        }

        protected Empleo()
        {
            ComprobantesPago = new List<ComprobantePago>();
            Id = Guid.NewGuid();
        }

      
    }
}