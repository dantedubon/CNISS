using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Modules.MotivoDespidoModule.Commands;
using CNISS.CommonDomain.Ports.Input.REST.Request.BeneficiarioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.MotivoDespidoRequest;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Nancy;
using Nancy.ModelBinding;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.ParentescoModule.Commands
{
    public class ParentescoModuleUpdate:NancyModule
    {
        public ParentescoModuleUpdate(ICommandUpdateIdentity<Parentesco> command)
        {
            Put["/enterprise/beneficiarios/parentescos"] = parameters =>
            {
                var request = this.Bind<ParentescoRequest>();

                if (request.isValidPut())
                {
                    var mapTipoEmpleo = new ParentescoMapping();
                    var motivoDespido = mapTipoEmpleo.getParentescoForPut(request);
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