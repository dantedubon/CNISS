using System.Collections.Generic;
using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.AutenticationDomain.Ports.Output.Database;
using FizzWare.NBuilder;
using FluentAssertions;
using Machine.Specifications;
using NHibernate;

namespace CNISS_Integration_Test.Repositories.RolRepository.ReadOnly
{
    [Subject(typeof(RolRepositoryReadOnly))]
    class when_ListAllRol_Should_ReturnFromDB
    {
        static IRolRepositoryReadOnly _repository;
        static IEnumerable<Rol> _expectedRols;
        static IEnumerable<Rol> _resultRols;
        static InMemoryDatabaseTest _databaseTest;
        static ISession _session;
        
        Establish context = () =>
        {
           _databaseTest = new InMemoryDatabaseTest(typeof(Rol).Assembly);
            _databaseTest.openSession();

            _session = _databaseTest.session;

            _repository = new RolRepositoryReadOnly(_session);
            _expectedRols = Builder<Rol>.CreateListOfSize(10).Build();

            using (var tx = _session.BeginTransaction())
            {
                foreach (var expectedRol in _expectedRols)
                {
                    _session.Save(expectedRol);
                }

                tx.Commit();
            }

            _session.Clear();



        };

         Because of = () =>   _resultRols = _repository.getAll();
         


         It should_get_all_rols = () => _expectedRols.ShouldBeEquivalentTo(_resultRols);





    }
}
