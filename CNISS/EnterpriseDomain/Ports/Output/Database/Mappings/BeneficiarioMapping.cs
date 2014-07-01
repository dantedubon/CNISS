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
        }
    }
}