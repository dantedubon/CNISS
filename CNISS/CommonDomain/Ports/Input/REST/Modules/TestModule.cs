using System.Linq;
using Nancy;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules
{
    public class TestModule:NancyModule
    {
        public TestModule()
        {
            Post["/test"] = parameters =>
            {
                
                var File2 = Request.Files.FirstOrDefault();
                return HttpStatusCode.OK;
            };
        }
    }
}