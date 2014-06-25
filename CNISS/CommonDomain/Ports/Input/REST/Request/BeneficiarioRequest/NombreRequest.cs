using System;
using System.Linq;

namespace CNISS.CommonDomain.Ports.Input.REST.Request.BeneficiarioRequest
{
    public class NombreRequest:IValidPost
    {
        public string nombres { get; set; }
   
        public string primerApellido { get; set; }
        public string segundoApellido { get; set; }

        public bool isValidPost()
        {
            return !string.IsNullOrEmpty(nombres)
                &&!string.IsNullOrEmpty(primerApellido)&&isAllChar();
        }

        private bool isAllChar()
        {
            return nombres.ToCharArray().All(x => Char.IsLetter(x) || Char.IsWhiteSpace(x)) 
            
                && primerApellido.ToCharArray().All(x => Char.IsLetter(x) || Char.IsWhiteSpace(x))
                && segundoApellido.ToCharArray().All(x => Char.IsLetter(x) || Char.IsWhiteSpace(x));
        }

    }
}