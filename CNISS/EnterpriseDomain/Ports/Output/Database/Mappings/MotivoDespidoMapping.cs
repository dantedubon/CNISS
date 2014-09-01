using CNISS.EnterpriseDomain.Domain.ValueObjects;
using FluentNHibernate.Mapping;

namespace CNISS.EnterpriseDomain.Ports.Output.Database.Mappings
{
    public class MotivoDespidoMapping : ClassMap<MotivoDespido>
    {
        public MotivoDespidoMapping()
        {
            Id(x => x.Id).GeneratedBy.Assigned().Column("MotivoDespidoId");
            Map(x => x.Descripcion);
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