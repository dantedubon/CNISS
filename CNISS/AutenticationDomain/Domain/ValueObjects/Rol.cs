using System;
using CNISS.CommonDomain.Domain;

namespace CNISS.AutenticationDomain.Domain.ValueObjects
{
    public class Rol:ValueObject<Guid>
    {
        public Rol()
        {
            Id = Guid.NewGuid();
        }

        public Rol(string name, string description):this()
        {
           
            this.Name = name;
            this.Description = description;
        }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual int Nivel { get; set; }
        public virtual Auditoria Auditoria { get; set; }

        
    }

    public class RolNull:Rol
    {
        public virtual string Name { get { return string.Empty; } }
        public virtual string Description { get { return string.Empty; } }
        public virtual Guid Id { get { return Guid.Empty; } }
    }
}