using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using CNISS.EnterpriseDomain.Ports.Output.Database;
using FizzWare.NBuilder;
using FluentAssertions;
using Machine.Specifications;
using NHibernate;

namespace CNISS_Integration_Test.Repositories.GremioRepository.ReadOnly
{
    [Subject(typeof(GremioRepositoryReadOnly))]
    public class when_UserAskGremioNonExisting_Should_ReturnFalse
    {
        static InMemoryDatabaseTest _databaseTest;
        static ISession _session;
        static GremioRepositoryReadOnly repository;
        static Gremio _expectedGremio;
        static bool _response;


        Establish context = () =>
        {
            _databaseTest = new InMemoryDatabaseTest(typeof(Departamento).Assembly);
            _databaseTest.openSession();

            _session = _databaseTest.session;

            repository = new GremioRepositoryReadOnly(_session);

            var representanteGremio = getRepresentante();

            var idMunicipio = "municipio1";
            var idDepartamento = "departamento1";
            var municipio = getMunicipio(idMunicipio, idDepartamento);
            var departamento = getDepartamento(idDepartamento, municipio);

            var direccionGremio = new Direccion(departamento, municipio, "Barrio Abajo");

            var RTNGremio = new RTN("08011985123960");
            _expectedGremio = new Gremio(RTNGremio, representanteGremio, direccionGremio, "Nombre de Gremio");




        };

        private static Municipio getMunicipio(string idMunicipio, string idDepartamento)
        {
            var municipio = Builder<Municipio>.CreateNew()
                .With(x => x.Id = idMunicipio)
                .With(x => x.departamentoId = idDepartamento)
                .Build();
            return municipio;
        }

        private static Departamento getDepartamento(string idDepartamento, Municipio municipio)
        {
            var departamento = Builder<Departamento>.CreateNew()
                .With(x => x.Id = idDepartamento)
                .With(x => x.municipios = new List<Municipio>
                {
                    municipio
                })
                .Build();
            return departamento;
        }

        private static RepresentanteLegal getRepresentante()
        {
            var identidadRepresentante = new Identidad("0801198512396");
            return new RepresentanteLegal(identidadRepresentante, "Juan Perez");
        }

        Because of = () => { _response = repository.exists(_expectedGremio.Id); };

        It should_return_false = () => _response.Should().BeFalse();
    }
}
