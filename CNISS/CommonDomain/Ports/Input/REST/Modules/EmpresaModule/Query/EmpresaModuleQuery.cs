using System;
using System.Collections.Generic;
using System.Linq;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
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
        public EmpresaModuleQuery(IEmpresaRepositoryReadOnly repositoryRead,IGremioRepositoryReadOnly gremioRepositoryRead)
        {
            Get["/enterprise"] = parameters =>
            {
                var empresas = repositoryRead.getAll();

                var response = empresas.Select(x => new EmpresaRequest()
                {
                    rtnRequest = new RTNRequest() { RTN = x.Id.Rtn},
                   
                    contentFile = x.Contrato == null ? "": x.Contrato.Id.ToString(),
                    empleadosTotales = x.EmpleadosTotales,
                    fechaIngreso = x.FechaIngreso,
                    actividadEconomicaRequests = new List<ActividadEconomicaRequest>(),
                    gremioRequest = new GremioRequest(),
                    sucursalRequests = new List<SucursalRequest>(),
                    nombre = x.Nombre,
                    programaPiloto = x.ProyectoPiloto,
                    auditoriaRequest = new AuditoriaRequest()
                    {
                        fechaCreo = x.Auditoria.FechaCreacion,
                        fechaModifico = x.Auditoria.FechaActualizacion,
                        usuarioCreo = x.Auditoria.CreadoPor,
                        usuarioModifico = x.Auditoria.ActualizadoPor
                    }

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
                        var empresa = repositoryRead.get(new RTN(rtnEmpresa.Rtn));
                        return Response.AsJson(getEmpresaRequest(empresa))
                      .WithStatusCode(HttpStatusCode.OK);
                    }
                  
                }
                return new Response()
                    .WithStatusCode(HttpStatusCode.BadRequest);

            };

            Get["/enterprise/by/gremio/id="] = parameters =>
            {
                var rtn = this.Bind<RTNRequest>();
                if (rtn.isValidPost())
                {
                    var rtnGremio = new RTN(rtn.RTN);
                    if (rtnGremio.isRTNValid())
                    {
                        var empresas = gremioRepositoryRead.get(rtnGremio).Empresas;
                        var response = empresas.Select(x => new EmpresaRequest()
                        {
                            rtnRequest = new RTNRequest() { RTN = x.Id.Rtn },

                            contentFile = x.Contrato == null ? "" : x.Contrato.Id.ToString(),
                            empleadosTotales = x.EmpleadosTotales,
                            fechaIngreso = x.FechaIngreso,
                            actividadEconomicaRequests = new List<ActividadEconomicaRequest>(),
                            gremioRequest = new GremioRequest(),
                            sucursalRequests = new List<SucursalRequest>(),
                            nombre = x.Nombre,
                            programaPiloto = x.ProyectoPiloto,
                            auditoriaRequest = new AuditoriaRequest()
                            {
                                fechaCreo = x.Auditoria.FechaCreacion,
                                fechaModifico = x.Auditoria.FechaActualizacion,
                                usuarioCreo = x.Auditoria.CreadoPor,
                                usuarioModifico = x.Auditoria.ActualizadoPor
                            }

                        });

                        return Response.AsJson(response);
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
                nombre = empresa.Nombre,
                actividadEconomicaRequests = empresa.ActividadesEconomicas.Select(x => new ActividadEconomicaRequest()
                {
                    descripcion = x.Descripcion,
                    guid = x.Id
                }),
                contentFile = empresa.Contrato == null? "":empresa.Contrato.Id.ToString(),
                empleadosTotales = empresa.EmpleadosTotales,
                fechaIngreso = empresa.FechaIngreso,
                gremioRequest = new GremioRequest()
                {
                    nombre = empresa.Gremial.Nombre,
                    rtnRequest = new RTNRequest()
                    {
                        RTN = empresa.Gremial.Id.Rtn
                    }
                },
                programaPiloto = empresa.ProyectoPiloto,
                rtnRequest = new RTNRequest() { RTN = empresa.Id.Rtn },
                sucursalRequests = empresa.Sucursales.Select(x => new SucursalRequest()
                {
                    guid = x.Id,
                    nombre = x.Nombre,
                    direccionRequest = new DireccionRequest()
                    {
                        departamentoRequest = new DepartamentoRequest()
                        {
                            idDepartamento = x.Direccion.Departamento.Id,
                            nombre = x.Direccion.Departamento.Nombre
                        },
                        descripcion = x.Direccion.ReferenciaDireccion,
                        IdGuid = x.Id,
                        municipioRequest = new MunicipioRequest()
                        {
                            idDepartamento = x.Direccion.Municipio.DepartamentoId,
                            idMunicipio = x.Direccion.Municipio.Id,
                            nombre = x.Direccion.Municipio.Nombre
                        }

                    },
                    userFirmaRequest = new UserRequest()
                    {
                        Id = x.Firma.User.Id
                    },
                    auditoriaRequest = new AuditoriaRequest()
                    {
                        fechaCreo = x.Auditoria.FechaCreacion,
                        fechaModifico = x.Auditoria.FechaActualizacion,
                        usuarioCreo = x.Auditoria.CreadoPor,
                        usuarioModifico = x.Auditoria.ActualizadoPor
                    }
                }),
                auditoriaRequest = new AuditoriaRequest()
                {
                    fechaCreo = empresa.Auditoria.FechaCreacion,
                    fechaModifico = empresa.Auditoria.FechaActualizacion,
                    usuarioCreo = empresa.Auditoria.CreadoPor,
                    usuarioModifico = empresa.Auditoria.ActualizadoPor
                }
            };
            return empresaRequest;
        }

       
    }
}