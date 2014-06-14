using System;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Domain.ValueObjects;

namespace CNISS.EnterpriseDomain.Domain.Entities
{
    public class RepresentanteLegal:Entity<Identidad>
    {
        public virtual string _nombre { get; protected set; }
        public RepresentanteLegal(Identidad identidad,  string nombre)
        {
           
            if (identidad == null) throw new ArgumentException("Identidad no puede ser nula");
            if (string.IsNullOrEmpty(nombre))
                throw new ArgumentException("Nombre de Representante no puede ser nulo");
            Id = identidad;
            _nombre = nombre;
        }
    }
}