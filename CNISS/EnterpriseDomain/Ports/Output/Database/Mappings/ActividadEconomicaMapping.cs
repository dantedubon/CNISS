using CNISS.EnterpriseDomain.Domain.ValueObjects;
using FluentNHibernate.Mapping;

namespace CNISS.EnterpriseDomain.Ports.Output.Database.Mappings
{
    public class ActividadEconomicaMapping:ClassMap<ActividadEconomica>
    {
        public ActividadEconomicaMapping()
        {
            Id(x => x.Id).Column("ActividadEconomicaId");
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