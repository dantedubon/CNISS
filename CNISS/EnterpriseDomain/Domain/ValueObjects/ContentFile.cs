namespace CNISS.EnterpriseDomain.Domain.ValueObjects
{
    public class ContentFile
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