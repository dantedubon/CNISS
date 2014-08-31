using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.EnterpriseDomain.Domain.Entities;
using FluentNHibernate.Mapping;

namespace CNISS.EnterpriseDomain.Ports.Output.Database.Mappings
{
    public class NotaDespidoMapping:ClassMap<NotaDespido>
    {
        public NotaDespidoMapping()
        {
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.FechaDespido);
            Map(x => x.PosicionGps);
            References(x => x.MotivoDespido);
            References(x => x.DocumentoDespido);
            References(x => x.Supervisor);
            References(x => x.FirmaAutorizada);
            Component(x => x.Auditoria, m =>
            {
                m.Map(x => x.CreadoPor);
                m.Map(x => x.FechaCreacion);
                m.Map(x => x.ActualizadoPor);
                m.Map(x => x.FechaActualizacion);
            });
        }
    }
}