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
                .KeyProperty(x => x.Id.Rtn);
            Map(x => x.EmpleadosTotales);
            Map(x => x.FechaIngreso);
            Map(x => x.Nombre);
            Map(x => x.ProyectoPiloto);
            
            References(x => x.Gremial,"rtn_gremio").Not.Nullable();

            
            HasMany(x => x.Sucursales);
              
            HasManyToMany(x => x.ActividadesEconomicas)
                .Cascade.All()
                .Table("ActividadesEconomicasEmpresas");
            References(x => x.Contrato);

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