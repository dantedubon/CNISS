using System.Linq;

namespace CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest
{
    public class MunicipioRequest:IValidPost
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

       
    }
}