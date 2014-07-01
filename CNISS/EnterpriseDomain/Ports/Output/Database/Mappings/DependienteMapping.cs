using CNISS.EnterpriseDomain.Domain.Entities;
using FluentNHibernate.Mapping;

namespace CNISS.EnterpriseDomain.Ports.Output.Database.Mappings
{
    public class DependienteMapping:ClassMap<Dependiente>
    {
        public DependienteMapping()
        {
            Id(x => x.idGuid).GeneratedBy.Assigned();

            Component(x => x.Id, z => z.Map(x => x.identidad).Index("identidad_indx"));

            Component(x => x.nombre, z =>
            {
                z.Map(x => x.nombres);
                z.Map(x => x.primerApellido);
                z.Map(x => x.segundoApellido);
              
            }
                );

            Map(x => x.edad);

            References(x => x.parentesco);

        }
    }
}