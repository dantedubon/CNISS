using System;

namespace CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest
{
    public class DireccionRequest:IValidRequest
    {
        public Guid IdGuid { get; set; }
        public MunicipioRequest municipioRequest { get; set; }
        public DepartamentoRequest departamentoRequest { get; set; }
        public string descripcion { get; set; }

        public DireccionRequest()
        {
            descripcion = "";
        }

        public bool isValidPost()
        {
            return municipioRequest!=null && municipioRequest.isValidPost() 
                && departamentoRequest!=null && departamentoRequest.isValidPost()
                && descripcion != null&& !string.IsNullOrEmpty(descripcion);
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