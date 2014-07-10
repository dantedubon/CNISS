using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Domain;

namespace CNISS.EnterpriseDomain.Domain.Entities
{
    public class Visita : Entity<Guid>
    {
        public virtual string nombre { get; protected set; }
        public virtual DateTime fechaInicial { get; protected set; }
        public virtual DateTime fechaFinal { get; protected set; }
        public virtual Auditoria auditoria { get;  set; }
        public virtual IList<Supervisor> supervisores { get; protected set; }


        public Visita(string nombre, DateTime fechaInicial, DateTime fechaFinal):this()
        {
            this.nombre = nombre;
            this.fechaInicial = fechaInicial;
            this.fechaFinal = fechaFinal;
        }

        public Visita()
        {
            Id = Guid.NewGuid();
            supervisores = new List<Supervisor>();
        }

        public virtual  void addSupervisor(Supervisor supervisor)
        {
            supervisores.Add(supervisor);
        }

    }
}