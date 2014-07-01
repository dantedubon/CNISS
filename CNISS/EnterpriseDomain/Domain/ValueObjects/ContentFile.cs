using System;
using CNISS.CommonDomain.Domain;

namespace CNISS.EnterpriseDomain.Domain.ValueObjects
{
    public class ContentFile:ValueObject<Guid>
    {
        public ContentFile(byte[] data)
        {
            dataFile = data;
        }

        protected ContentFile()
        {
            
        }

        public virtual byte[] dataFile { get; set; }
      
    }
}