using CNISS.EnterpriseDomain.Domain.Entities;
using FluentNHibernate.Mapping;

namespace CNISS.EnterpriseDomain.Ports.Output.Database.Mappings
{
    public class EmpresaMapping : ClassMap<Empresa>
    {
        public EmpresaMapping()
        {
            CompositeId()
                .ComponentCompositeIdentifier(x => x.Id)
                .KeyProperty(x => x.Id.rtn);
            Map(x => x.empleadosTotales);
            Map(x => x.fechaIngreso);
            Map(x => x.nombre);
            Map(x => x.proyectoPiloto);
            
            References(x => x.gremial,"rtn_gremio").Not.Nullable();

            
            HasMany(x => x.sucursales);
              
            HasManyToMany(x => x.actividadesEconomicas)
                .Cascade.All()
                .Table("ActividadesEconomicasEmpresas");
            References(x => x.contrato).Cascade.All();
        }
    }
}