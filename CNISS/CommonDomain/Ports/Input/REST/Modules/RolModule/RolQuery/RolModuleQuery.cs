using System;
using System.Collections.Generic;
using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.AutenticationDomain.Domain.ValueObjects;
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

                Guid id = parameters.id;
               
                 return Response.AsJson(repository.get(id))
                     .WithStatusCode(HttpStatusCode.OK);
            };

        }
    }
}