using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using CNISS.EnterpriseDomain.Ports.Output;
using CNISS.EnterpriseDomain.Ports.Output.Database;
using CNISS_Integration_Test.Unit_Of_Work;
using FizzWare.NBuilder;
using FluentAssertions;
using Machine.Specifications;
using NHibernate;

namespace CNISS_Integration_Test.Repositories.GremioRepository.Command
{
    [Subject(typeof (GremioRepositoryCommands))]
    public class when_UserSaveGremioWithExistingRepresentante_Should_SaveGremio
    {
        static InFileDataBaseTest _dataBaseTest;
        static ISessionFactory _sessionFactory;
        static ISession _session;
        static GremioRepositoryCommands _repository;
        static Gremio _expectedGremio;
        static Gremio _responseGremio;


        private Establish context = () =>
        {
            _dataBaseTest = new InFileDataBaseTest();
            _sessionFactory = _dataBaseTest.sessionFactory;
            var municipio = getMunicipio("01", "01");
            var departamento = getDepartamento("01", municipio);
            var representante = getRepresentante();

            saveDepartamentoMunicipio(departamento, municipio);
            saveRepresentante(representante);

            var direccion = new Direccion(departamento, municipio, "Barrio abajo");

            var rtn = new RTN("08011985123960");
            _expectedGremio = new Gremio(rtn, representante, direccion, "Camara");




        };



        #region Helpers Methods

        private static void saveRepresentante(RepresentanteLegal representante)
        {
            _session = _sessionFactory.OpenSession();
            using (var tx = _session.BeginTransaction())
            {
                _session.Save(representante);
               
                tx.Commit();
            }
            _session.Close();
        }

        private static void saveDepartamentoMunicipio(Departamento departamento, Municipio municipio)
        {
            _session = _sessionFactory.OpenSession();
            using (var tx = _session.BeginTransaction())
            {
                _session.Save(departamento);
                _session.Save(municipio);
                tx.Commit();
            }
            _session.Close();
        }

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

        #endregion



        private Because of = () =>
        {
            using (var uow = new NHibernateUnitOfWork(_sessionFactory.OpenSession()))
            {
                var representanteRepository = new RepresentanteLegalRepositoryReadOnly(uow.Session);
                _repository = new GremioRepositoryCommands(uow.Session, representanteRepository);
                _repository.save(_expectedGremio);
                uow.commit();

            }
        };


        It should_save_gremio = () =>
        {
            using (var uow = new NHibernateUnitOfWork(_sessionFactory.OpenSession()))
            {
                _responseGremio = uow.Session.Get<Gremio>(_expectedGremio.Id);
                _responseGremio.ShouldBeEquivalentTo(_expectedGremio);
            }
        };
    }
}
