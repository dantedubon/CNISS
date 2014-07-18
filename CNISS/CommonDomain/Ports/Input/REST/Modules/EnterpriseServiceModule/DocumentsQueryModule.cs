using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Nancy;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.EnterpriseServiceModule
{
    public class DocumentsQueryModule:NancyModule
    {
        public DocumentsQueryModule(IContentFileRepositoryReadOnly repositoryReadOnly)
        {
            Get["/enterprise/Documents/{id:guid}"] = parameters =>
            {
                Guid id = parameters.id;

                var fileData = repositoryReadOnly.get(id);
                if (fileData == null)
                    return new Response()
                        .WithStatusCode(HttpStatusCode.NotFound);
               

                return Response.FromStream(() => new MemoryStream(fileData.dataFile),"application/pdf")

                    .WithStatusCode(HttpStatusCode.OK);

            };

            Get["/enterprise/Images/{id:guid}"] = parameters =>
            {
                Guid id = parameters.id;

                var fileData = repositoryReadOnly.get(id);
                if (fileData == null)
                    return new Response()
                        .WithStatusCode(HttpStatusCode.NotFound);


                return Response.FromStream(() => new MemoryStream(fileData.dataFile), "image/jpeg")

                    .WithStatusCode(HttpStatusCode.OK);

            };
        }
    }
}