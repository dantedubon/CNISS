using CNISS.EnterpriseDomain.Domain.Entities;
using FluentNHibernate.Mapping;

namespace CNISS.EnterpriseDomain.Ports.Output.Database.Mappings
{
    public class BeneficiarioMapping:ClassMap<Beneficiario>
    {
        public BeneficiarioMapping()
        {

            CompositeId()
                .ComponentCompositeIdentifier(x => x.Id)
                .KeyProperty(x => x.Id.identidad);

            Component(x => x.Nombre, z =>
            {
                z.Map(x => x.Nombres);
                z.Map(x => x.PrimerApellido);
                z.Map(x => x.SegundoApellido);
              
            }
                );

            Map(x => x.FechaNacimiento);
            HasMany(x => x.Dependientes).Cascade.All();


            Map(x => x.TelefonoCelular);
            Map((x => x.TelefonoFijo));
            References(x => x.FotografiaBeneficiario);
            References(x => x.Direccion).Cascade.All();
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