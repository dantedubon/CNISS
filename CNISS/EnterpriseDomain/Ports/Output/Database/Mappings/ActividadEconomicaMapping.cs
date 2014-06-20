using CNISS.EnterpriseDomain.Domain.ValueObjects;
using FluentNHibernate.Mapping;

namespace CNISS.EnterpriseDomain.Ports.Output.Database.Mappings
{
    public class ActividadEconomicaMapping:ClassMap<ActividadEconomica>
    {
        public ActividadEconomicaMapping()
        {
            Id(x => x.Id);
            Map(x => x.descripcion);
        }
    }
}