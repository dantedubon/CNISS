using System;
using CNISS.CommonDomain.Domain;

namespace CNISS.CommonDomain.Application
{
    public class CommandDeleteIdentity<T> : ICommandDeleteIdentity<T>
    {
         protected IRepositoryCommands<T> _repository;
        protected Func<IUnitOfWork> _factory;

        protected CommandDeleteIdentity(IRepositoryCommands<T> repository, Func<IUnitOfWork> unitOfWork)
        {
            _repository = repository;
            _factory = unitOfWork;
        }

        public void execute(T identity)
        {
            
            var _uow = _factory();
            using (_uow)
            {
                _repository.delete(identity);
                _uow.commit();
            }
        }
    }
}