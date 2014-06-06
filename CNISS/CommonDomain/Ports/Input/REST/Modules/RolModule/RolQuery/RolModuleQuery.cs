using System;
using CNISS.AutenticationDomain.Domain.Repositories;
using Nancy;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.RolModule.RolQuery
{
    public class RolModuleQuery:NancyModule
    {
        public RolModuleQuery(IRolRepositoryReadOnly repository)
        {
          
            Get["/rol"] = parameters =>
            {
               
                var rolModule = repository.getAll();
                return Response.AsJson(rolModule)
                 .WithStatusCode(HttpStatusCode.OK);


            };

            Get["/rol/id={id}"] = parameters =>
            {
                Guid id;
                if (Guid.TryParse(parameters.id, out id))
                {
                    return Response.AsJson(repository.get(id))
                     .WithStatusCode(HttpStatusCode.OK);
                }
                return new Response() { }
                    .WithStatusCode(HttpStatusCode.NotFound);
            };

        }
    }
}