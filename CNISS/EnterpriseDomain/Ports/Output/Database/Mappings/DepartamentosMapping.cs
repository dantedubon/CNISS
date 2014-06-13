using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using FluentNHibernate.Mapping;
using NHibernate.Mapping.ByCode.Conformist;

namespace CNISS.EnterpriseDomain.Ports.Output.Database.Mappings
{
    public class DepartamentoMapping:ClassMap<Departamento>
    {
        public DepartamentoMapping()
        {
            
            Table("Departamentos");
            ReadOnly();
            LazyLoad();
            Id(x => x.Id).Column("CodigoDepartamento");
            Map(x => x.nombre).Column("DescripcionDepartamento");
            HasMany(x => x.municipios)
                .Inverse()
                .KeyColumns.Add("CodigoDepartamento", mapping => mapping.Name("CodigoDepartamento"));


        }
    }
}