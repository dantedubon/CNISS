using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest
{
    public class AuditoriaRequest
    {
        public string usuarioCreo { get; set; }
        public DateTime fechaCreo { get; set; }
        public string usuarioModifico { get; set; }
        public DateTime fechaModifico { get; set; }
    }
}