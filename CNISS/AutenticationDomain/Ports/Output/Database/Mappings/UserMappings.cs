using CNISS.AutenticationDomain.Domain.Entities;
using FluentNHibernate.Mapping;

namespace CNISS.AutenticationDomain.Ports.Output.Database.Mappings
{
    public class UserMappings:ClassMap<User>
    {
        public UserMappings()
        {
            Id(x => x.Id);
            Map(x => x.firstName);
            Map(x => x.mail);
            Map(x => x.secondName);
            Map(x => x.password);
            Map(x => x.userKey);
            References(x => x.userRol);
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