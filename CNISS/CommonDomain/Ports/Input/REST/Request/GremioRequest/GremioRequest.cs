namespace CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest
{
    public class GremioRequest:IValidPost,IValidPut
    {
        public RTNRequest rtnRequest { get; set; }
        public DireccionRequest direccionRequest { get; set; }
        public RepresentanteLegalRequest representanteLegalRequest { get; set; }
        public string nombre { get; set; }

        public AuditoriaRequest.AuditoriaRequest auditoriaRequest { get; set; }

        public bool isValidPost()
        {
            return representanteLegalRequest!=null && representanteLegalRequest.isValidPost()
               && rtnRequest!=null && rtnRequest.isValidPost()
               && direccionRequest!=null&& direccionRequest.isValidPost()
               && !string.IsNullOrEmpty(nombre);
        }


        public bool isValidPutRepresentante()
        {
            return isValidPut()&&representanteLegalRequest!=null &&representanteLegalRequest.isValidPost();
        }

        public bool isValidPut()
        {
            return rtnRequest!=null&& rtnRequest.isValidPost();
        }

        public bool isValidPutDireccion()
        {
            return isValidPut()&& direccionRequest!=null && direccionRequest.isValidPost();
        }
    }
}