using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Ports.Output.Database;
using FizzWare.NBuilder;
using FluentAssertions;
using Machine.Specifications;
using NHibernate;

namespace CNISS_Integration_Test.Unit_Of_Work
{
    [Subject("Unit of Work")]
    public class when_UoW_WithoutCommit_Should_Rollback
    {
        static InFileDataBaseTest _databaseTest;
        static ISessionFactory _sessionFactory;
        static Rol _expectedRol;
        static Rol _resultRol;

        Establish context = () =>
        {
            _databaseTest = new InFileDataBaseTest();
            _sessionFactory = _databaseTest.sessionFactory;

            _expectedRol = Builder<Rol>.CreateNew().Build();


        };

        Because of = () =>
        {
            using (var uow = new NHibernateUnitOfWork(_sessionFactory.OpenSession()))
            {
                uow.Session.Save(_expectedRol);
                
            }
        };

        It should_RollBack= () =>
        {
            using (var uow = new NHibernateUnitOfWork(_sessionFactory.OpenSession()))
            {
                _resultRol = uow.Session.Get<Rol>(_expectedRol.Id);
            }



            _resultRol.Should().BeNull();
        };
    }
}