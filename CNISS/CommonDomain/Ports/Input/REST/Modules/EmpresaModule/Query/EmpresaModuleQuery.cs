using System;
using System.Collections.Generic;
using System.Linq;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.RolModule;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using FluentNHibernate.Testing.Values;
using Nancy;
using Nancy.ModelBinding;

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
                   
                    contentFile = x.contrato == null ? "": x.contrato.Id.ToString(),
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

            Get["/enterprise/id="] = parameters =>
            {
                var rtn = this.Bind<RTNRequest>();
                if (rtn.isValidPost())
                {
                    var rtnEmpresa = new RTN(rtn.RTN);
                    if (rtnEmpresa.isRTNValid())
                    {
                        var empresa = repositoryRead.get(new RTN(rtnEmpresa.rtn));
                        return Response.AsJson(getEmpresaRequest(empresa))
                      .WithStatusCode(HttpStatusCode.OK);
                    }
                  
                }
                return new Response()
                    .WithStatusCode(HttpStatusCode.BadRequest);

            };
        }

        private  EmpresaRequest getEmpresaRequest(Empresa empresa)
        {
            var empresaRequest = new EmpresaRequest()
            {
                nombre = empresa.nombre,
                actividadEconomicaRequests = empresa.actividadesEconomicas.Select(x => new ActividadEconomicaRequest()
                {
                    descripcion = x.descripcion,
                    guid = x.Id
                }),
                contentFile = empresa.contrato == null? "":empresa.contrato.Id.ToString(),
                empleadosTotales = empresa.empleadosTotales,
                fechaIngreso = empresa.fechaIngreso,
                gremioRequest = new GremioRequest()
                {
                    nombre = empresa.gremial.nombre,
                    rtnRequest = new RTNRequest()
                    {
                        RTN = empresa.gremial.Id.rtn
                    }
                },
                programaPiloto = empresa.proyectoPiloto,
                rtnRequest = new RTNRequest() { RTN = empresa.Id.rtn },
                sucursalRequests = empresa.sucursales.Select(x => new SucursalRequest()
                {
                    guid = x.Id,
                    nombre = x.nombre,
                    direccionRequest = new DireccionRequest()
                    {
                        departamentoRequest = new DepartamentoRequest()
                        {
                            idDepartamento = x.direccion.departamento.Id,
                            nombre = x.direccion.departamento.nombre
                        },
                        descripcion = x.direccion.referenciaDireccion,
                        IdGuid = x.Id,
                        municipioRequest = new MunicipioRequest()
                        {
                            idDepartamento = x.direccion.municipio.departamentoId,
                            idMunicipio = x.direccion.municipio.Id,
                            nombre = x.direccion.municipio.nombre
                        }

                    },
                    firmaRequest = new UserRequest()
                    {
                        Id = x.firma.user.Id
                    }
                })
            };
            return empresaRequest;
        }

       
    }
}