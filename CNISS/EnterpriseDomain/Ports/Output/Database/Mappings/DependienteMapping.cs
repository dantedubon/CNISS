using CNISS.EnterpriseDomain.Domain.Entities;
using FluentNHibernate.Mapping;

namespace CNISS.EnterpriseDomain.Ports.Output.Database.Mappings
{
    public class DependienteMapping:ClassMap<Dependiente>
    {
        public DependienteMapping()
        {
            Id(x => x.idGuid).GeneratedBy.Assigned().Column("DependienteId");

            Component(x => x.Id, z => z.Map(x => x.identidad).Index("identidad_indx"));

            Component(x => x.Nombre, z =>
            {
                z.Map(x => x.Nombres);
                z.Map(x => x.PrimerApellido);
                z.Map(x => x.SegundoApellido);
              
            }
                );

            Map(x => x.FechaNacimiento);

            References(x => x.Parentesco);

            Component(x => x.auditoria, m =>
            {
                m.Map(x => x.CreadoPor);
                m.Map(x => x.FechaCreacion);
                m.Map(x => x.ActualizadoPor);
                m.Map(x => x.FechaActualizacion);
            });

        }
    }
}