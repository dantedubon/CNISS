using System;
using System.Linq;

namespace CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest
{
    public class RTNRequest:IValidRequest
    {
        public string RTN { get; set; }
        public bool isValidPost()
        {
            return is14chars() && isNumeric();
        }

        private bool is14chars()
        {
            return 14 == RTN.Length;
        }

        private bool isNumeric()
        {
            return RTN.ToCharArray().All(char.IsNumber);
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