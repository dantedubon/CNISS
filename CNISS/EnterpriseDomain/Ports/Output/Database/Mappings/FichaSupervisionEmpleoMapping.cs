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
            Id(x => x.Id).GeneratedBy.Assigned().Column("FichaSupervisionEmpleoId");
            Map(x => x.Cargo);
            Map(x => x.Funciones);
            Map(x => x.TelefonoCelular);
            Map(x => x.TelefonoFijo);
            Map(x => x.PosicionGps);
            Map(x => x.DesempeñoEmpleado);
            References(x => x.Supervisor);
            References(x => x.Firma);

            References(x => x.FotografiaBeneficiario);

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