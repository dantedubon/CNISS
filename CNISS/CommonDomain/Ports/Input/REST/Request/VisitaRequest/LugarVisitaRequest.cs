using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.EmpresaRequest;

namespace CNISS.CommonDomain.Ports.Input.REST.Request.VisitaRequest
{
    public class LugarVisitaRequest:IValidPost
    {
        public Guid guid { get; set; }  
        public EmpresaRequest.EmpresaRequest empresaRequest { get; set; }
        public SucursalRequest sucursalRequest { get; set; }
        public AuditoriaRequest.AuditoriaRequest auditoriaRequest { get; set; }

        public bool isValidPost()
        {
            return empresaRequest!=null&&empresaRequest.isValidPostForBasicData()
                && sucursalRequest!=null&& sucursalRequest.isValidForPostBasicData()
                && auditoriaRequest!=null&&auditoriaRequest.isValidPost();
        }
    }
}