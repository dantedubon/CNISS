using Nancy;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules
{
    public class HelloModule: NancyModule
    {
        public HelloModule()
        {
            Get["/"] = parameters => "Hello World";
            
        }

    }
}