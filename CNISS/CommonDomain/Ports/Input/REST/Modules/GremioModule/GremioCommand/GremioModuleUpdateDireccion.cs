using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.EnterpriseDomain.Application;
using Nancy;
using Nancy.ModelBinding;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.GremioModule.GremioCommand
{
    public class GremioModuleUpdateDireccion:NancyModule
    {
        public GremioModuleUpdateDireccion(ICommandUpdateGremioDireccion command)
        {
            var _gremioMap = new GremioMap();
            Put["enterprise/gremio/direccion"] = parameters =>
            {
                var request = this.Bind<GremioRequest>();
                if (request.isValidPutDireccion())
                {
                    var gremio = _gremioMap.getGremioForPost(request);
                    if (command.isExecutable(gremio))
                    {
                        command.execute(gremio);
                        return new Response()
                       .WithStatusCode(HttpStatusCode.OK);
                    }
                  
                }
                return new Response()
                 .WithStatusCode(HttpStatusCode.BadRequest);
               
            };
        }
    }
}