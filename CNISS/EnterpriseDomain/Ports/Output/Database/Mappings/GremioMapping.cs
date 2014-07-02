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
            HasMany(x => x.empresas).Inverse().Cascade.SaveUpdate().KeyColumn("rtn_gremio");
            Component(x => x.auditoria, m =>
            {
                m.Map(x => x.usuarioCreo);
                m.Map(x => x.fechaCreo);
                m.Map(x => x.usuarioModifico);
                m.Map(x => x.fechaModifico);
            });

        }
    }
}