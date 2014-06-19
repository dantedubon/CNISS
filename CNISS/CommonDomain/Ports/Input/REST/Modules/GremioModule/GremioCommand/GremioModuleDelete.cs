using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.EnterpriseDomain.Application;
using CNISS.EnterpriseDomain.Domain;
using Nancy;
using Nancy.ModelBinding;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.GremioModule.GremioCommand
{
    public class GremioModuleDelete:NancyModule
    {
        public GremioModuleDelete(ICommandDeleteGremio command)
        {
            Delete["/enterprise/gremio/"] = paramters =>
            {
                var rtnToDelete = this.Bind<RTNRequest>();
                if (rtnToDelete.isValidPost())
                {
                    var rtn = convertToRTN(rtnToDelete);

                    if (command.isExecutable(rtn))
                    {
                        command.execute(rtn);
                        return new Response()
                            .WithStatusCode(HttpStatusCode.OK);
                    }
                }
                

                return new Response()
                    .WithStatusCode(HttpStatusCode.NotAcceptable);
            };
        }

        private RTN convertToRTN(RTNRequest request)
        {
            return new RTN(request.RTN);
        }
    }
}