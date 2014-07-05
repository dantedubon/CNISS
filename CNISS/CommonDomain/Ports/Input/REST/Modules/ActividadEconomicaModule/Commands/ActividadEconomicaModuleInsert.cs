using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;
using CNISS.EnterpriseDomain.Application;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Nancy;
using Nancy.ModelBinding;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.ActividadEconomicaModule.Commands
{
    public class ActividadEconomicaModuleInsert:NancyModule
    {
        private readonly ActividadEconomicaMapping _actividadEconomicaMapping;

        public ActividadEconomicaModuleInsert(ICommandInsertIdentity<ActividadEconomica> command)
        {
            _actividadEconomicaMapping = new ActividadEconomicaMapping();
            Post["/enterprise/actividades"] = parameters =>
            {
                var request = this.Bind<ActividadEconomicaRequest>();

                if (request.isValidPost())
                {
                    var actividad = _actividadEconomicaMapping.getActividadEconomicaForPost(request);
                    command.execute(actividad);
                    return new Response()
                   .WithStatusCode(HttpStatusCode.OK);
                }

                return new Response()
                    .WithStatusCode(HttpStatusCode.BadRequest);
            };
        }
    }
}