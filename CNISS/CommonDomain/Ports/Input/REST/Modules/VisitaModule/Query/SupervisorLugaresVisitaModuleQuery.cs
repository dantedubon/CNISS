﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.Services;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Ports.Input.REST.Request;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.VisitaRequest;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using Nancy;
using Nancy.Authentication.Token;
using Nancy.ModelBinding;
using Nancy.Security;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.VisitaModule.Query
{
    public class SupervisorLugaresVisitaModuleQuery:NancyModule
    {
        public SupervisorLugaresVisitaModuleQuery(ISerializeJsonRequest serializerJson,Func<string,IEncrytRequestProvider> encryptRequestProvider, 
            ITokenizer tokenizer,IVisitaRepositoryReadOnly repository)
        {
            Post["/movil/supervisor/lugaresVisita/"] = _ =>
            {
               

               var movilRequest = this.Bind<MovilRequest>();
               var userId = tokenizer.Detokenize(movilRequest.token, Context);
               if (userId == null)
               {
                   return new Response().WithStatusCode(HttpStatusCode.Unauthorized);
               }



                var actualUser = userId.UserName;
                var user = new User(actualUser, "", "", "", "", new RolNull());
                 var supervisor = repository.getAgendaSupervisor(user);
                if (supervisor == null)
                    return new Response().WithStatusCode(HttpStatusCode.NotFound);

                var agenda = getSupervisorRequest(supervisor);
                var agendaString = serializerJson.toJson(agenda);
                var respuestaEncryptada = encryptRequestProvider(movilRequest.token).encryptString(agendaString);
                return respuestaEncryptada;


            };
        }

        private  SupervisorRequest getSupervisorRequest(Supervisor supervisor)
        {
            return new SupervisorRequest()
            {
                guid = supervisor.Id,
                userRequest = new UserRequest()
                {
                    Id = supervisor.Usuario.Id
                },
                lugarVisitaRequests = supervisor.LugaresVisitas.Select(x => new LugarVisitaRequest()
                {
                    guid = x.Id,
                    empresaRequest = new EmpresaRequest()
                    {
                        nombre = x.Empresa.Nombre,
                        rtnRequest = new RTNRequest() { RTN = x.Empresa.Id.Rtn }
                    },
                    sucursalRequest = new SucursalRequest()
                    {
                        guid = x.Sucursal.Id,
                        nombre = x.Sucursal.Nombre,
                        direccionRequest = new DireccionRequest()
                        {
                            departamentoRequest = new DepartamentoRequest()
                            {
                                idDepartamento = x.Sucursal.Direccion.Departamento.Id,
                                nombre = x.Sucursal.Direccion.Departamento.Nombre
                            },
                            municipioRequest = new MunicipioRequest()
                            {
                                idDepartamento = x.Sucursal.Direccion.Municipio.DepartamentoId,
                                idMunicipio = x.Sucursal.Direccion.Municipio.Id,
                                nombre = x.Sucursal.Direccion.Municipio.Nombre
                            },
                            descripcion = x.Sucursal.Direccion.ReferenciaDireccion
                        },
                        userFirmaRequest = new UserRequest()
                        {
                            Id = x.Sucursal.Firma.User.Id
                        }
                    }


                }).ToList()
            };
        }
    }
}