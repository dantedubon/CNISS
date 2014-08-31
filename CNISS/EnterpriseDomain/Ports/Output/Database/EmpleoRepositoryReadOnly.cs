using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace CNISS.EnterpriseDomain.Ports.Output.Database
{
    public class EmpleoRepositoryReadOnly:NHibernateReadOnlyRepository<Empleo,Guid>,IEmpleoRepositoryReadOnly
    {
        public EmpleoRepositoryReadOnly(ISession session) : base(session)
        {
        }


        public bool existsEmpleoRecienteParaBeneficiario(DateTime fechaDeBusqueda, int days, Identidad identidadBeneficiario)
        {
            var fechaBase = fechaDeBusqueda.AddDays(-days);
            return (from empleo in Session.Query<Empleo>()
                where
                    empleo.Beneficiario.Id == identidadBeneficiario && empleo.FechaDeInicio > fechaBase
                select empleo.Id
                ).Any();
            ;
        }

        public bool existsEmpleoRecienteParaBeneficiario(Guid idEmpleo, DateTime fechaDeBusqueda, int days,
            Identidad identidadBeneficiario)
        {
            var fechaBase = fechaDeBusqueda.AddDays(-days);
            return (from empleo in Session.Query<Empleo>()
                    where
                        empleo.Beneficiario.Id == identidadBeneficiario && empleo.FechaDeInicio > fechaBase && empleo.Id != idEmpleo
                    select empleo.Id
                ).Any();
            ;
        }

        public IEnumerable<Empleo> getEmpleosByEmpresa(RTN rtn)
        {
            return (from empleo in Session.Query<Empleo>()
                where
                    empleo.Empresa.Id == rtn
                select empleo
                );

        }

        public IEnumerable<Empleo> getEmpleosByBeneficiario(Identidad identidad)
        {
            return (from empleo in Session.Query<Empleo>()
                    where
                        empleo.Beneficiario.Id == identidad
                       
                    select empleo
                    
                );
        }

        public Empleo getEmpleoMasRecienteBeneficiario(Identidad identidad)
        {


           return (Session.QueryOver<Empleo>()

                .Where(x => x.Beneficiario.Id == identidad)
                .OrderBy(x => x.FechaDeInicio).Desc
                .List<Empleo>()).FirstOrDefault();



        }

        public bool existsComprobante(Guid empleoid, Guid comprobanteId)
        {
            var empleo = Session.Get<Empleo>(empleoid);
            return empleo != null && empleo.ComprobantesPago.Any(x => x.Id == comprobanteId);
        }

        public bool existsEmpleoForNotaDespido(Guid empleoId, DateTime fecha)
        {
            return (from empleo in Session.Query<Empleo>()
                    where empleo.Id == empleoId && empleo.FechaDeInicio < fecha && empleo.NotaDespido == null
                    select empleo.Id
                ).Any()
            ;
        }

        public override bool exists(Guid id)
        {
            return (from empleo in Session.Query<Empleo>()
                   where empleo.Id == id 
                   select empleo.Id
                ).Any()
            ;
        }
    }
}