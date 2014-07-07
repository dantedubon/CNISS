using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
}