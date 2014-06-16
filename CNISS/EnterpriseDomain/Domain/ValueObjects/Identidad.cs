using System;
using System.Linq;
using CNISS.CommonDomain.Domain;

namespace CNISS.EnterpriseDomain.Domain.ValueObjects
{
    public class Identidad:IDomainObjectNotIdentified, IEquatable<Identidad>
    {
        public virtual string identidad { get; set; }

        protected Identidad()
        {
            
        }

        public Identidad(string identidad)
        {
         


            if (!isNumber(identidad))
                throw new ArgumentException("Identidad no numerica");
            if(identidad.Length != 13)
                throw new ArgumentException("Identidad no tiene 13 caracteres");
            this.identidad = identidad;

        }

        public bool Equals(Identidad other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(identidad, other.identidad);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Identidad) obj);
        }

        public override int GetHashCode()
        {
            return (identidad != null ? identidad.GetHashCode() : 0);
        }

        private bool isNumber(string identidadInvalida)
        {
            return identidadInvalida.ToCharArray().All(char.IsNumber);
        }

       
    }
}