using System.Collections.Generic;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using CNISS.EnterpriseDomain.Ports.Output.Database;
using FizzWare.NBuilder;
using FluentAssertions;
using Machine.Specifications;
using NHibernate;

namespace CNISS_Integration_Test.Repositories.MunicipioRepository
{
    [Subject(typeof(DepartamentRepositoryReadOnly))]
    public class when_UserAskIfMunicipioBelongDepartamentoMunicipioNotExist_Should_ReturnFalse
    {
        static InMemoryDatabaseTest _databaseTest;
        static ISession _session;
        static DepartamentRepositoryReadOnly _repositoryRead;
        static Departamento _departamento;
        static Municipio _municipio;
        static Municipio _municipioNotFromDepartamento;
        static bool _response;


        Establish context = () =>
        {
            _databaseTest = new InMemoryDatabaseTest(typeof(Departamento).Assembly);
            _databaseTest.openSession();

            _session = _databaseTest.session;

            _repositoryRead = new DepartamentRepositoryReadOnly(_session);

            var idMunicipio = "municipio1";
            var idDepartamento = "departamento1";
            _municipioNotFromDepartamento = Builder<Municipio>.CreateNew().Build();
            _municipio = Builder<Municipio>.CreateNew()
                .With(x => x.Id = idMunicipio)
                .With(x => x.departamentoId = idDepartamento)
                .Build();
            _departamento = Builder<Departamento>.CreateNew()
                .With(x => x.Id = idDepartamento)
                .With(x => x.municipios = new List<Municipio>
                 {
                     _municipio
                 })
                .Build();


            using (var tx = _session.BeginTransaction())
            {
                _session.Save(_departamento);
                _session.Save(_municipio);
                tx.Commit();
            }
            _session.Clear();
        };

        Because of = () =>
        {
            _response = _repositoryRead.isValidMunicipio(_municipioNotFromDepartamento);
        };

         It should_return_true = () => _response.Should().BeFalse();
    }
}
