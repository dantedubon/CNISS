using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Ports.Output.Database;
using FizzWare.NBuilder;
using FluentAssertions;
using Machine.Specifications;
using NHibernate;

namespace CNISS_Integration_Test.Unit_Of_Work
{
    [Subject("Unit Of Work")]
    class when_UoW_Rollback_Should_Works
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
                uow.rollback();
                
            }
        };

         It should_be_null_result_roll = () =>
        {
            using (var uow = new NHibernateUnitOfWork(_sessionFactory.OpenSession()))
            {
                _resultRol = uow.Session.Get<Rol>(_expectedRol.idKey); 
            }



            _resultRol.Should().BeNull();
        };


    }
}
