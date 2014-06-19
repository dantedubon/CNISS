using CNISS.EnterpriseDomain.Domain.ValueObjects;
using CNISS.EnterpriseDomain.Ports.Output.Database;
using FizzWare.NBuilder;
using FluentAssertions;
using Machine.Specifications;
using NHibernate;

namespace CNISS_Integration_Test.Repositories.MunicipioRepository
{
    [Subject(typeof (MunicipioRepositoryReadOnly))]
    public class when_UserGetSpecificMunicipioById_Should_ReturnMunicipio
    {

        static InMemoryDatabaseTest _databaseTest;
        static ISession _session;
        static Municipio _expectedMunicipio;
        static Municipio _responseMunicipio;
        static MunicipioRepositoryReadOnly _repositoryRead;

         Establish context = () =>
        {
            _databaseTest = new InMemoryDatabaseTest(typeof(Municipio).Assembly);
            _databaseTest.openSession();

            _session = _databaseTest.session;

            _repositoryRead = new MunicipioRepositoryReadOnly(_session);

            var departamentoMunicipio = Builder<Departamento>.CreateNew().Build();
            _expectedMunicipio = Builder<Municipio>.CreateNew().With(x => x.departamentoId = departamentoMunicipio.Id)
               .Build();

            using (var tx = _session.BeginTransaction())
            {
                _session.Save(departamentoMunicipio);

                _session.Save(_expectedMunicipio);

                tx.Commit();
            }

            _session.Clear();
        };

         Because of = () =>
         {
             _responseMunicipio = _repositoryRead.get(_expectedMunicipio.departamentoId, _expectedMunicipio.Id);
         };

        It should_return_municipio = () => _responseMunicipio.ShouldBeEquivalentTo(_expectedMunicipio);
    }
}
