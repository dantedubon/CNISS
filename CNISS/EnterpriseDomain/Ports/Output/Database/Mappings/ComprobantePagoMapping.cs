using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.EnterpriseDomain.Domain.Entities;
using FluentNHibernate.Mapping;

namespace CNISS.EnterpriseDomain.Ports.Output.Database.Mappings
{
    public class ComprobantePagoMapping:ClassMap<ComprobantePago>
    {
        public ComprobantePagoMapping()
        {
            Id(x => x.Id).GeneratedBy.Assigned().Column("ComprobantePagoId");
            Map(x => x.Deducciones);
            Map(x => x.SueldoNeto);
            Map(x => x.Bonificaciones);
            Map(x => x.Total);
            Map(x => x.FechaPago);
            References(x => x.ImagenComprobante);
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