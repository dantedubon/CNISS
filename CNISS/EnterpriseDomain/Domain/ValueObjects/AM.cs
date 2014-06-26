namespace CNISS.EnterpriseDomain.Domain.ValueObjects
{
    public class AM:IParteDia
    {
        public virtual string parte { get;  set; }

        public AM()
        {
            parte = "AM";
        }

    }
}