using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Domain.Entities
{
    public class Sucursal:Entity<Guid>
    {
        public Sucursal(string nombre, Direccion direccion, FirmaAutorizada firmaAutorizada):this()
        {
            
            this.nombre = nombre;
            this.direccion = direccion;
            this.firma = firmaAutorizada;
        }

        protected Sucursal()
        {
            Id = Guid.NewGuid();
        }
        public virtual string nombre { get; set; }
        public virtual Direccion direccion { get; set; }
        public virtual FirmaAutorizada firma { get; set; }
       
    }
}