using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Modules.TipoEmpleoModule.Commands;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.MotivoDespidoRequest;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Nancy;
using Nancy.ModelBinding;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.MotivoDespidoModule.Commands
{
    public class MotivoDespidoModuleInsert:NancyModule
    {
        private readonly MotivoDespidoMapping _motivoDespidoMapping;

        public MotivoDespidoModuleInsert(ICommandInsertIdentity<MotivoDespido> command )
        {
            _motivoDespidoMapping = new MotivoDespidoMapping();
            Post["/enterprise/motivoDespido"] = parameters =>
            {
                var request = this.Bind<MotivoDespidoRequest>();
                if (request.isValidPost())
                {
                    var motivoDespido = _motivoDespidoMapping.getMotivoDespidoForPost(request);
                    command.execute(motivoDespido);

                    return new Response()
                   .WithStatusCode(HttpStatusCode.OK);
                }
                return new Response()
                    .WithStatusCode(HttpStatusCode.BadRequest);
            };
        }
    }
}