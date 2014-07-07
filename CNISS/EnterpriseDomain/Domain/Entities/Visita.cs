using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.CommonDomain.Domain;

namespace CNISS.EnterpriseDomain.Domain.Entities
{
    public class Visita:Entity<Guid>
    {
        public virtual string nombre { get; protected  set; }
        public virtual DateTime fechaInicial { get;protected set; }
        public virtual DateTime fechaFinal { get; protected set; }
        public virtual Auditoria auditoria { get; protected set; }    
        public virtual IList<Supervisor> supervisores { get; protected set; }
        
    }

    public class Supervisor:Entity<Guid>
    {
        public virtual User usuario { get; set; }
        public virtual Auditoria auditoria { get; set; }
        public virtual IList<LugarVisita> lugaresVisitas { get; set; }
       
       
    }

    public class LugarVisita:Entity<Guid>
    {
        public virtual Empresa empresa { get; set; }
        public virtual Sucursal sucursal { get; set; }
        public Auditoria auditoria { get; set; }
    }


}