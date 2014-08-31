using System;
using System.Collections.Generic;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Domain.Entities
{
    public class Gremio : EnterpriseEntities
    {
        protected Gremio()
        {
            Empresas = new List<Empresa>();
        }

        public Gremio(RTN rtn, RepresentanteLegal representanteLegal, Direccion direccion, String nombre):base(rtn)
        {
            if (representanteLegal == null) throw new ArgumentNullException("Representante Legal Nulo");
            if (direccion == null) throw new ArgumentNullException("Direccio Nulo");
            if (string.IsNullOrEmpty(nombre)) throw new ArgumentNullException("Nombre Nulo");
            if (!rtn.isRTNValid())
                throw new ArgumentException("rtn Invalido");
            this.RepresentanteLegal = representanteLegal;
            this.Direccion = direccion;
            this.Nombre = nombre;
            Empresas = new List<Empresa>();

        }

        public virtual Auditoria Auditoria { get; set; }
        public virtual string Nombre { get; set; }
        public virtual RepresentanteLegal RepresentanteLegal { get; set; }
        public virtual Direccion Direccion { get; set; }
        public virtual IEnumerable<Empresa> Empresas { get; set; }
 

    }

    public class GremioNull:Gremio
    {
        public virtual  string Nombre { get { return string.Empty; } }
        public virtual RepresentanteLegal RepresentanteLegal { get{return new RepresentanteLegalNull();} }
        public virtual Direccion Direccion { get{return new DireccionNull();} }
      
    }
}
