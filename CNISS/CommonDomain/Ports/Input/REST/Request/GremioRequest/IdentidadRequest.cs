using System;
using System.Linq;

namespace CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest
{
    public class IdentidadRequest:IValidRequest
    {
        public string identidad { get; set; }
        public bool isValidPost()
        {
            return is13Chars()&&isNumeric();
        }

        private bool isNumeric()
        {
            return identidad.ToCharArray().All(char.IsNumber);
        }

        private bool is13Chars()
        {
            return 13 == identidad.Length;
        }

        public bool isValidPut()
        {
            throw new NotImplementedException();
        }

        public bool isValidDelete()
        {
            throw new NotImplementedException();
        }

        public bool isValidGet()
        {
            throw new NotImplementedException();
        }
    }
}