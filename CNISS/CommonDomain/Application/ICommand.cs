namespace CNISS.CommonDomain.Application
{
   public  interface ICommand<in T>
    {
        void execute(T identity);
    }
}
