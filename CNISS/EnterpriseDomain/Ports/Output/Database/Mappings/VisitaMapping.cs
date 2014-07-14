using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.EnterpriseDomain.Domain.Entities;
using FluentNHibernate.Mapping;

namespace CNISS.EnterpriseDomain.Ports.Output.Database.Mappings
{
    public class VisitaMapping:ClassMap<Visita>
    {
        public VisitaMapping()
        {
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.fechaInicial).Index("Indx_fechaInicial");
            Map(x => x.fechaFinal).Index("Indx_fechaFinal");
            Map(x => x.nombre);
            Component(x => x.auditoria, m =>
            {
                m.Map(x => x.usuarioCreo);
                m.Map(x => x.fechaCreo);
                m.Map(x => x.usuarioModifico);
                m.Map(x => x.fechaModifico);
            });
            HasMany(x => x.supervisores).Cascade.All();


        }
    }
}