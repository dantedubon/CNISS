using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest
{
    public class FirmaAutorizadaRequest
    {
        public UserRequest.UserRequest userRequest { get; set; }
        public Guid IdGuid { get; set; }
        public DateTime fechaCreacion { get; set; }

        public bool isValidPostForFichaSupervision()
        {
            return Guid.Empty != IdGuid && userRequest.isValidForAuthenticate();
        }
    }
}