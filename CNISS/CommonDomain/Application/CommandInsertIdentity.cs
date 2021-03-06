﻿using System;
using CNISS.CommonDomain.Domain;

namespace CNISS.CommonDomain.Application
{
    public abstract class CommandInsertIdentity<T>:ICommandInsertIdentity<T>
    {
        protected IRepositoryCommands<T> _repository;
        protected Func<IUnitOfWork> _factory;

        protected CommandInsertIdentity(IRepositoryCommands<T> repository, Func<IUnitOfWork> unitOfWork)
        {
            _repository = repository;
            _factory = unitOfWork;
        }

        public virtual void  execute(T identity)
        {
            
            var _uow = _factory();
            using (_uow)
            {
                _repository.save(identity);
                _uow.commit();
            }
        }

        public virtual bool isExecutable(T identity)
        {
            throw new NotImplementedException();
        }

        public string message { get; set; }
    }
}