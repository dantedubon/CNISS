using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Modules.TipoEmpleoModule.Commands;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.MotivoDespidoRequest;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Nancy;
using Nancy.ModelBinding;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.MotivoDespidoModule.Commands
{
    public class MotivoDespidoModuleUpdate:NancyModule
    {
        public MotivoDespidoModuleUpdate(ICommandUpdateIdentity<MotivoDespido> command )
        {
            Put["/enterprise/motivoDespido"] = parameters =>
            {
                var request = this.Bind<MotivoDespidoRequest>();

                if (request.isValidPut())
                {
                    var mapTipoEmpleo = new MotivoDespidoMapping();
                    var motivoDespido = mapTipoEmpleo.getMotivoDespidoForPut(request);
                    if (command.isExecutable(motivoDespido))
                    { 
                        command.execute(motivoDespido);
                      
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