using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Application.Comandos;
using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Application;
using CNISS.CommonDomain.Domain;
using FizzWare.NBuilder;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace CNISS_Tests.Rol_Test.Commands
{
    [Subject(typeof (ICommandUpdateIdentity<>))]
    public class when_CommandUpdateIdentity_Shuld_UpdateIdentity
    {
         static ICommandUpdateIdentity<Rol> _command;
         static Func<IUnitOfWork> _uow;
         static IRolRepositoryCommands _repository;
         static Rol _rolUpdated;
             
         Establish context = () =>
         {
             _rolUpdated = Builder<Rol>.CreateNew().Build();
             _uow = Mock.Of<Func<IUnitOfWork>>();
             Mock.Get(_uow).Setup(x => x()).Returns(new DummyUnitOfWork());
             _repository = Mock.Of<IRolRepositoryCommands>();
             _command = new CommandUpdateRol(_repository,_uow);
         };

        Because of = () => _command.execute(_rolUpdated);

        It should_update_identity= () => Mock.Get(_repository).Verify( x=> x.update( Moq.It.Is<Rol>( z => z.idKey == _rolUpdated.idKey)));
    }

    internal class DummyUnitOfWork:IUnitOfWork
    {
        public void Dispose()
        {
            
        }

        public void commit()
        {
           
        }

        public void rollback()
        {
           
        }
    }
}
