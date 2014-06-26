using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using FluentNHibernate.Mapping;

namespace CNISS.EnterpriseDomain.Ports.Output.Database.Mappings
{
    public class TipoEmpleoMapping:ClassMap<TipoEmpleo>
    {
        public TipoEmpleoMapping()
        {
            Id(x => x.Id);
            Map(x => x.descripcion);
        }
    }
}