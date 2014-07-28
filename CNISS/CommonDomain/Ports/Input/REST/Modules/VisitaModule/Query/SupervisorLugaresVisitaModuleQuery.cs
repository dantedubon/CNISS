using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.VisitaRequest;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using Nancy;
using Nancy.Security;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.VisitaModule.Query
{
    public class SupervisorLugaresVisitaModuleQuery:NancyModule
    {
        public SupervisorLugaresVisitaModuleQuery(IVisitaRepositoryReadOnly repository)
        {
            Get["/movil/supervisor/lugaresVisita"] = _ =>
            {
               this.RequiresClaims(new[] { "movil" });
                
                var actualUser = this.Context.CurrentUser.UserName;
                var user = new User(actualUser, "", "", "", "", new RolNull());
                
                var supervisor = repository.getAgendaSupervisor(user);
                return supervisor == null ? HttpStatusCode.NotFound : Response.AsJson(getSupervisorRequest(supervisor));
            };
        }

        private  SupervisorRequest getSupervisorRequest(Supervisor supervisor)
        {
            return new SupervisorRequest()
            {
                guid = supervisor.Id,
                userRequest = new UserRequest()
                {
                    Id = supervisor.usuario.Id
                },
                lugarVisitaRequests = supervisor.lugaresVisitas.Select(x => new LugarVisitaRequest()
                {
                    guid = x.Id,
                    empresaRequest = new EmpresaRequest()
                    {
                        nombre = x.empresa.nombre,
                        rtnRequest = new RTNRequest() { RTN = x.empresa.Id.rtn }
                    },
                    sucursalRequest = new SucursalRequest()
                    {
                        guid = x.sucursal.Id,
                        nombre = x.sucursal.nombre,
                        direccionRequest = new DireccionRequest()
                        {
                            departamentoRequest = new DepartamentoRequest()
                            {
                                idDepartamento = x.sucursal.direccion.departamento.Id,
                                nombre = x.sucursal.direccion.departamento.nombre
                            },
                            municipioRequest = new MunicipioRequest()
                            {
                                idDepartamento = x.sucursal.direccion.municipio.departamentoId,
                                idMunicipio = x.sucursal.direccion.municipio.Id,
                                nombre = x.sucursal.direccion.municipio.nombre
                            },
                            descripcion = x.sucursal.direccion.referenciaDireccion
                        },
                        userFirmaRequest = new UserRequest()
                        {
                            Id = x.sucursal.firma.user.Id
                        }
                    }


                }).ToList()
            };
        }
    }
}