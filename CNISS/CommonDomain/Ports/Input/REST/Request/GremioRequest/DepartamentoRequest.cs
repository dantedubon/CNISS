using System;
using System.Linq;

namespace CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest
{
    public class DepartamentoRequest:IValidPost
    {
        public string idDepartamento { get; set; }
        public string nombre { get; set; }

        public DepartamentoRequest()
        {
            idDepartamento = "";
            nombre = ""
                ;
        }

        public bool isValidPost()
        {
            return is2Digit()&&isNumeric()&&!string.IsNullOrEmpty(nombre);
        }

        private bool isNumeric()
        {
            return idDepartamento.ToCharArray().All(char.IsNumber);
        }

        private bool is2Digit()
        {
            return 2 == idDepartamento.Length;
        }
      
        
    }
}