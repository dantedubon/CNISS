using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Entities;
using FluentNHibernate.Mapping;

namespace CNISS.AutenticationDomain.Ports.Output.Database.Mappings
{
    public class UserMappings:ClassMap<User>
    {
        public UserMappings()
        {
            Id(x => x.Id);
            Map(x => x.firstName);
            Map(x => x.mail);
            Map(x => x.password);
            Map(x => x.secondName);
            References(x => x.userRol);
        }
    }
}