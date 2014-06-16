using System;

namespace CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest
{
    public class RepresentanteLegalRequest:IValidRequest
    {
        public IdentidadRequest identidadRequest { get; set; }
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