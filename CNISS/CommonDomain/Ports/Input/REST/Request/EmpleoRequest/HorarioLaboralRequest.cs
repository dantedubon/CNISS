using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest
{
    public class HorarioLaboralRequest:IValidPost
    {
        public HoraRequest horaEntrada { get; set; }
        public HoraRequest horaSalida { get; set; }
        public DiasLaborablesRequest diasLaborablesRequest { get; set; }
        public bool isValidPost()
        {
            return horaEntrada!=null &&horaEntrada.isValidPost() &&
                horaSalida!=null &&horaSalida.isValidPost()&&
                diasLaborablesRequest!=null&&diasLaborablesRequest.isValidPost();
        }
    }
}