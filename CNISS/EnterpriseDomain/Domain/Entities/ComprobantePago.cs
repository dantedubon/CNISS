using System;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Domain.Entities
{
    public class ComprobantePago:Entity<Guid>
    {
        public virtual DateTime FechaPago { get; protected set; }
        public virtual decimal Deducciones { get; protected set; }
        public virtual decimal SueldoNeto { get; protected set; }
        public virtual decimal Bonificaciones { get; set; }
        public virtual decimal Total { get; protected set; }
        public virtual ContentFile ImagenComprobante { get;  set; }
        public virtual Auditoria Auditoria { get; set; }


        protected ComprobantePago()
        {
            Id = Guid.NewGuid();
           
        }

        public ComprobantePago(DateTime fechaPago, decimal deducciones, decimal sueldoNeto, decimal bonificaciones):this()
        {
            this.FechaPago = fechaPago;
            this.Deducciones = deducciones;
            this.SueldoNeto = sueldoNeto;
            this.Bonificaciones = bonificaciones;
            getTotal(deducciones, sueldoNeto, bonificaciones);


        }

        private void getTotal(decimal deducciones, decimal sueldoNeto, decimal bonificaciones)
        {
            Total = (sueldoNeto + bonificaciones) - deducciones;
        }
    }
}