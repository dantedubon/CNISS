using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest
{
    public class AuditoriaRequest:IValidPost
    {
        public string usuarioCreo { get; set; }
        public DateTime fechaCreo { get; set; }
        public string usuarioModifico { get; set; }
        public DateTime fechaModifico { get; set; }

        public bool isValidPost()
        {
            return !string.IsNullOrEmpty(usuarioCreo)&&fechaCreo>new DateTime(2014,1,1)
                && !string.IsNullOrEmpty(usuarioModifico)
                && fechaModifico > new DateTime(2014,1,1);
        }
    }
}