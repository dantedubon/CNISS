using CNISS.EnterpriseDomain.Domain.Entities;
using FluentNHibernate.Mapping;

namespace CNISS.EnterpriseDomain.Ports.Output.Database.Mappings
{
    public class RepresentanteLegalMapping:ClassMap<RepresentanteLegal>
    {
        public RepresentanteLegalMapping()
        {
            CompositeId()
                .ComponentCompositeIdentifier(x => x.Id)
                .KeyProperty(x => x.Id.identidad);

            Map(x => x.Nombre);
        }
    }
}