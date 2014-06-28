using System;
using CNISS.CommonDomain.Domain;

namespace CNISS.AutenticationDomain.Domain.ValueObjects
{
    public class Rol:ValueObject<Guid>
    {
        public Rol() 
        {
            
        }

        public Rol(string name, string description)
        {
            Id = new Guid();
            this.name = name;
            this.description = description;
        }
        public virtual string name { get; set; }
        public virtual string description { get; set; }
      

        
    }

    public class RolNull:Rol
    {
        public virtual string name { get { return string.Empty; } }
        public virtual string description { get { return string.Empty; } }
        public virtual Guid Id { get { return Guid.Empty; } }
    }
}