using CNISS.EnterpriseDomain.Domain.ValueObjects;
using FluentNHibernate.Mapping;

namespace CNISS.EnterpriseDomain.Ports.Output.Database.Mappings
{
    public class DireccionMapping:ClassMap<Direccion>
    {
        public DireccionMapping()
        {
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.referenciaDireccion);
            References(x => x.departamento).Column("CodigoDepartamento");
            References(x => x.municipio).Columns("CodigoDepartamentoMunicipio","CodigoMunicipio");


        }
    }
}