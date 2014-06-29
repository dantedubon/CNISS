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
                    empleo.beneficiario.Id == identidadBeneficiario && empleo.fechaDeInicio > fechaBase
                select empleo.Id
                ).Any();
            ;
        }

        public IEnumerable<Empleo> getEmpleosByEmpresa(RTN rtn)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Empleo> getEmpleosByBeneficiario(Identidad identidad)
        {
            throw new NotImplementedException();
        }
    }
}