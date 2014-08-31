using CNISS.EnterpriseDomain.Domain.ValueObjects;
using FluentNHibernate.Mapping;

namespace CNISS.EnterpriseDomain.Ports.Output.Database.Mappings
{
    public class DepartamentoMapping:ClassMap<Departamento>
    {
        public DepartamentoMapping()
        {
            
            Table("Departamentos");
            ReadOnly();
       
            Id(x => x.Id).Column("CodigoDepartamento");
            Map(x => x.Nombre).Column("DescripcionDepartamento");
            HasMany(x => x.Municipios)
                .Inverse()
                .KeyColumns.Add("CodigoDepartamento", mapping => mapping.Name("CodigoDepartamento"));


        }
    }
}