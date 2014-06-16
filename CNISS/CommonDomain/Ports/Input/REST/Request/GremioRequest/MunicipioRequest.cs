using System;
using System.Linq;

namespace CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest
{
    public class MunicipioRequest:IValidRequest
    {
        public string idMunicipio { get; set; }
        public string idDepartamento { get; set; }
        public string nombre { get; set; }

        public MunicipioRequest()
        {
            idMunicipio = "";
            idDepartamento = "";
            nombre = "";
        }

        public bool isValidPost()
        {
            return is2DigitId() && isNumericId()&& !string.IsNullOrEmpty(nombre);
        }


        private bool is2DigitId()
        {
            return idMunicipio.Length == 2 && idDepartamento.Length == 2;
        }

        private bool isNumericId()
        {
            return idMunicipio.ToCharArray().All(char.IsNumber) && idDepartamento.ToCharArray().All(char.IsNumber);
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