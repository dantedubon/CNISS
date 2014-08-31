using CNISS.EnterpriseDomain.Domain.ValueObjects;
using FluentNHibernate.Mapping;

namespace CNISS.EnterpriseDomain.Ports.Output.Database.Mappings
{
    public class MunicipiosMapping:ClassMap<Municipio>
    {
        public MunicipiosMapping()
        {
            Table("Municipios");
            ReadOnly();

            CompositeId().KeyProperty(x => x.DepartamentoId, "CodigoDepartamento")
                .KeyProperty( x => x.Id,"CodigoMunicipio");
            Map(x => x.Nombre).Column("DescripcionMunicipio");
            


        }
    }
}