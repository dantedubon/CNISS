using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Services;
using CNISS.EnterpriseDomain.Domain.Repositories;
using Nancy;
using Nancy.Authentication.Token;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.EnterpriseServiceModule
{
    public class PersonRNPMovilModule:NancyModule
    {
        public PersonRNPMovilModule(ISerializeJsonRequest serializerJson,Func<string,IEncrytRequestProvider> encryptRequestProvider, 
            ITokenizer tokenizer,IPersonRNPRepositoryReadOnly repository)
        {
            Get["/movil/enterprise/Person/id={id}"] = parameters =>
            {
                var query = Request.Query.ToDictionary();
                string token = query["token"];
                try
                {
                    var userId = tokenizer.Detokenize(token, Context);
                    if (userId == null)
                    {
                        return new Response().WithStatusCode(HttpStatusCode.Unauthorized);
                    }
                }
                catch (Exception e)
                {
                    return new Response().WithStatusCode(HttpStatusCode.Unauthorized);
                }

                string id = parameters.id;
                if (!string.IsNullOrEmpty(id))
                {
                    var result = repository.get(id);
                    var personaString = serializerJson.toJson(result);
                    var respestaEncriptada = encryptRequestProvider(token).encryptString(personaString);

                    return respestaEncriptada;
                }
                return new Response()
                    .WithStatusCode(HttpStatusCode.BadRequest);
            };
        }
    }
}