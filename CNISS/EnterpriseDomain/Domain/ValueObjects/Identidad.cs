using System;
using System.Linq;
using CNISS.CommonDomain.Domain;

namespace CNISS.EnterpriseDomain.Domain.ValueObjects
{
    public class Identidad:ValueObject<string>
    {

        protected Identidad()
        {
            
        }

        public Identidad(string identidad)
        {
         


            if (!isNumber(identidad))
                throw new ArgumentException("Identidad no numerica");
            if(identidad.Length != 13)
                throw new ArgumentException("Identidad no tiene 13 caracteres");

            Id = identidad;
        }

        private bool isNumber(string identidadInvalida)
        {
            return identidadInvalida.ToCharArray().All(char.IsNumber);
        }
    }
}