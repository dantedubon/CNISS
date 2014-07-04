using System;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Domain.Entities
{
    public class ComprobantePago:Entity<Guid>
    {
        public virtual DateTime fechaPago { get; protected set; }
        public virtual decimal deducciones { get; protected set; }
        public virtual decimal sueldoNeto { get; protected set; }
        public virtual decimal bonificaciones { get; set; }
        public virtual decimal total { get; protected set; }
        public virtual ContentFile imagenComprobante { get;  set; }
        public virtual Auditoria auditoria { get; set; }


        protected ComprobantePago()
        {
            Id = Guid.NewGuid();
           
        }

        public ComprobantePago(DateTime fechaPago, decimal deducciones, decimal sueldoNeto, decimal bonificaciones):this()
        {
            this.fechaPago = fechaPago;
            this.deducciones = deducciones;
            this.sueldoNeto = sueldoNeto;
            this.bonificaciones = bonificaciones;
            getTotal(deducciones, sueldoNeto, bonificaciones);


        }

        private void getTotal(decimal deducciones, decimal sueldoNeto, decimal bonificaciones)
        {
            total = (sueldoNeto + bonificaciones) - deducciones;
        }
    }
}