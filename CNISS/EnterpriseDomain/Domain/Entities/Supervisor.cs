using System;
using System.Collections.Generic;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.CommonDomain.Domain;

namespace CNISS.EnterpriseDomain.Domain.Entities
{
    public class Supervisor:Entity<Guid>
    {
        public virtual User Usuario { get; protected set; }
        public virtual Auditoria Auditoria { get;  set; }
        public virtual IList<LugarVisita> LugaresVisitas { get;  set; }

        protected Supervisor()
        {
            Id = Guid.NewGuid();
            LugaresVisitas = new List<LugarVisita>();
        }

        public Supervisor(User user):this()
        {
            Usuario = user;
        }

        

        public virtual void addLugarVisita(LugarVisita lugarVisita)
        {
            LugaresVisitas.Add(lugarVisita);
        }
       

    }
}