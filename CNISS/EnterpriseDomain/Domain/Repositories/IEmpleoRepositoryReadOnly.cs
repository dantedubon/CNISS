using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Domain.Repositories
{
    public interface IEmpleoRepositoryReadOnly:IRepositoryReadOnly<Empleo,Guid>
    {
        bool existsEmpleoRecienteParaBeneficiario(DateTime fechaDeBusqueda, int days, Identidad identidadBeneficiario);
        bool existsEmpleoRecienteParaBeneficiario(Guid idEmpleo,DateTime fechaDeBusqueda, int days, Identidad identidadBeneficiario);
 
        IEnumerable<Empleo> getEmpleosByEmpresa(RTN rtn);
        IEnumerable<Empleo> getEmpleosByBeneficiario(Identidad identidad);
        Empleo getEmpleoMasRecienteBeneficiario(Identidad identidad);
        

        bool existsComprobante(Guid empleoid, Guid comprobanteId);
    }
}