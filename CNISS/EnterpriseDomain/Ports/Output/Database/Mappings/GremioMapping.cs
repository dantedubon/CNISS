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
                .KeyProperty(x => x.Id.Rtn);

            Map(x => x.Nombre);
            References(x => x.RepresentanteLegal);
            References(x => x.Direccion);
            HasMany(x => x.Empresas).Inverse().Cascade.SaveUpdate().KeyColumn("rtn_gremio");
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