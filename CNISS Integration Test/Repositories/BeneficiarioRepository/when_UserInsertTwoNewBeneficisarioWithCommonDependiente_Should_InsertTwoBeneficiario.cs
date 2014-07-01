using System;
using System.ComponentModel.DataAnnotations;
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
    [Subject(typeof(BeneficiarioRepositoryCommands))]
    public class when_UserInsertTwoNewBeneficisarioWithCommonDependiente_Should_InsertTwoBeneficiario
    {
        static InFileDataBaseTest _dataBaseTest;
        static ISessionFactory _sessionFactory;
        static ISession _session;
        private static IBeneficiarioRepositoryCommands _repositoryCommands;
        private static Beneficiario _expectedBeneficiario;
        private static Beneficiario _responseBeneficiario;
        private static Beneficiario _expectedBeneficiario2;
        private static Beneficiario _responseBeneficiario2;

        private Establish context = () =>
        {
            _dataBaseTest = new InFileDataBaseTest();
            _sessionFactory = _dataBaseTest.sessionFactory;
            var parentescoHijo = getParentescoHijo();
            var parentescoMadre = getParentescoMadre();
            var parentescoPadre = getParentescoPadre();

            _expectedBeneficiario = getBeneficiario(new Identidad("0801198512396"),new Nombre("Dante","Ruben","Castillo"),new DateTime(1984,8,2)  );
            _expectedBeneficiario2 = getBeneficiario(new Identidad("0801197915396"),
                new Nombre("Angela", "Velasquez", "Rosario"), new DateTime(1979, 4, 21));


            var dependiente1 = getDependiente(new Identidad("0801195712396"),
                new Nombre("Lavinia", "Dubon", "Fajardo"), parentescoMadre);
            var dependiente2 = getDependiente(new Identidad("0801201516395"),
                new Nombre("Daniel", "Castillo", "Velasquez"), parentescoHijo);
            var dependiente3 = getDependiente(new Identidad("0801195013366"),
               new Nombre("David", "Castillo", "Velasquez"), parentescoPadre);

            var dependiente4 = getDependiente(new Identidad("0801195712396"),
                new Nombre("Lavinia", "Dubon", "Fajardo"), parentescoMadre);


            _expectedBeneficiario.addDependiente(dependiente1);
            _expectedBeneficiario.addDependiente(dependiente2);

            _expectedBeneficiario2.addDependiente(dependiente3);
            _expectedBeneficiario2.addDependiente(dependiente4);

            prepareParentesco(parentescoHijo);
            prepareParentesco(parentescoMadre);
            prepareParentesco(parentescoPadre);



        };


        private Because of = () =>
        {
            using (var uow = new NHibernateUnitOfWork(_sessionFactory.OpenSession()))
            {
                var beneficiarioRepository = new BeneficiarioRepositoryCommands(uow.Session);
               
                beneficiarioRepository.save(_expectedBeneficiario);
                beneficiarioRepository.save(_expectedBeneficiario2);
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
                _responseBeneficiario2 = _session.Get<Beneficiario>(_expectedBeneficiario2.Id);
                _responseBeneficiario2.ShouldBeEquivalentTo(_expectedBeneficiario2);
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
        private static Parentesco getParentescoPadre()
        {
            return new Parentesco("Padre");
        }

        private static Parentesco getParentescoMadre()
        {
            return new Parentesco("Madre");
        }

        private static Dependiente getDependiente(Identidad identidad, Nombre nombre, Parentesco parentesco)
        {
            return new Dependiente(identidad, nombre, parentesco, 30);
        }

        private static Beneficiario getBeneficiario(Identidad identidad, Nombre nombre, DateTime fechaNacimiento)
        {
            var beneficiario = new Beneficiario(identidad,
               nombre,fechaNacimiento);

            return beneficiario;
        }
    }
}