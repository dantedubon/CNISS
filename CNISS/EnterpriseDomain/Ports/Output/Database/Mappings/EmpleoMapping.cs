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
                   
                });
                z.Component(x => x.horaSalida, h =>
                {
                    h.Map(v => v.hora, "horaSalida");
                    h.Map(v => v.minutos, "minutosSalida");
                    h.Map(v => v.parte, "parteSalida");
                   
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
            References(x => x.contrato);
            HasMany(x => x.comprobantesPago).Cascade.All();
            Component(x => x.auditoria, m =>
            {
                m.Map(x => x.usuarioCreo);
                m.Map(x => x.fechaCreo);
                m.Map(x => x.usuarioModifico);
                m.Map(x => x.fechaModifico);
            });
        }
    }
}