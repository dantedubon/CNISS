using CNISS.EnterpriseDomain.Domain.ValueObjects;
using FluentNHibernate.Mapping;

namespace CNISS.EnterpriseDomain.Ports.Output.Database.Mappings
{
    public class ParentescoMapping:ClassMap<Parentesco>
    {
        public ParentescoMapping()
        {
            Id(x => x.Id);
            Map(x => x.descripcion);
        }
    }
}