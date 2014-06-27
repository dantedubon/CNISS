using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest
{
    public class DiasLaborablesRequest:IValidPost
    {
        public  bool lunes { get; set; }
        public  bool martes { get; set; }
        public  bool miercoles { get; set; }
        public  bool jueves { get; set; }
        public  bool viernes { get; set; }
        public  bool sabado { get; set; }
        public  bool domingo { get; set; }
        public bool isValidPost()
        {
            return lunes || martes || miercoles || jueves || viernes || sabado || domingo;
        }
    }
}