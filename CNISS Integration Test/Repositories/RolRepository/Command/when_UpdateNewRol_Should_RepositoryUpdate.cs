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
    public class when_UpdateNewRol_Should_RepositoryUpdate
    {

        static IRolRepositoryCommands _repository;
        static InFileDataBaseTest _dataBaseTest;

        static ISessionFactory _sessionFactory;

        static Rol _expectedRol;
        static Rol _resultRol;

        static string _newRolName;

        Establish context = () =>
        {
            _dataBaseTest = new InFileDataBaseTest();
            _sessionFactory = _dataBaseTest.sessionFactory;
            _expectedRol = Builder<Rol>.CreateNew().Build();
            _newRolName = "Nuevo Nombre";

            using (var uow = new NHibernateUnitOfWork(_sessionFactory.OpenSession()))
            {
                _repository = new RolRepositoryCommands(uow.Session);
                _repository.save(_expectedRol);
                uow.commit();

            }
            
        };

         Because of = () =>
         {
             _expectedRol.name = _newRolName;
             using (var uow = new NHibernateUnitOfWork(_sessionFactory.OpenSession()))
             {
                 _repository = new RolRepositoryCommands(uow.Session);
                 _repository.update(_expectedRol);
                 uow.commit();

             }
             
         };

        It should_update_existing_rol = () =>
        {

            using (var uow = new NHibernateUnitOfWork(_sessionFactory.OpenSession()))
            {
                _resultRol = uow.Session.Get<Rol>(_expectedRol.Id);

            }

            _resultRol.name.Should().BeEquivalentTo(_newRolName);
        };
    }
}
