using System;
using System.Collections.Generic;
using System.Linq;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.RolModule;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Repositories;
using FluentNHibernate.Testing.Values;
using Nancy;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.EmpresaModule.Query
{
    public class EmpresaModuleQuery:NancyModule
    {
        public EmpresaModuleQuery(IEmpresaRepositoryReadOnly repositoryRead)
        {
            Get["/enterprise"] = parameters =>
            {
                var empresas = repositoryRead.getAll();

                var response = empresas.Select(x => new EmpresaRequest()
                {
                    rtnRequest = new RTNRequest() { RTN = x.Id.rtn},
                   
                    contentFile = "",
                    empleadosTotales = x.empleadosTotales,
                    fechaIngreso = x.fechaIngreso,
                    actividadEconomicaRequests = new List<ActividadEconomicaRequest>(),
                    gremioRequest = new GremioRequest(),
                    sucursalRequests = new List<SucursalRequest>(),
                    nombre = x.nombre,
                    programaPiloto = x.proyectoPiloto,
                    

                });

                return Response.AsJson(response);
            };
        }

       

       
    }
}