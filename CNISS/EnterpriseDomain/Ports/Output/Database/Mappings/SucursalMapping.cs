using CNISS.EnterpriseDomain.Domain.Entities;
using FluentNHibernate.Mapping;

namespace CNISS.EnterpriseDomain.Ports.Output.Database.Mappings
{
    public class SucursalMapping : ClassMap<Sucursal>
    {
        public SucursalMapping()
        {
            Id(x => x.Id).GeneratedBy.Assigned().Column("SucursalId");
            Map(x => x.Nombre);
            References(x => x.Direccion);
            References(x => x.Firma);
            Component(x => x.Auditoria, m =>
            {
                m.Map(x => x.CreadoPor);
                m.Map(x => x.FechaCreacion);
                m.Map(x => x.ActualizadoPor);
                m.Map(x => x.FechaActualizacion);
            });

        }
    }
}