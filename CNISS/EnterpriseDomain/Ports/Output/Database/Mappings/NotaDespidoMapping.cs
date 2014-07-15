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
            Id(x => x.Id);
            Map(x => x.fechaDespido);
            Map(x => x.posicionGPS);
            References(x => x.motivoDespido);
            References(x => x.documentoDespido);
            References(x => x.supervisor);
            References(x => x.firmaAutorizada);
        }
    }
}