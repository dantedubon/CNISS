namespace CNISS.CommonDomain.Domain
{
    public abstract class ValueObject<TKey>:IDomainObjectIdentified<TKey>
    {
        public virtual TKey Id { get;  set; }

        protected ValueObject()
        {
           
        }

        protected ValueObject(TKey id)
        {
            Id = id;
        }
      
    }


   
}