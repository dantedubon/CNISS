using CNISS.EnterpriseDomain.Domain.Entities;
using FluentNHibernate.Mapping;

namespace CNISS.EnterpriseDomain.Ports.Output.Database.Mappings
{
    public class GremioMapping : ClassMap<Gremio>
    {
        public GremioMapping()
        {
            CompositeId()
                .ComponentCompositeIdentifier(x => x.Id)
                .KeyProperty(x => x.Id.rtn);

            Map(x => x.nombre);
            References(x => x.representanteLegal);
            References(x => x.direccion);
      
        }
    }
}