using CNISS.EnterpriseDomain.Domain.Entities;
using FluentNHibernate.Mapping;

namespace CNISS.EnterpriseDomain.Ports.Output.Database.Mappings
{
    public class FirmaAutorizadaMapping : ClassMap<FirmaAutorizada>
    {
        public FirmaAutorizadaMapping()
        {
            Id(x => x.Id);
            Map(x => x.fechaCreacion);
            References(x => x.user).Columns(x => x.Id).Column("UserName");
        } 
    }
}