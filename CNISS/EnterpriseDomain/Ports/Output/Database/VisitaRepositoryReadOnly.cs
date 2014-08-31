using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace CNISS.EnterpriseDomain.Ports.Output.Database
{
    public class VisitaRepositoryReadOnly:NHibernateReadOnlyRepository<Visita,Guid>,IVisitaRepositoryReadOnly
    {
        public VisitaRepositoryReadOnly(ISession session) : base(session)
        {
        }
        

        public override bool exists(Guid id)
        {
            return (from visita in Session.Query<Visita>()
                    where visita.Id == id
                    select visita.Id).Any();
        }

        public IEnumerable<Visita> visitasEntreFechas(DateTime fechaInicial, DateTime fechaFinal)
        {
            var visitas =
                Session.Query<Visita>()
                    .Where(visita => visita.FechaInicial < fechaFinal && fechaInicial < visita.FechaFinal);
            return visitas.ToList();
        }

        public Supervisor getAgendaSupervisor(User user)
        {
            var fechaActual = DateTime.Now.Date;
            var supervisoresActuales = Session.Query<Visita>()
                .Where(visita => visita.FechaInicial <= fechaActual && visita.FechaFinal >= fechaActual)
                .SelectMany(users => users.Supervisores).ToFuture();

            var resultado = supervisoresActuales.FirstOrDefault(supervisor => supervisor.Usuario.Id == user.Id);
            return resultado;
        }


        public IEnumerable<User> usuariosSinVisitaAgendada(DateTime fechaInicial, DateTime fechaFinal)
        {
       
            var usuariosSupervisores = Session.Query<Visita>()
                .Where(visita => visita.FechaInicial < fechaFinal && fechaInicial < visita.FechaFinal)
                .SelectMany(users => users.Supervisores).ToFuture();

       


            var usuariosNoSupervisores = Session.QueryOver<User>()
               
                .WhereRestrictionOn(u => u.Id)
                .Not.IsIn(usuariosSupervisores.Select(x => x.Usuario.Id).ToList());


            return usuariosNoSupervisores.List().Where(x => x.UserRol.Nivel ==2);


        }
    }
}