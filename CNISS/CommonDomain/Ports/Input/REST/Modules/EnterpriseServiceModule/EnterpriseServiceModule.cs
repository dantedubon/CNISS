using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.EnterpriseDomain.Application;
using Nancy;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.EnterpriseServiceModule
{
    public class EnterpriseServiceModule:NancyModule
    {
        public EnterpriseServiceModule(IServiceValidatorRTN _serviceValidatorRtn)
        {
            Get["/enterprise/isValidRTN={rtn}"] = parameters =>
            {
                string RTN = parameters.RTN;
                var rtn = new RTNRequest(){RTN = RTN};
                var response= false;
                if (rtn.isValidPost())
                {
                     response = _serviceValidatorRtn.isValidRTN(rtn.RTN);
                   
                }
                

                return Response.AsJson(response)
                       .WithStatusCode(HttpStatusCode.OK);

            };

           
        }
    }
}