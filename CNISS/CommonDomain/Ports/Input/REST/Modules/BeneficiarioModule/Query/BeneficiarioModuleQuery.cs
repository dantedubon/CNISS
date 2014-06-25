using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.BeneficiarioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using Nancy;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.BeneficiarioModule.Query
{
    public class BeneficiarioModuleQuery:NancyModule
    {
        public BeneficiarioModuleQuery(IBeneficiarioRepositoryReadOnly repository)
        {
            Get["/enterprise/beneficiarios"] = parameters =>
            {
                var beneficiarios = repository.getAll();
                return Response.AsJson(beneficiarios.Select(getShortBeneficiarioRequest).ToList());
            };
        }

        private BeneficiarioRequest getShortBeneficiarioRequest(Beneficiario beneficiario)
        {
            return new BeneficiarioRequest()
            {
                identidadRequest = new IdentidadRequest() { identidad = beneficiario.Id.identidad},
                nombreRequest = new NombreRequest() {
                    nombres = beneficiario.nombre.nombres,
                    primerApellido = beneficiario.nombre.primerApellido, 
                    segundoApellido = beneficiario.nombre.segundoApellido
                },
                fechaNacimiento = beneficiario.fechaNacimiento,
                dependienteRequests = new List<DependienteRequest>()
                
            };
        }
    }
}