using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using FluentNHibernate.Mapping;

namespace CNISS.EnterpriseDomain.Ports.Output.Database.Mappings
{
    public class MunicipiosMapping:ClassMap<Municipio>
    {
        public MunicipiosMapping()
        {
            Table("Municipios");
            ReadOnly();
      
            CompositeId().KeyProperty(x => x.departamentoId, "CodigoDepartamento")
                .KeyProperty( x => x.Id,"CodigoMunicipio");
            Map(x => x.nombre).Column("DescripcionMunicipio");
            References(x => x.departamento)
                .Class<Departamento>()
                .Columns("CodigoDepartamento");


        }
    }
}