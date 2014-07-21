using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Modules.MotivoDespidoModule.Commands;
using CNISS.CommonDomain.Ports.Input.REST.Request.BeneficiarioRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.MotivoDespidoRequest;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Nancy;
using Nancy.ModelBinding;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.ParentescoModule.Commands
{
    public class ParentescoModuleInsert:NancyModule
    {
        private readonly ParentescoMapping _parentescoMapping;

        public ParentescoModuleInsert(ICommandInsertIdentity<Parentesco> command)
        {
            _parentescoMapping = new ParentescoMapping();
            Post["/enterprise/beneficiarios/parentescos"] = parameters =>
            {
                var request = this.Bind<ParentescoRequest>();
                if (request.isValidPost())
                {
                    var motivoDespido = _parentescoMapping.getParentescoForPost(request);
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