using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using FluentNHibernate.Mapping;

namespace CNISS.AutenticationDomain.Ports.Output.Database.Mappings
{
    public class RolMapping:ClassMap<Rol>
    {
        public RolMapping()
        {
            Id(x => x.idKey);
            Map(x => x.name);
            Map(x => x.description);


        } 
    }
}