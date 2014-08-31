using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.EnterpriseDomain.Domain.Entities;
using FluentNHibernate.Mapping;

namespace CNISS.EnterpriseDomain.Ports.Output.Database.Mappings
{
    public class SupervisorMapping:ClassMap<Supervisor>
    {
        public SupervisorMapping()
        {
            Id(x => x.Id).GeneratedBy.Assigned();
            References(x => x.Usuario);
            Component(x => x.Auditoria, m =>
            {
                m.Map(x => x.CreadoPor);
                m.Map(x => x.FechaCreacion);
                m.Map(x => x.ActualizadoPor);
                m.Map(x => x.FechaActualizacion);
            });
            HasMany(x => x.LugaresVisitas).Cascade.All();
        }
    }
}