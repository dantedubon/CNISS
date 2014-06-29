using CNISS.AutenticationDomain.Domain.ValueObjects;
using FluentNHibernate.Mapping;

namespace CNISS.AutenticationDomain.Ports.Output.Database.Mappings
{
    public class RolMapping:ClassMap<Rol>
    {
        public RolMapping()
        {
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.name);
            Map(x => x.description);


        } 
    }
}