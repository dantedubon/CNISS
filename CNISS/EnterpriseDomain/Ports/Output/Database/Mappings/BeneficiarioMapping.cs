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

            Component(x => x.nombre, z =>
            {
                z.Map(x => x.nombres);
                z.Map(x => x.primerApellido);
                z.Map(x => x.segundoApellido);
              
            }
                );

            Map(x => x.fechaNacimiento);
            HasMany(x => x.dependientes).Cascade.All();

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