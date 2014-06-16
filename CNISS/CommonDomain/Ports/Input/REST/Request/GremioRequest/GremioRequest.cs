using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest
{
    public class GremioRequest:IValidRequest
    {
        public RTNRequest rtnRequest { get; set; }
        public DireccionRequest direccionRequest { get; set; }
        public RepresentanteLegalRequest representanteLegalRequest { get; set; }
        public string nombre { get; set; }
        public bool isValidPost()
        {
            throw new NotImplementedException();
        }

        public bool isValidPut()
        {
            throw new NotImplementedException();
        }

        public bool isValidDelete()
        {
            throw new NotImplementedException();
        }

        public bool isValidGet()
        {
            throw new NotImplementedException();
        }
    }
}