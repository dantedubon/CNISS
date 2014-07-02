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
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.deducciones);
            Map(x => x.percepciones);
            Map(x => x.total);
            Map(x => x.fechaPago);
            References(x => x.imagenComprobante).Cascade.All();
            Component(x => x.auditoria, m =>
            {
                m.Map(x => x.usuarioCreo);
                m.Map(x => x.fechaCreo);
                m.Map(x => x.usuarioModifico);
                m.Map(x => x.fechaModifico);
            });
        }
    }
}