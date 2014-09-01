using CNISS.AutenticationDomain.Domain.ValueObjects;
using FluentNHibernate.Mapping;

namespace CNISS.AutenticationDomain.Ports.Output.Database.Mappings
{
    public class RolMapping:ClassMap<Rol>
    {
        public RolMapping()
        {
            Id(x => x.Id).GeneratedBy.Assigned().Column("RolId");
            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.Nivel);
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