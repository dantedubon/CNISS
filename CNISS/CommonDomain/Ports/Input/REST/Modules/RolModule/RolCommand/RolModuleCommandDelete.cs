using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Request.RolModule;
using Nancy;
using Nancy.ModelBinding;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.RolModule.RolCommand
{
    public class RolModuleCommandDelete:NancyModule
    {
        public RolModuleCommandDelete(IRolRepositoryReadOnly repositoryRead, ICommandDeleteIdentity<Rol> commandDelete)
        {
            Delete["/rol"] = parameters =>
            {
                var _rolRequest = this.Bind<RolRequest>();

                if (repositoryRead.exists(_rolRequest.idGuid))
                {
                    var _rol = new Rol(_rolRequest.name, _rolRequest.description);
                    _rol.idKey = _rolRequest.idGuid;
                    commandDelete.execute(_rol);

                    return new Response()
                        .WithStatusCode(HttpStatusCode.OK);
                }
                return new Response()
                    .WithStatusCode(HttpStatusCode.NotFound);

            };
        }
    }
}