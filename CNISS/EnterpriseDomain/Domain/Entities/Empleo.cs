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
        public virtual IList<ComprobantePago>  comprobantesPago { get; protected set; }


    }
}