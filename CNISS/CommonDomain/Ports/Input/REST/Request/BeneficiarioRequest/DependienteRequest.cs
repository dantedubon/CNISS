using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.CommonDomain.Ports.Input.REST.Request.BeneficiarioRequest
{
    public class DependienteRequest:IValidPost
    {
        public NombreRequest nombreRequest { get; set; }
        public IdentidadRequest identidadRequest { get; set; }
        public ParentescoRequest parentescoRequest { get; set; }
        public Guid IdGuid { get; set; }

        public int edad { get; set; }


        public bool isValidPost()
        {
            return nombreRequest!=null&&nombreRequest.isValidPost()&&
                identidadRequest!=null&&identidadRequest.isValidPost()
                &&parentescoRequest!=null;
        }
    }
}