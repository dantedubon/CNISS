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
        public UserModuleQuery(IUserRepositoryReadOnly repository)
        {
            Get["/user"] = parameters =>
            {
                var userResponse = repository.getAll();
                return Response.AsJson(convertToRequest(userResponse))
                    .WithStatusCode(HttpStatusCode.OK);
            };

            Get["/user/id={id}"] = parameters =>
            {
                string idUser = parameters.id;
                if (!string.IsNullOrEmpty(idUser))
                {
                    var user = repository.get(idUser);
                    UserRequest response = convertToRequest(user);
                    return Response.AsJson(response)
                        .WithStatusCode(HttpStatusCode.OK); 
                }
                return new Response()
                    .WithStatusCode(HttpStatusCode.NotAcceptable);



            };
        }

        private UserRequest convertToRequest(User user)
        {
            return new UserRequest
            {
                firstName = user.firstName,
                secondName = user.secondName,
                Id = user.Id,
                mail = user.mail,
                password = "",
                userRol = new RolRequest
                {
                    description = user.userRol.description,
                    name = user.userRol.name,
                    idGuid = user.userRol.Id
                },
                auditoriaRequest = new AuditoriaRequest()
                {
                    fechaCreo =  user.auditoria.fechaCreo,
                    fechaModifico = user.auditoria.fechaModifico,
                    usuarioCreo = user.auditoria.usuarioCreo,
                    usuarioModifico = user.auditoria.usuarioModifico
                }
            };
        }

        private  IEnumerable<UserRequest> convertToRequest(IEnumerable<User> users)
        {
            return users.Select(x => new UserRequest
            {
                firstName = x.firstName,
                secondName = x.secondName,
                Id = x.Id,
                mail = x.mail,
                password = "",
                userRol = new RolRequest
                {
                    description = x.userRol.description,
                    name = x.userRol.name,
                    idGuid = x.userRol.Id,
                    auditoriaRequest = new AuditoriaRequest()
                    {
                        fechaCreo = x.userRol.auditoria.fechaCreo,
                        fechaModifico = x.userRol.auditoria.fechaModifico,
                        usuarioCreo = x.userRol.auditoria.usuarioCreo,
                        usuarioModifico = x.userRol.auditoria.usuarioModifico
                    }
                },
                 auditoriaRequest = new AuditoriaRequest()
                {
                    fechaCreo =  x.auditoria.fechaCreo,
                    fechaModifico = x.auditoria.fechaModifico,
                    usuarioCreo = x.auditoria.usuarioCreo,
                    usuarioModifico = x.auditoria.usuarioModifico
                }
            }).ToList();
        }
    }
}