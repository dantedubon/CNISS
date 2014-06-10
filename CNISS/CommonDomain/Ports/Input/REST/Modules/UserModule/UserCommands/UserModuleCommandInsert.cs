using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using Nancy;
using Nancy.ModelBinding;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.UserModule.UserCommands
{
    public class UserModuleCommandInsert:NancyModule
    {

        public UserModuleCommandInsert(IUserRepositoryReadOnly repository, ICommandInsertIdentity<User> commandInsert )
        {
            Post["/user"] = parameters =>
            {
                 var _userRequest = this.Bind<UserRequest>();
                if (_userRequest.isValidPost())
                {
                    if (!repository.exists(_userRequest.Id))
                    {
                        var _userRol = new Rol(_userRequest.userRol.name,
                            _userRequest.userRol.description);
                        _userRol.Id = _userRequest.userRol.idGuid;

                        var user = new User(_userRequest.Id,
                            _userRequest.firstName,
                            _userRequest.secondName,
                            _userRequest.password,
                            _userRequest.mail,
                           _userRol);
                        commandInsert.execute(user);
                       
                        return new Response()
                   .WithStatusCode(HttpStatusCode.OK);
                    }
                }

               
               

                return new Response()
                    .WithStatusCode(HttpStatusCode.NotAcceptable);

            };
        }

     
    }
}