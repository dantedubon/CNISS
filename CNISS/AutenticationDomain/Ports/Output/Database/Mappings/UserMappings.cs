using CNISS.AutenticationDomain.Domain.Entities;
using FluentNHibernate.Mapping;

namespace CNISS.AutenticationDomain.Ports.Output.Database.Mappings
{
    public class UserMappings:ClassMap<User>
    {
        public UserMappings()
        {
            Id(x => x.Id).Column("UserId");
            Map(x => x.FirstName);
            Map(x => x.Mail);
            Map(x => x.SecondName);
            Map(x => x.Password);
            Map(x => x.UserKey);
            References(x => x.UserRol);
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