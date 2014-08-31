using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Domain;
using CNISS.CommonDomain.Ports.Input.REST.Request.RolModule;
using Nancy;
using Nancy.ModelBinding;

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
                    var rol = new Rol(_rolRequest.name, _rolRequest.description);
                    var auditoriaRequest = _rolRequest.auditoriaRequest;
                    rol.Auditoria = new Auditoria(auditoriaRequest.usuarioCreo, auditoriaRequest.fechaCreo, auditoriaRequest.usuarioModifico, auditoriaRequest.fechaModifico);


                    rol.Id = _rolRequest.idGuid;
                    rol.Nivel = _rolRequest.nivel;
                    commandUpdate.execute(rol);

                    return new Response()
                        .WithStatusCode(HttpStatusCode.OK);
                }
                return new Response()
                    .WithStatusCode(HttpStatusCode.NotFound);

            };
        }
    }
}