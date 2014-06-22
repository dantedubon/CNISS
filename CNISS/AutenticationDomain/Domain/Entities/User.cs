using System;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Domain;

namespace CNISS.AutenticationDomain.Domain.Entities
{
    public class User:Entity<string>, IEquatable<User>
    {
        public virtual String firstName { get; set; }
        public virtual String mail { get; set; }
        public virtual String secondName { get; set; }
        public virtual String password { get; set; }
        public virtual String userKey { get; set; }
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
}