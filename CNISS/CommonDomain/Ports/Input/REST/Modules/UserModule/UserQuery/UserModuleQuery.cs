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