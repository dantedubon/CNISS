using System.Linq;

namespace CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest
{
    public class RTNRequest:IValidPost
    {
        public string RTN { get; set; }
        public bool isValidPost()
        {
            return RTN != null && is14chars() && isNumeric();
        }

        private bool is14chars()
        {
            return 14 == RTN.Length;
        }

        private bool isNumeric()
        {
            return RTN.ToCharArray().All(char.IsNumber);
        }
   
    }
}