using System;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Domain.Entities
{
    public class ComprobantePago:Entity<Guid>
    {
        public virtual DateTime fechaPago { get; protected set; }
        public virtual decimal deducciones { get; protected set; }
        public virtual decimal percepciones { get; protected set; }
        public virtual decimal total { get; protected set; }
        public virtual ContentFile imagenComprobante { get;  set; }
        public virtual Auditoria auditoria { get; set; }

        protected ComprobantePago()
        {
            Id = Guid.NewGuid();
           
        }

        public ComprobantePago(DateTime fechaPago, decimal deducciones, decimal percepciones, decimal total):this()
        {
            this.fechaPago = fechaPago;
            this.deducciones = deducciones;
            this.percepciones = percepciones;
            this.total = total;
            
          
        }


    }
}