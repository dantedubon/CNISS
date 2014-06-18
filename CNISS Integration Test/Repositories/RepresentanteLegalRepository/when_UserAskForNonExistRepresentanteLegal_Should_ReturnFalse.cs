using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using CNISS.EnterpriseDomain.Ports.Output.Database;
using FluentAssertions;
using Machine.Specifications;
using NHibernate;

namespace CNISS_Integration_Test.Repositories.RepresentanteLegalRepository
{
    [Subject(typeof(RepresentanteLegalRepositoryReadOnly))]
    public class when_UserAskForNonExistRepresentanteLegal_Should_ReturnFalse
    {
        static InMemoryDatabaseTest _databaseTest;
        static ISession _session;
        static RepresentanteLegalRepositoryReadOnly repository;
        static RepresentanteLegal _representanteExpected;
        static bool _response;

        Establish context = () =>
        {

            _databaseTest = new InMemoryDatabaseTest(typeof(Departamento).Assembly);
            _databaseTest.openSession();

            _session = _databaseTest.session;

            repository = new RepresentanteLegalRepositoryReadOnly(_session);

            var Identidad = new Identidad("0801198512396");
            _representanteExpected = new RepresentanteLegal(Identidad, "Juan Perez");

           

        };

        private Because of = () => { _response = repository.exists(_representanteExpected.Id); };

        It should_return_false = () => _response.Should().BeFalse();
    }
}