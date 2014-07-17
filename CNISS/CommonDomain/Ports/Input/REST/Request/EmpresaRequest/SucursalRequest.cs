using System;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;

namespace CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest
{
    public class SucursalRequest:IValidPost
    {
        public Guid guid { get; set; }
        public string nombre { get; set; }
        public UserRequest.UserRequest userFirmaRequest { get; set; }
        public DireccionRequest direccionRequest { get; set; }
        public AuditoriaRequest.AuditoriaRequest auditoriaRequest { get; set; }
        public FirmaAutorizadaRequest firmaAutorizadaRequest { get; set; }
        

        public bool isValidPost()
        {
            return userFirmaRequest!=null && userFirmaRequest.isValidPost() 
                && direccionRequest!=null && direccionRequest.isValidPost()
                && nombre!=null &&!string.IsNullOrEmpty(nombre);
        }

        public bool isValidForPostBasicData()
        {
            return guid != Guid.Empty 
                && nombre != null && !string.IsNullOrEmpty(nombre);
        }
    }
}