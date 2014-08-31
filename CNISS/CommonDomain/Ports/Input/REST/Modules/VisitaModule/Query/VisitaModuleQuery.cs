using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.RolModule;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.VisitaRequest;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using Nancy;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.VisitaModule.Query
{
    public class VisitaModuleQuery:NancyModule
    {
        public VisitaModuleQuery(IVisitaRepositoryReadOnly repository)
        {
            Get["/visita"] = parameters =>
            {

                var visitas = repository.getAll();

                return Response.AsJson(visitas.Select(getVisitaRequest));

            };
            Get["/visita/{id:guid}"] = parameters =>
            {
                Guid id = parameters.id;
                var visita = repository.get(id);
                return visita!= null ? Response.AsJson(getVisitaRequest(visita)) : new Response().WithStatusCode(HttpStatusCode.NotFound);
            };

            Get["/visita/{fechaInicial:datetime(yyyy-MM-dd)}/{fechaFinal:datetime(yyyy-MM-dd)}"] = parameters =>
            {
                DateTime fechaInicial = parameters.fechaInicial;
                DateTime fechaFinal = parameters.fechaFinal;
                var visitas = repository.visitasEntreFechas(fechaInicial, fechaFinal);
                return Response.AsJson(visitas.Select(getVisitaRequest));

            };


        }


        private  VisitaRequest getVisitaRequest(Visita visita)
        {
            var visitaRequest = new VisitaRequest()
            {
                guid = visita.Id,
                fechaInicial = visita.FechaInicial,
                fechaFinal = visita.FechaFinal,
                nombre = visita.Nombre,
                auditoriaRequest = new AuditoriaRequest()
                {
                    fechaCreo = visita.Auditoria.FechaCreacion,
                    fechaModifico = visita.Auditoria.FechaActualizacion,
                    usuarioCreo = visita.Auditoria.CreadoPor,
                    usuarioModifico = visita.Auditoria.ActualizadoPor
                },
                supervisoresRequests = getSupervisoresRequests(visita.Supervisores)
            };

            return visitaRequest;
        }


        private  IList<SupervisorRequest> getSupervisoresRequests(IEnumerable<Supervisor> supervisores)
        {
            return supervisores.Select(x => new SupervisorRequest()
            {
                guid = x.Id,
                auditoriaRequest = new AuditoriaRequest()
                {
                    fechaCreo = x.Auditoria.FechaCreacion,
                    fechaModifico = x.Auditoria.FechaActualizacion,
                    usuarioCreo = x.Auditoria.CreadoPor,
                    usuarioModifico = x.Auditoria.ActualizadoPor
                },

                userRequest = new UserRequest()
                {
                    Id = x.Usuario.Id,
                    firstName = x.Usuario.FirstName,
                    mail = x.Usuario.Mail,
                    secondName = x.Usuario.SecondName,
                    password = "XXX",
                    userRol = new RolRequest()
                    {
                        idGuid = x.Usuario.UserRol.Id,

                    }

                },
                lugarVisitaRequests = x.LugaresVisitas.Select(z => new LugarVisitaRequest()
                {
                    guid = z.Id,
                    empresaRequest = new EmpresaRequest()
                    {
                        rtnRequest = new RTNRequest() { RTN = z.Empresa.Id.Rtn },
                        nombre = z.Empresa.Nombre
                        
                    },
                    sucursalRequest = new SucursalRequest()
                    {
                        guid = z.Sucursal.Id,
                        nombre = z.Sucursal.Nombre,
                        direccionRequest = new DireccionRequest()
                        {
                            departamentoRequest = new DepartamentoRequest()
                            {
                                idDepartamento = z.Sucursal.Direccion.Departamento.Id,
                                nombre = z.Sucursal.Direccion.Departamento.Nombre
                            },
                            municipioRequest = new MunicipioRequest()
                            {
                                idDepartamento = z.Sucursal.Direccion.Municipio.DepartamentoId,
                                idMunicipio = z.Sucursal.Direccion.Municipio.Id,
                                nombre = z.Sucursal.Direccion.Municipio.Nombre
                            },
                            descripcion = z.Sucursal.Direccion.ReferenciaDireccion


                        }
                        
                    },
                    auditoriaRequest = new AuditoriaRequest()
                    {
                        fechaCreo = z.Auditoria.FechaCreacion,
                        fechaModifico = z.Auditoria.FechaActualizacion,
                        usuarioCreo = z.Auditoria.CreadoPor,
                        usuarioModifico = z.Auditoria.ActualizadoPor
                    },



                }).ToList()
            }).ToList();
        } 
        
    }
}