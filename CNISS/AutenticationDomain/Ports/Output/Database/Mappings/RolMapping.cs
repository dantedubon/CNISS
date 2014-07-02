using CNISS.AutenticationDomain.Domain.ValueObjects;
using FluentNHibernate.Mapping;

namespace CNISS.AutenticationDomain.Ports.Output.Database.Mappings
{
    public class RolMapping:ClassMap<Rol>
    {
        public RolMapping()
        {
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.name);
            Map(x => x.description);
            Map(x => x.nivel);
            Component(x => x.auditoria, m =>
            {
                m.Map(x => x.usuarioCreo);
                m.Map(x => x.fechaCreo);
                m.Map(x => x.usuarioModifico);
                m.Map(x => x.fechaModifico);
            });


        } 
    }
}