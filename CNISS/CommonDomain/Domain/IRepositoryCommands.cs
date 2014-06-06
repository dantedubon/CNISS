namespace CNISS.CommonDomain.Domain
{
    public interface IRepositoryCommands <in T>
    {
        void save(T entity);
        void delete(T entity);
        void update(T entity);
       
    }
}