using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using Nancy;
using Nancy.ModelBinding;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.UserModule.UserCommands
{
    public class UserModuleCommandDelete : NancyModule
    {
        public UserModuleCommandDelete(IUserRepositoryReadOnly repository, ICommandDeleteIdentity<User> commandDelete)
        {
            Delete["/user"] = parameters =>
            {
                var _userRequest = this.Bind<UserRequest>();
                if (_userRequest.isValidDelete())
                {
                    if (repository.exists(_userRequest.Id))
                    {

                        var user = new User(_userRequest.Id,
                            _userRequest.firstName,
                            _userRequest.secondName,
                            _userRequest.password,
                            _userRequest.mail,
                           null);
                        commandDelete.execute(user);

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