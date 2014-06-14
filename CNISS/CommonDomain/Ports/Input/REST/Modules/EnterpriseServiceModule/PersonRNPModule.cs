using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using Nancy;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.EnterpriseServiceModule
{
    public class PersonRNPModule:NancyModule
    {
        public PersonRNPModule(IPersonRNPRepositoryReadOnly repository)
        {
            Get["/enterprise/Person/id={id}"] = parameters =>
            {
                string id = parameters.id;
                if (!string.IsNullOrEmpty(id))
                {
                    var result = repository.get(id);
                    return Response.AsJson(result)
                        .WithStatusCode(HttpStatusCode.OK);
                }
                return new Response()
                    .WithStatusCode(HttpStatusCode.BadRequest);
            };
        }
    }
}