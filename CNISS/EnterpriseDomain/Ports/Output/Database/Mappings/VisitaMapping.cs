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
            Map(x => x.FechaInicial).Index("Indx_fechaInicial");
            Map(x => x.FechaFinal).Index("Indx_fechaFinal");
            Map(x => x.Nombre);
            Component(x => x.Auditoria, m =>
            {
                m.Map(x => x.CreadoPor);
                m.Map(x => x.FechaCreacion);
                m.Map(x => x.ActualizadoPor);
                m.Map(x => x.FechaActualizacion);
            });
            HasMany(x => x.Supervisores).Cascade.All();


        }
    }
}