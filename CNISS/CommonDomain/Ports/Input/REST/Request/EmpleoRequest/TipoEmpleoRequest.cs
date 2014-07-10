using System;

namespace CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest
{
    public class TipoEmpleoRequest:IValidPost,IValidPut
    {
        public Guid IdGuid { get; set; }
        public  string descripcion { get;  set; }
        public  AuditoriaRequest.AuditoriaRequest auditoriaRequest { get; set; }
        public bool isValidPost()
        {
            return !string.IsNullOrEmpty(descripcion) && auditoriaRequest!=null && auditoriaRequest.isValidPost();
        }

        public bool isValidPut()
        {
            return Guid.Empty != IdGuid && isValidPost();
        }
    }
}