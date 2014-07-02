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
            empresas = new List<Empresa>();
        }

        public Gremio(RTN rtn, RepresentanteLegal representanteLegal, Direccion direccion, String nombre):base(rtn)
        {
            if (representanteLegal == null) throw new ArgumentNullException("Representante Legal Nulo");
            if (direccion == null) throw new ArgumentNullException("Direccio Nulo");
            if (string.IsNullOrEmpty(nombre)) throw new ArgumentNullException("Nombre Nulo");
            if (!rtn.isRTNValid())
                throw new ArgumentException("rtn Invalido");
            this.representanteLegal = representanteLegal;
            this.direccion = direccion;
            this.nombre = nombre;
            empresas = new List<Empresa>();

        }

        public virtual Auditoria auditoria { get; set; }
        public virtual string nombre { get; set; }
        public virtual RepresentanteLegal representanteLegal { get; set; }
        public virtual Direccion direccion { get; set; }
        public virtual IEnumerable<Empresa> empresas { get; set; }
 

    }

    public class GremioNull:Gremio
    {
        public virtual  string nombre { get { return string.Empty; } }
        public virtual RepresentanteLegal representanteLegal { get{return new RepresentanteLegalNull();} }
        public virtual Direccion direccion { get{return new DireccionNull();} }
      
    }
}
