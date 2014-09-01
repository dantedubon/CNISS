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
            Id(x => x.Id).Column("EmpleoId");
            Map(x => x.Cargo);
            Map(x => x.Sueldo);
            Map(x => x.FechaDeInicio);
            Map(x => x.Supervisado);
            References(x => x.TipoEmpleo);
            References(x => x.Empresa,"rtn_empresa");
            References(x => x.Beneficiario,"identidad_beneficiario");
            References(x => x.Sucursal);
            Component(x => x.HorarioLaboral, z =>
            {
                z.Component(x => x.HoraEntrada, h =>
                {
                    h.Map(v => v.HoraEntera,"horaEntrada");
                    h.Map(v => v.Minutos,"minutosEntrada");
                    h.Map(v => v.Parte, "parteEntrada");
                   
                });
                z.Component(x => x.HoraSalida, h =>
                {
                    h.Map(v => v.HoraEntera, "horaSalida");
                    h.Map(v => v.Minutos, "minutosSalida");
                    h.Map(v => v.Parte, "parteSalida");
                   
                });
                z.Component(x => x.DiasLaborables, h =>
                {
                    h.Map(v => v.Lunes);
                    h.Map(v => v.Martes);
                    h.Map(v => v.Miercoles);
                    h.Map(v => v.Jueves);
                    h.Map(v => v.Viernes);
                    h.Map(v => v.Sabado);
                    h.Map(v => v.Domingo);
                });
            });
            References(x => x.Contrato);
            References(x => x.NotaDespido).Cascade.All();
            HasMany(x => x.ComprobantesPago).Cascade.All();
            HasMany(x => x.FichasSupervisionEmpleos).Cascade.All();
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