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
                z.Map(x => x.primerNombre);
                z.Map(x => x.primerApellido);
                z.Map(x => x.segundoApellido);
                z.Map(x => x.segundoNombre);
            }
                );

            Map(x => x.fechaNacimiento);
            HasManyToMany(x => x.dependientes)
                .Cascade.All().
                Table("DependientesBeneficiario");
        }
    }
}