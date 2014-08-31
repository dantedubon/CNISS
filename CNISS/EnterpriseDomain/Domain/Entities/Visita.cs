using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Domain;

namespace CNISS.EnterpriseDomain.Domain.Entities
{
    public class Visita : Entity<Guid>
    {
        public virtual string Nombre { get; protected set; }
        public virtual DateTime FechaInicial { get; protected set; }
        public virtual DateTime FechaFinal { get; protected set; }
        public virtual Auditoria Auditoria { get;  set; }
        public virtual IList<Supervisor> Supervisores { get;  set; }


        public Visita(string nombre, DateTime fechaInicial, DateTime fechaFinal):this()
        {
            this.Nombre = nombre;
            this.FechaInicial = fechaInicial;
            this.FechaFinal = fechaFinal;
        }

        public Visita()
        {
            Id = Guid.NewGuid();
            Supervisores = new List<Supervisor>();
        }

        public virtual  void addSupervisor(Supervisor supervisor)
        {
            Supervisores.Add(supervisor);
        }

    }
}