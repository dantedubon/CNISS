using System;
using System.Collections.Generic;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Domain.Entities
{
    public class Empresa:EnterpriseEntities, IEquatable<Empresa>
    {
        
        public virtual  string Nombre { get; set; }
        public virtual IEnumerable<ActividadEconomica> ActividadesEconomicas { get; set; }
        public virtual IList<Sucursal> Sucursales { get; set; }
        public virtual DateTime FechaIngreso { get; set; }
        public virtual int EmpleadosTotales { get; set; }
        public virtual bool ProyectoPiloto { get; set; }
        public virtual Gremio Gremial { get; set; }
        public virtual ContentFile Contrato { get; set; }
        public virtual Auditoria Auditoria { get; set; }

        protected Empresa()
        {
            Sucursales = new List<Sucursal>();
        }


        public virtual void AddSucursal(Sucursal sucursal)
        {
           Sucursales.Add(sucursal);
        }

        public Empresa(RTN rtnEmpresa, string nombre,DateTime fechaIngreso, Gremio gremial):this()
        {
            this.Nombre = nombre;
            this.FechaIngreso = fechaIngreso;
            this.Gremial = gremial;
            Id = rtnEmpresa;
        }

        public virtual bool Equals(Empresa other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id);
        }

        public  override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Empresa)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Id != null ? Nombre.GetHashCode() : 0);
              
                return hashCode;
            }
        }

    }
}