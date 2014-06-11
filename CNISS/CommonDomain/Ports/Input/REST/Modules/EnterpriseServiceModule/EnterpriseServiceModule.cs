using CNISS.EnterpriseDomain.Application;
using Nancy;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.EnterpriseServiceModule
{
    public class EnterpriseServiceModule:NancyModule
    {
        public EnterpriseServiceModule(IServiceValidatorRTN _serviceValidatorRtn)
        {
            Get["/enterprise/isValidRTN={RTN}"] = parameters =>
            {
                string RTN = parameters.RTN;
                bool response = _serviceValidatorRtn.isValidRTN(RTN);
                return Response.AsJson(response)
                    .WithStatusCode(HttpStatusCode.OK);

            };
        }
    }
}