using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.EnterpriseDomain.Domain.Entities;
using FluentNHibernate.Mapping;

namespace CNISS.EnterpriseDomain.Ports.Output.Database.Mappings
{
    public class FichaSupervisionEmpleoMapping:ClassMap<FichaSupervisionEmpleo>
    {
        public FichaSupervisionEmpleoMapping()
        {
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.cargo);
            Map(x => x.funciones);
            Map(x => x.telefonoCelular);
            Map(x => x.telefonoFijo);
            Map(x => x.posicionGPS);
            Map(x => x.desempeñoEmpleado);
            References(x => x.supervisor);
            References(x => x.firma);

            References(x => x.fotografiaBeneficiario);
        }
    }
}