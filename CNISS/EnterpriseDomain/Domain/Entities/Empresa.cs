﻿using System;
using System.Collections.Generic;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Domain.Entities
{
    public class Empresa:EnterpriseEntities, IEquatable<Empresa>
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
                int hashCode = (Id != null ? nombre.GetHashCode() : 0);
              
                return hashCode;
            }
        }

    }
}