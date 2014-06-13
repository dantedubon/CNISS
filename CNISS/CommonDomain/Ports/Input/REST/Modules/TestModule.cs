using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.ModelBinding;

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