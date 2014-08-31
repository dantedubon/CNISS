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
            Map(x => x.Names).Column("NombresBeneficiario");
            Map(x => x.FirstSurname).Column("PrimerApellido");
            Map(x => x.SecondSurname).Column("SegundoApellido");
            Map(x => x.DateBirth).Column("FechaDeNacimiento");
        }
    }
}