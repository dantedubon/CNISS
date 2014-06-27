using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNISS.CommonDomain.Ports.Input.REST.Request.EmpleoRequest
{
    public class HoraRequest:IValidPost
    {
        public int hora { get; set; }
        public int minutos { get; set; }
        public string parte { get; set; }
        public bool isValidPost()
        {
           return validHora() && validMinutos() && validParte();
        }

        private bool validParte()
        {
            return parte == "AM" || parte == "PM";
        }
        private bool validMinutos()
        {
            return minutos >= 0 && minutos <= 59;
        }

        private bool validHora()
        {
            return hora >= 1 && hora <= 12;
        }
    }
}