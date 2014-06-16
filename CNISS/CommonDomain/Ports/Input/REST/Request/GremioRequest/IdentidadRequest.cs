using System;
using System.Linq;

namespace CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest
{
    public class IdentidadRequest:IValidPost
    {
        public string identidad { get; set; }
        public bool isValidPost()
        {
            return identidad != null &&  is13Chars() && isNumeric();
        }

        private bool isNumeric()
        {
            return  identidad.ToCharArray().All(char.IsNumber);
        }

        private bool is13Chars()
        {
            return 13 == identidad.Length;
        }

        
    }
}