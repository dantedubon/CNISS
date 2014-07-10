using System;
using System.Collections.Generic;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.CommonDomain.Domain;

namespace CNISS.EnterpriseDomain.Domain.Entities
{
    public class Supervisor:Entity<Guid>
    {
        public virtual User usuario { get; protected set; }
        public virtual Auditoria auditoria { get;  set; }
        public virtual IList<LugarVisita> lugaresVisitas { get;  set; }

        protected Supervisor()
        {
            Id = Guid.NewGuid();
            lugaresVisitas = new List<LugarVisita>();
        }

        public Supervisor(User user):this()
        {
            usuario = user;
        }

        

        public virtual void addLugarVisita(LugarVisita lugarVisita)
        {
            lugaresVisitas.Add(lugarVisita);
        }
       

    }
}