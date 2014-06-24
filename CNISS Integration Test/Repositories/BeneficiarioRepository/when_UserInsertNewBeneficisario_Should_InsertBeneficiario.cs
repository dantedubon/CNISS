﻿using System;
using CNISS.CommonDomain.Ports.Output.Database;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using CNISS.EnterpriseDomain.Ports.Output.Database;
using CNISS_Integration_Test.Unit_Of_Work;
using FluentAssertions;
using Machine.Specifications;
using NHibernate;

namespace CNISS_Integration_Test.Repositories.BeneficiarioRepository
{
    [Subject(typeof (BeneficiarioRepositoryCommands))]
    public class when_UserInsertNewBeneficisario_Should_InsertBeneficiario
    {
        static InFileDataBaseTest _dataBaseTest;
        static ISessionFactory _sessionFactory;
        static ISession _session;
        private static IBeneficiarioRepositoryCommands _repositoryCommands;
        private static Beneficiario _expectedBeneficiario;
        private static Beneficiario _responseBeneficiario;

        private Establish context = () =>
        {
            _dataBaseTest = new InFileDataBaseTest();
            _sessionFactory = _dataBaseTest.sessionFactory;
            var parentescoHijo = getParentescoHijo();
            var parentescoMadre = getParentescoMadre();

            _expectedBeneficiario = getBeneficiario();
            _expectedBeneficiario.addDependiente(getDependiente(new Identidad("0801196712396"), new Nombre("Lavinia", "", "Dubon", "Fajardo"), parentescoMadre));
            _expectedBeneficiario.addDependiente(getDependiente(new Identidad("0801196712395"),new Nombre("Daniel", "", "Castillo", "Velasquez"), parentescoHijo));

            prepareParentesco(parentescoHijo);
            prepareParentesco(parentescoMadre);



        };


        private Because of = () =>
        {
            using (var uow = new NHibernateUnitOfWork(_sessionFactory.OpenSession()))
            {
                var beneficiarioRepository = new BeneficiarioRepositoryCommands(uow.Session);
                beneficiarioRepository.save(_expectedBeneficiario);
                uow.commit();
            }


        };

        It should_save_beneficario = () =>
        {
             _session = _sessionFactory.OpenSession();

            using (var tx = _session.BeginTransaction())
            {
                _responseBeneficiario = _session.Get<Beneficiario>(_expectedBeneficiario.Id);
                _responseBeneficiario.ShouldBeEquivalentTo(_expectedBeneficiario);
            }
        };


        private static void prepareParentesco(Parentesco parentesco)
        {
             _session = _sessionFactory.OpenSession();

            using (var tx = _session.BeginTransaction())
            {
                _session.Save(parentesco);
                tx.Commit();
            }
            _session.Close();
        }

        private static Parentesco getParentescoHijo()
        {
            return new Parentesco("Hijo");
        }

        private static Parentesco getParentescoMadre()
        {
            return new Parentesco("Madre");
        }

        private static Dependiente getDependiente(Identidad identidad, Nombre nombre, Parentesco parentesco)
        {
            return new Dependiente(identidad,nombre,parentesco,30);
        }

        private static Beneficiario getBeneficiario()
        {
            var beneficiario = new Beneficiario(new Identidad("0801198512396"),
                new Nombre("Dante", "Dubon", "Castillo", "Rubén"), new DateTime(1984, 8, 2));

            return beneficiario;
        }
    }
}