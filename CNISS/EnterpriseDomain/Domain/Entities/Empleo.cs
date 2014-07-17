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
        public virtual Empresa empresa { get;  set; }
        public virtual Sucursal sucursal { get;  set; }
        public virtual Beneficiario beneficiario { get;  set; }
        public virtual HorarioLaboral horarioLaboral { get;  set; }
        public virtual string cargo { get;  set; }
        public virtual decimal sueldo { get;  set; }
        public virtual TipoEmpleo tipoEmpleo { get;  set; }
        public virtual ContentFile contrato { get;  set; }
        public virtual DateTime fechaDeInicio { get;  set; }
        public virtual IList<ComprobantePago>  comprobantesPago { get; protected set; }
        public virtual IList<FichaSupervisionEmpleo> fichasSupervisionEmpleos { get; set; }
        public virtual Auditoria auditoria { get; set; }
        public virtual NotaDespido notaDespido { get; set; }
        public virtual bool supervisado { get; set; }

        public Empleo(Empresa empresa, Sucursal sucursal, Beneficiario beneficiario, HorarioLaboral horarioLaboral,
            string cargo, decimal sueldo, TipoEmpleo tipoEmpleo, DateTime fechaDeInicio)
        {
            this.empresa = empresa;
            this.sucursal = sucursal;
            this.beneficiario = beneficiario;
            this.horarioLaboral = horarioLaboral;
            this.cargo = cargo;
            this.sueldo = sueldo;
            this.tipoEmpleo = tipoEmpleo;
            this.contrato = contrato;
            this.fechaDeInicio = fechaDeInicio;
            comprobantesPago = new List<ComprobantePago>();
            fichasSupervisionEmpleos = new List<FichaSupervisionEmpleo>();
            Id = Guid.NewGuid();
            supervisado = false;
        }

        public virtual void addComprobante(ComprobantePago comprobantePago)
        {
            comprobantesPago.Add(comprobantePago);
            
        }

        public virtual void addFichaSupervision(FichaSupervisionEmpleo fichaSupervision)
        {
            fichasSupervisionEmpleos.Add(fichaSupervision);
        }


        public virtual void removeComprobante(ComprobantePago comprobantePago)
        {
            comprobantesPago.Remove(comprobantePago);
        }

        protected Empleo()
        {
            comprobantesPago = new List<ComprobantePago>();
            Id = Guid.NewGuid();
        }

      
    }
}