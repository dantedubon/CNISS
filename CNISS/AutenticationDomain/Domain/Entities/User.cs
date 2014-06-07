using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Domain;

namespace CNISS.AutenticationDomain.Domain.Entities
{
    public class User:Entity<string>
    {
        public virtual String firstName { get; set; }
        public virtual String mail { get; set; }
        public virtual String secondName { get; set; }
        public virtual String password { get; set; }
        public virtual Rol userRol { get; set; }

        protected User()
        {
            
        }

        public User(String id, String firstName, String secondName, String password, String mail, Rol userRol)
        {
            Id = id;
            this.firstName = firstName;
            this.secondName = secondName;
            this.password = password;
            this.mail = mail;
            this.userRol = userRol;

        }
    }
}