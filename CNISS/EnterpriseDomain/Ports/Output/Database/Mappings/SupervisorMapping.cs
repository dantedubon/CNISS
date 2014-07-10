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
            References(x => x.usuario);
            Component(x => x.auditoria, m =>
            {
                m.Map(x => x.usuarioCreo);
                m.Map(x => x.fechaCreo);
                m.Map(x => x.usuarioModifico);
                m.Map(x => x.fechaModifico);
            });
            HasMany(x => x.lugaresVisitas).Cascade.All();
        }
    }
}