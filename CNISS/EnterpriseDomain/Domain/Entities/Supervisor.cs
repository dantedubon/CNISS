using System;
using System.Collections.Generic;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.CommonDomain.Domain;

namespace CNISS.EnterpriseDomain.Domain.Entities
{
    public class Supervisor:Entity<Guid>
    {
        public virtual User usuario { get; protected set; }
        public virtual Auditoria auditoria { get; protected set; }
        public virtual IList<LugarVisita> lugaresVisitas { get; protected set; }
       
       

    }
}