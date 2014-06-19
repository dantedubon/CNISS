using System;

namespace CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest
{
    public class RepresentanteLegalRequest:IValidPost
    {
        public IdentidadRequest identidadRequest { get; set; }
        public string nombre { get; set; }
        public bool isValidPost()
        {
            return identidadRequest!=null && identidadRequest.isValidPost()
                && nombre != null && !string.IsNullOrEmpty(nombre);
        }

     
    }
}