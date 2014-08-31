using System;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Domain;

namespace CNISS.AutenticationDomain.Domain.Entities
{
    public class User:Entity<string>, IEquatable<User>
    {
        public virtual String FirstName { get; set; }
        public virtual String Mail { get; set; }
        public virtual String SecondName { get; set; }
        public virtual String Password { get; set; }
        public virtual String UserKey { get; set; }
        public virtual Rol UserRol { get; set; }
        public virtual Auditoria Auditoria { get; set; }
       

        protected User()
        {
            
        }

        public User(String id, String firstName, String secondName, String password, String mail, Rol userRol)
        {
            Id = id;
            this.FirstName = firstName;
            this.SecondName = secondName;
            this.Password = password;
            this.Mail = mail;
            this.UserRol = userRol;

        }

        public virtual bool Equals(User other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Id, other.Id);
        }

        public override  bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((User) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Id != null ? Id.GetHashCode() : 0);
               
                return hashCode;
            }
        }
    }

    public class UserNull:User
    {
        public virtual String firstName { get { return string.Empty; }  }
        public virtual  String mail { get { return string.Empty; } }
        public virtual  String secondName { get { return string.Empty; } }
        public virtual String password { get { return string.Empty; }  }
        public virtual  String userKey { get { return string.Empty; }  }
        public virtual Rol userRol { get{return new RolNull();} }
       
    }
}