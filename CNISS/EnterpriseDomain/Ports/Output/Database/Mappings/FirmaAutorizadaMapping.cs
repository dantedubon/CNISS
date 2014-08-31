using CNISS.EnterpriseDomain.Domain.Entities;
using FluentNHibernate.Mapping;

namespace CNISS.EnterpriseDomain.Ports.Output.Database.Mappings
{
    public class FirmaAutorizadaMapping : ClassMap<FirmaAutorizada>
    {
        public FirmaAutorizadaMapping()
        {
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.fechaCreacion);
            References(x => x.User).Columns(x => x.Id).Column("UserName");
        } 
    }
}