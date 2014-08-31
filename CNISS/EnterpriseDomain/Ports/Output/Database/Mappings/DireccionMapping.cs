using CNISS.EnterpriseDomain.Domain.ValueObjects;
using FluentNHibernate.Mapping;

namespace CNISS.EnterpriseDomain.Ports.Output.Database.Mappings
{
    public class DireccionMapping:ClassMap<Direccion>
    {
        public DireccionMapping()
        {
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.ReferenciaDireccion);
            References(x => x.Departamento).Column("CodigoDepartamento");
            References(x => x.Municipio).Columns("CodigoDepartamentoMunicipio","CodigoMunicipio");


        }
    }
}