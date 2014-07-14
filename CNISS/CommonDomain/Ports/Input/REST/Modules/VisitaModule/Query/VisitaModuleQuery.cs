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
                fechaInicial = visita.fechaInicial,
                fechaFinal = visita.fechaFinal,
                nombre = visita.nombre,
                auditoriaRequest = new AuditoriaRequest()
                {
                    fechaCreo = visita.auditoria.fechaCreo,
                    fechaModifico = visita.auditoria.fechaModifico,
                    usuarioCreo = visita.auditoria.usuarioCreo,
                    usuarioModifico = visita.auditoria.usuarioModifico
                },
                supervisoresRequests = getSupervisoresRequests(visita.supervisores)
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
                    fechaCreo = x.auditoria.fechaCreo,
                    fechaModifico = x.auditoria.fechaModifico,
                    usuarioCreo = x.auditoria.usuarioCreo,
                    usuarioModifico = x.auditoria.usuarioModifico
                },

                userRequest = new UserRequest()
                {
                    Id = x.usuario.Id,
                    firstName = x.usuario.firstName,
                    mail = x.usuario.mail,
                    secondName = x.usuario.secondName,
                    password = "XXX",
                    userRol = new RolRequest()
                    {
                        idGuid = x.usuario.userRol.Id,

                    }

                },
                lugarVisitaRequests = x.lugaresVisitas.Select(z => new LugarVisitaRequest()
                {
                    guid = z.Id,
                    empresaRequest = new EmpresaRequest()
                    {
                        rtnRequest = new RTNRequest() { RTN = z.empresa.Id.rtn },
                        nombre = z.empresa.nombre
                        
                    },
                    sucursalRequest = new SucursalRequest()
                    {
                        guid = z.sucursal.Id,
                        nombre = z.sucursal.nombre,
                        direccionRequest = new DireccionRequest()
                        {
                            departamentoRequest = new DepartamentoRequest()
                            {
                                idDepartamento = z.sucursal.direccion.departamento.Id,
                                nombre = z.sucursal.direccion.departamento.nombre
                            },
                            municipioRequest = new MunicipioRequest()
                            {
                                idDepartamento = z.sucursal.direccion.municipio.departamentoId,
                                idMunicipio = z.sucursal.direccion.municipio.Id,
                                nombre = z.sucursal.direccion.municipio.nombre
                            },
                            descripcion = z.sucursal.direccion.referenciaDireccion


                        }
                        
                    },
                    auditoriaRequest = new AuditoriaRequest()
                    {
                        fechaCreo = z.auditoria.fechaCreo,
                        fechaModifico = z.auditoria.fechaModifico,
                        usuarioCreo = z.auditoria.usuarioCreo,
                        usuarioModifico = z.auditoria.usuarioModifico
                    },



                }).ToList()
            }).ToList();
        } 
        
    }
}