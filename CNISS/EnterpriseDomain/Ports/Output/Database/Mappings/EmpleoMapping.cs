using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.WebParts;
using CNISS.EnterpriseDomain.Domain.Entities;
using FluentNHibernate.Mapping;

namespace CNISS.EnterpriseDomain.Ports.Output.Database.Mappings
{
    public class EmpleoMapping:ClassMap<Empleo>
    {
        public EmpleoMapping()
        {
            Id(x => x.Id);
            Map(x => x.cargo);
            Map(x => x.sueldo);
            Map(x => x.fechaDeInicio);
            References(x => x.tipoEmpleo);
            References(x => x.empresa,"rtn_empresa");
            References(x => x.beneficiario,"identidad_beneficiario");
            References(x => x.sucursal);
            Component(x => x.horarioLaboral, z =>
            {
                z.Component(x => x.horaEntrada, h =>
                {
                    h.Map(v => v.hora,"horaEntrada");
                    h.Map(v => v.minutos,"minutosEntrada");
                    h.Map(v => v.parte, "parteEntrada");
                    //   h.Component(v => v.parteDia, w => w.Map(d => d.parte,"parteDiaEntrada"));
                });
                z.Component(x => x.horaSalida, h =>
                {
                    h.Map(v => v.hora, "horaSalida");
                    h.Map(v => v.minutos, "minutosSalida");
                    h.Map(v => v.parte, "parteSalida");
                    //     h.Component(v => v.parteDia, w => w.Map(d => d.parte, "parteDiaSalida"));
                });
                z.Component(x => x.diasLaborables, h =>
                {
                    h.Map(v => v.lunes);
                    h.Map(v => v.martes);
                    h.Map(v => v.miercoles);
                    h.Map(v => v.jueves);
                    h.Map(v => v.viernes);
                    h.Map(v => v.sabado);
                    h.Map(v => v.domingo);
                });
            });
            Component(x => x.contrato, z => z.Map(x => x.dataFile));
            HasMany(x => x.comprobantesPago).Cascade.AllDeleteOrphan();
        }
    }
}