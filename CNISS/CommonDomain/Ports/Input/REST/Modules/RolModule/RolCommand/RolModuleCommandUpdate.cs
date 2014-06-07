using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Domain;
using CNISS.CommonDomain.Ports.Input.REST.Request.RolModule;
using Nancy;
using Nancy.ModelBinding;
using NHibernate.Param;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.RolModule.RolCommand
{
    public class RolModuleCommandUpdate : NancyModule
    {
        public RolModuleCommandUpdate(IRolRepositoryReadOnly repositoryRead, ICommandUpdateIdentity<Rol> commandUpdate)
        {
            Put["/rol"] = parameters =>
            {
                var _rolRequest = this.Bind<RolRequest>();
                
                if (repositoryRead.exists(_rolRequest.idGuid))
                {
                    var _rol = new Rol(_rolRequest.name, _rolRequest.description);
                    _rol.Id = _rolRequest.idGuid;
                    commandUpdate.execute(_rol);

                    return new Response()
                        .WithStatusCode(HttpStatusCode.OK);
                }
                return new Response()
                    .WithStatusCode(HttpStatusCode.NotFound);

            };
        }
    }
}