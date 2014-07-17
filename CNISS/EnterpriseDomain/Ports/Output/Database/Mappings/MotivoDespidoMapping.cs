using CNISS.EnterpriseDomain.Domain.ValueObjects;
using FluentNHibernate.Mapping;

namespace CNISS.EnterpriseDomain.Ports.Output.Database.Mappings
{
    public class MotivoDespidoMapping : ClassMap<MotivoDespido>
    {
        public MotivoDespidoMapping()
        {
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.descripcion);
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