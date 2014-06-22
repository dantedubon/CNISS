using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using CNISS.EnterpriseDomain.Ports.Output.Database;
using CNISS_Integration_Test.Unit_Of_Work;
using FluentAssertions;
using Machine.Specifications;
using NHibernate;

namespace CNISS_Integration_Test.Repositories.ActividadEconomicaRepository
{
    [Subject(typeof (ActividadEconomicaRepositoryReadOnly))]
    public class when_UserAskIfExistsAllActividadesExisting_Should_ReturnFalse
    {
        static InFileDataBaseTest _dataBaseTest;
        static ISessionFactory _sessionFactory;
        private static bool _response;
        private static ActividadEconomicaRepositoryReadOnly _repositoryRead;
        private static ISession _session;
        private static IEnumerable<ActividadEconomica> _listaDatosExistentes;
        private static IEnumerable<ActividadEconomica> _listaDatosABuscar;


        private Establish context = () =>
        {
            _dataBaseTest = new InFileDataBaseTest();
            _sessionFactory = _dataBaseTest.sessionFactory;
            var actividad1 = new ActividadEconomica("Maquila");
            var actividad2 = new ActividadEconomica("Textiles");
            var actividad3 = new ActividadEconomica("Restaurante");
            var actividad4 = new ActividadEconomica("Farmacia");

            _listaDatosExistentes = new List<ActividadEconomica>()
            {
                actividad1,
                actividad2,
                actividad3
            };


            _listaDatosABuscar = new List<ActividadEconomica>()
            {
                 actividad1,
                actividad2,
                actividad4
            };

            saveListaActividades(_listaDatosExistentes);




        };

        private Because of = () =>
        {
            using (var _uow = new NHibernateUnitOfWork(_sessionFactory.OpenSession()))
            {
                _repositoryRead = new ActividadEconomicaRepositoryReadOnly(_uow.Session);
                _response = _repositoryRead.existsAll(_listaDatosABuscar);
            }
        };

        It should_return_false = () => { _response.Should().BeFalse(); };

        private static void saveListaActividades(IEnumerable<ActividadEconomica> actividadEconomicas)
        {
            using (var _uow = new NHibernateUnitOfWork(_sessionFactory.OpenSession()))
            {
                var session = _uow.Session;
                actividadEconomicas.ToList().ForEach(x => session.Save(x));
                _uow.commit();
            }

        }
    }
}
