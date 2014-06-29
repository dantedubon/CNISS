using CNISS.EnterpriseDomain.Domain.Entities;
using FluentNHibernate.Mapping;

namespace CNISS.EnterpriseDomain.Ports.Output.Database.Mappings
{
    public class SucursalMapping : ClassMap<Sucursal>
    {
        public SucursalMapping()
        {
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.nombre);
            References(x => x.direccion);
            References(x => x.firma);

        }
    }
}