using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Services;
using CNISS.CommonDomain.Ports.Input.REST.Request;
using CNISS.EnterpriseDomain.Domain.Repositories;
using Nancy;
using Nancy.Authentication.Token;
using Nancy.ModelBinding;
using Nancy.Security;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.MotivoDespidoModule.Query
{
    public class MotivoDespidoModuleQueryMovil:NancyModule
    {
        public MotivoDespidoModuleQueryMovil(ISerializeJsonRequest serializerJson,Func<string,IEncrytRequestProvider> encryptRequestProvider, 
            ITokenizer tokenizer,IMotivoDespidoRepositoryReadOnly readOnlyRepository)
        {
            Post["/movil/motivosDespido"] = parameters =>
            {
               


                var movilRequest = this.Bind<MovilRequest>();
                var userId = tokenizer.Detokenize(movilRequest.token, Context);
                if (userId == null)
                {
                    return new Response().WithStatusCode(HttpStatusCode.Unauthorized);
                }
                var motivos = readOnlyRepository.getAll();

                var motivosString = serializerJson.toJson(motivos);
                var respuestaEncryptada = encryptRequestProvider(movilRequest.token).encryptString(motivosString);

                return respuestaEncryptada;
            };
        }
    }
}