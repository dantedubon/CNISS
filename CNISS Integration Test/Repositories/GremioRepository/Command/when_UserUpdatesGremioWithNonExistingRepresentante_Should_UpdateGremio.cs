﻿using System.Collections.Generic;
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
    public class when_UserUpdatesGremioWithNonExistingRepresentante_Should_UpdateGremio
    {
        private static InFileDataBaseTest _dataBaseTest;
        private static ISessionFactory _sessionFactory;
        private static ISession _session;
        private static GremioRepositoryCommands _repository;
        private static Gremio _originalGremio;
        private static Gremio _updatedGremio;
        private static RepresentanteLegal _representanteLegalNuevo;
        private Establish context = () =>
        {
            _dataBaseTest = new InFileDataBaseTest();
            _sessionFactory = _dataBaseTest.sessionFactory;
            var municipio = getMunicipio("01", "01");
            var departamento = getDepartamento("01", municipio);
            var representante = getRepresentante(new Identidad("0801198512396"));

            saveDepartamentoMunicipio(departamento, municipio);
            var direccion = new Direccion(departamento, municipio, "Barrio abajo");
            var rtn = new RTN("08011985123960");
            _originalGremio = new Gremio(rtn, representante, direccion, "Camara");
           
            using (var uow = new NHibernateUnitOfWork(_sessionFactory.OpenSession()))
            {
                var representanteRepository = new RepresentanteLegalRepositoryReadOnly(uow.Session);
                var direccionRepository = new DireccionRepositoryReadOnly(uow.Session);
                _repository = new GremioRepositoryCommands(uow.Session, representanteRepository, direccionRepository);
                _repository.save(_originalGremio);
                uow.commit();

            }

             _representanteLegalNuevo = getRepresentante(new Identidad("0801199911111"));



        };

        private Because of = () =>
        {
            _originalGremio.RepresentanteLegal = _representanteLegalNuevo;
            using (var uow = new NHibernateUnitOfWork(_sessionFactory.OpenSession()))
            {
                var representanteRepository = new RepresentanteLegalRepositoryReadOnly(uow.Session);
                var direccionRepository = new DireccionRepositoryReadOnly(uow.Session);
                _repository = new GremioRepositoryCommands(uow.Session, representanteRepository, direccionRepository);
                _repository.updateRepresentante(_originalGremio);
                uow.commit();

            } 
        };

        It should_update_gremio = () =>
        {
            using (var uow = new NHibernateUnitOfWork(_sessionFactory.OpenSession()))
            {
                _updatedGremio = uow.Session.Get<Gremio>(_originalGremio.Id);
                _updatedGremio.ShouldBeEquivalentTo(_originalGremio);
            }
        };

        #region Helpers Methods
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
                .With(x => x.DepartamentoId = idDepartamento)
                .Build();
            return municipio;
        }

        private static Departamento getDepartamento(string idDepartamento, Municipio municipio)
        {
            var departamento = Builder<Departamento>.CreateNew()
                .With(x => x.Id = idDepartamento)
                .With(x => x.Municipios = new List<Municipio>
                {
                    municipio
                })
                .Build();
            return departamento;
        }

        private static RepresentanteLegal getRepresentante(Identidad identidadRepresentante)
        {
            return new RepresentanteLegal(identidadRepresentante, "Juan Perez");
        }

        #endregion
    }
}
