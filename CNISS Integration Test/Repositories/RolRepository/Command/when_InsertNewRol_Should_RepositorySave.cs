using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.AutenticationDomain.Ports.Output.Database;
using CNISS.CommonDomain.Ports.Output.Database;
using CNISS_Integration_Test.Unit_Of_Work;
using FizzWare.NBuilder;
using FluentAssertions;
using Machine.Specifications;
using NHibernate;

namespace CNISS_Integration_Test.Repositories.RolRepository.Command
{
    [Subject("RolRepositoryCommand")]
    public class when_InsertNewRol_Should_RepositorySave
    {
         static IRolRepositoryCommands _repository;
         static InFileDataBaseTest _dataBaseTest;

         static ISessionFactory _sessionFactory;

         static Rol _expectedRol;
         static Rol _resultRol;

         Establish context = () =>
         {
             _dataBaseTest = new InFileDataBaseTest();
             _sessionFactory = _dataBaseTest.sessionFactory;
             _expectedRol = Builder<Rol>.CreateNew().Build();



         };

         Because of = () =>
         {
             using (var uow = new NHibernateUnitOfWork(_sessionFactory))
             {
                 _repository = new RolRepositoryCommands(uow.Session);
                 _repository.save(_expectedRol);
                  uow.commit();

             }
         };

        It should_new_rol_be_saved = () =>
        {
            using (var uow = new NHibernateUnitOfWork(_sessionFactory))
            {
                _resultRol = uow.Session.Get<Rol>(_expectedRol.idKey);
            }

            _resultRol.ShouldBeEquivalentTo(_expectedRol);
        };
    }
}
