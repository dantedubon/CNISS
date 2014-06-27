using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest
{
    public class TipoEmpleoRequest
    {
        public Guid IdGuid { get; set; }
        public String descripcion { get; set; }
    }
}