using System.Collections.Generic;
using System.Linq;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.CommonDomain.Ports.Input.REST.Request.RolModule;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
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
                    idGuid = x.userRol.Id
                }
            }).ToList();
        }
    }
}