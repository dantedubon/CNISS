using CNISS.EnterpriseDomain.Domain.Entities;
using FluentNHibernate.Mapping;

namespace CNISS.EnterpriseDomain.Ports.Output.Database.Mappings
{
    public class PersonRNPMapping : ClassMap<PersonRNP>
    {
        public PersonRNPMapping()
        {
            Table("vuInscripcionesRNP");
            ReadOnly();
            LazyLoad();
            Id(x => x.Id).Column("NumeroIdentidad");
            Map(x => x.names).Column("NombresBeneficiario");
            Map(x => x.firstSurname).Column("PrimerApellido");
            Map(x => x.secondSurname).Column("SegundoApellido");
            Map(x => x.dateBirth).Column("FechaDeNacimiento");
        }
    }
}