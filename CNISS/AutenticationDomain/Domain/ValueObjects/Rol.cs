using System;
using CNISS.CommonDomain.Domain;

namespace CNISS.AutenticationDomain.Domain.ValueObjects
{
    public class Rol:ValueObject<Guid>
    {
        public Rol() 
        {
            idKey = new Guid();
        }

        public Rol(string name, string description):this()
        {
           
            this.name = name;
            this.description = description;
        }
        public virtual string name { get; set; }
        public virtual string description { get; set; }
      

        
    }
}