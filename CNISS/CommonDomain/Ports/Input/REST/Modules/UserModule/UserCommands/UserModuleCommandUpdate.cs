using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Domain;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using Nancy;
using Nancy.ModelBinding;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.UserModule.UserCommands
{
    public class UserModuleCommandUpdate : NancyModule
    {
        public UserModuleCommandUpdate(IUserRepositoryReadOnly repository, ICommandUpdateIdentity<User> commandUpdate)
        {
            Put["/user"] = parameters =>
            {

                var _userRequest = this.Bind<UserRequest>();
                if (_userRequest.isValidPost())
                {
                    if (repository.exists(_userRequest.Id))
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
                        user.auditoria = new Auditoria(_userRequest.auditoriaRequest.usuarioCreo,
                            _userRequest.auditoriaRequest.fechaCreo,_userRequest.auditoriaRequest.usuarioModifico,
                            _userRequest.auditoriaRequest.fechaModifico);
                        commandUpdate.execute(user);

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