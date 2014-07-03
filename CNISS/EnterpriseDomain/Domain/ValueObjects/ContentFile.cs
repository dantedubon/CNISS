using System;
using CNISS.CommonDomain.Domain;

namespace CNISS.EnterpriseDomain.Domain.ValueObjects
{
    public class ContentFile:ValueObject<Guid>, IEquatable<ContentFile>
    {
        public ContentFile(byte[] data)
        {
            Id = Guid.NewGuid();
            dataFile = data;
        }

        protected ContentFile()
        {
            Id = Guid.NewGuid();
        }

        public virtual byte[] dataFile { get; set; }

        public virtual bool Equals(ContentFile other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Id, other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ContentFile) obj);
        }

        public override int GetHashCode()
        {
            return (dataFile != null ? Id.GetHashCode() : 0);
        }
    }
}