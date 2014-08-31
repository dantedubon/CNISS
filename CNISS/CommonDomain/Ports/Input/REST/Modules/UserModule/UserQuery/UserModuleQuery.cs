using System.Collections.Generic;
using System.Linq;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.RolModule;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using FluentNHibernate.Conventions;
using Nancy;
using HttpStatusCode = Nancy.HttpStatusCode;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.UserModule.UserQuery
{
    public class UserModuleQuery:NancyModule
    {
        private readonly UserMapping _userMapping;

        public UserModuleQuery(IUserRepositoryReadOnly repository)
        {
            _userMapping = new UserMapping();
            Get["/user"] = parameters =>
            {
                var userResponse = repository.getAll();


                return Response.AsJson(_userMapping.convertToRequest(userResponse))
                    .WithStatusCode(HttpStatusCode.OK);
            };

            
            Get["/user/id={id}"] = parameters =>
            {
                string idUser = parameters.id;
                if (!string.IsNullOrEmpty(idUser))
                {
                    var user = repository.get(idUser);
                    UserRequest response = _userMapping.convertToRequest(user);
                    return Response.AsJson(response)
                        .WithStatusCode(HttpStatusCode.OK); 
                }
                return new Response()
                    .WithStatusCode(HttpStatusCode.NotAcceptable);



            };
        }

        private  IEnumerable<UserRequest> convertToRequest(IEnumerable<User> users)
        {
            return users.Select(x => new UserRequest
            {
                firstName = x.FirstName,
                secondName = x.SecondName,
                Id = x.Id,
                mail = x.Mail,
                password = "",
                userRol = new RolRequest
                {
                    description = x.UserRol.Description,
                    name = x.UserRol.Name,
                    idGuid = x.UserRol.Id,
                    auditoriaRequest = new AuditoriaRequest()
                    {
                        fechaCreo = x.UserRol.Auditoria.FechaCreacion,
                        fechaModifico = x.UserRol.Auditoria.FechaActualizacion,
                        usuarioCreo = x.UserRol.Auditoria.CreadoPor,
                        usuarioModifico = x.UserRol.Auditoria.ActualizadoPor
                    }
                },
                 auditoriaRequest = new AuditoriaRequest()
                {
                    fechaCreo =  x.Auditoria.FechaCreacion,
                    fechaModifico = x.Auditoria.FechaActualizacion,
                    usuarioCreo = x.Auditoria.CreadoPor,
                    usuarioModifico = x.Auditoria.ActualizadoPor
                }
            }).ToList();
        }
    }
}