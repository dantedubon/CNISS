using System;
using System.Collections.Generic;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Domain.Entities
{
    public class Empresa:EnterpriseEntities
    {
        public virtual  string nombre { get; set; }
        public virtual IEnumerable<ActividadEconomica> actividadesEconomicas { get; set; }
        public virtual IEnumerable<Sucursal> sucursales { get; set; }
        public virtual DateTime fechaIngreso { get; set; }
        public virtual int empleadosTotales { get; set; }
        public virtual bool proyectoPiloto { get; set; }
        public virtual Gremio gremial { get; set; }
        public virtual ContentFile contrato { get; set; }

        protected Empresa()
        {
            
        }

        public Empresa(RTN rtnEmpresa, string nombre,DateTime fechaIngreso, Gremio gremial)
        {
            this.nombre = nombre;
            this.fechaIngreso = fechaIngreso;
            this.gremial = gremial;
            Id = rtnEmpresa;
        }
    }
}