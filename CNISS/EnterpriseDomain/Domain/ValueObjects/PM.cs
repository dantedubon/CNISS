namespace CNISS.EnterpriseDomain.Domain.ValueObjects
{
    public class PM:IParteDia
    {
        public virtual string parte { get;  set; }

        public PM()
        {
            parte = "PM";
        }
    }
}