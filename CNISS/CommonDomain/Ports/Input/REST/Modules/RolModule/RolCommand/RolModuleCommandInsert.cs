using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Domain;
using CNISS.CommonDomain.Ports.Input.REST.Request.RolModule;
using Nancy;
using Nancy.ModelBinding;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.RolModule.RolCommand
{
    public class RolModuleCommandInsert:NancyModule
    {
       

        public RolModuleCommandInsert(ICommandInsertIdentity<Rol> command)
        {

            Post["/rol"] = parameters =>
            {
                var _rolRequest = this.Bind<RolRequest>();
                if (string.IsNullOrEmpty(_rolRequest.name) || string.IsNullOrEmpty(_rolRequest.description))
                    return new Response() {}
                        .WithStatusCode(HttpStatusCode.BadRequest);
                
                var _rol = new Rol(_rolRequest.name, _rolRequest.description);

                var auditoriaRequest = _rolRequest.auditoriaRequest;
                _rol.auditoria = new Auditoria(auditoriaRequest.usuarioCreo,auditoriaRequest.fechaCreo,auditoriaRequest.usuarioModifico,auditoriaRequest.fechaModifico);
                _rol.nivel = _rolRequest.nivel;
                command.execute(_rol);
                return new Response() {}
                    .WithStatusCode(HttpStatusCode.OK);
            };

        }
    }
}