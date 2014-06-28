using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Domain.Entities
{
    public class Empleo:Entity<Guid>
    {
        public virtual Empresa empresa { get; protected set; }
        public virtual Sucursal sucursal { get; protected set; }
        public virtual Beneficiario beneficiario { get; protected set; }
        public virtual HorarioLaboral horarioLaboral { get; protected set; }
        public virtual string cargo { get; protected set; }
        public virtual decimal sueldo { get; protected set; }
        public virtual TipoEmpleo tipoEmpleo { get; protected set; }
        public virtual ContentFile contrato { get; protected set; }
        public virtual DateTime fechaDeInicio { get; protected set; }
        public virtual IList<ComprobantePago>  comprobantesPago { get; protected set; }

        public Empleo(Empresa empresa, Sucursal sucursal, Beneficiario beneficiario, HorarioLaboral horarioLaboral,
            string cargo, decimal sueldo, TipoEmpleo tipoEmpleo, ContentFile contrato, DateTime fechaDeInicio)
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
            Id = Guid.NewGuid();
        }

        public virtual void addComprobante(ComprobantePago comprobantePago)
        {
            comprobantesPago.Add(comprobantePago);
        }

        protected Empleo()
        {
            comprobantesPago = new List<ComprobantePago>();
            Id = Guid.NewGuid();
        }
    }
}