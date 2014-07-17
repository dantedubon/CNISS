using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.Services;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Application;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Moq;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empleo_Test.Commands
{
    [TestFixture]
    public class CommandInsertFichaDeSupervision_Test
    {
        [Test]
        public void isExecutable_UsuarioFirmaNoValido_ReturnFalse()
        {
            var repositoryEmpleoRead = Mock.Of<IEmpleoRepositoryReadOnly>();
            var repositoryEmpleoCommand = Mock.Of<IEmpleoRepositoryCommands>();
            var repositoryBeneficiarioRead = Mock.Of<IBeneficiarioRepositoryReadOnly>();
            var repositoryBeneficarioCommand = Mock.Of<IBeneficiarioRepositoryCommands>();
            var authenticateUser = Mock.Of<IAuthenticateUser>();
            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());

            var ficha = getFichaSupervisionEmpleo();
            var beneficiaro = getBeneficiario();
            var idEmpleo = Guid.NewGuid();

            Mock.Get(authenticateUser).Setup(x => x.isValidUser(ficha.firma.user, 1)).Returns(false);

            var commando = new CommandInsertFichaDeSupervision(repositoryEmpleoRead, repositoryEmpleoCommand,
                repositoryBeneficiarioRead, repositoryBeneficarioCommand, authenticateUser,uow);

            var resultado = commando.isExecutable(ficha, beneficiaro,idEmpleo);

            Assert.IsFalse(resultado);
        }


        [Test]
        public void isExecutable_BeneficiarioNotExists_ReturnFalse()
        {
            var repositoryEmpleoRead = Mock.Of<IEmpleoRepositoryReadOnly>();
            var repositoryEmpleoCommand = Mock.Of<IEmpleoRepositoryCommands>();
            var repositoryBeneficiarioRead = Mock.Of<IBeneficiarioRepositoryReadOnly>();
            var repositoryBeneficarioCommand = Mock.Of<IBeneficiarioRepositoryCommands>();
            var authenticateUser = Mock.Of<IAuthenticateUser>();
            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());

            var ficha = getFichaSupervisionEmpleo();
            var beneficiaro = getBeneficiario();
            var idEmpleo = Guid.NewGuid();

            Mock.Get(authenticateUser).Setup(x => x.isValidUser(ficha.firma.user, 1)).Returns(true);
            Mock.Get(repositoryBeneficiarioRead).Setup(x => x.exists(beneficiaro.Id)).Returns(false);

            var commando = new CommandInsertFichaDeSupervision(repositoryEmpleoRead, repositoryEmpleoCommand,
                repositoryBeneficiarioRead, repositoryBeneficarioCommand, authenticateUser,uow);

            var resultado = commando.isExecutable(ficha, beneficiaro, idEmpleo);

            Assert.IsFalse(resultado);
        }

        [Test]
        public void isExecutable_EmpleoNotExists_ReturnFalse()
        {
            var repositoryEmpleoRead = Mock.Of<IEmpleoRepositoryReadOnly>();
            var repositoryEmpleoCommand = Mock.Of<IEmpleoRepositoryCommands>();
            var repositoryBeneficiarioRead = Mock.Of<IBeneficiarioRepositoryReadOnly>();
            var repositoryBeneficarioCommand = Mock.Of<IBeneficiarioRepositoryCommands>();
            var authenticateUser = Mock.Of<IAuthenticateUser>();
            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());


            var ficha = getFichaSupervisionEmpleo();
            var beneficiaro = getBeneficiario();
            var idEmpleo = Guid.NewGuid();

            Mock.Get(authenticateUser).Setup(x => x.isValidUser(ficha.firma.user, 1)).Returns(true);
            Mock.Get(repositoryBeneficiarioRead).Setup(x => x.exists(beneficiaro.Id)).Returns(true);
            Mock.Get(repositoryEmpleoRead).Setup(x => x.exists(idEmpleo)).Returns(false);

            var commando = new CommandInsertFichaDeSupervision(repositoryEmpleoRead, repositoryEmpleoCommand,
                repositoryBeneficiarioRead, repositoryBeneficarioCommand, authenticateUser,uow);

            var resultado = commando.isExecutable(ficha, beneficiaro, idEmpleo);

            Assert.IsFalse(resultado);
        }

        [Test]
        public void execute_BeneficiarioCommandRepositoryCalled_insertFichaSupervision()
        {
            var repositoryEmpleoRead = Mock.Of<IEmpleoRepositoryReadOnly>();
            var repositoryEmpleoCommand = Mock.Of<IEmpleoRepositoryCommands>();
            var repositoryBeneficiarioRead = Mock.Of<IBeneficiarioRepositoryReadOnly>();
            var repositoryBeneficarioCommand = Mock.Of<IBeneficiarioRepositoryCommands>();
            var authenticateUser = Mock.Of<IAuthenticateUser>();
            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());

            var ficha = getFichaSupervisionEmpleo();
            var beneficiaro = getBeneficiario();
            var idEmpleo = Guid.NewGuid();

            Mock.Get(authenticateUser).Setup(x => x.isValidUser(ficha.firma.user, 1)).Returns(true);
            Mock.Get(repositoryBeneficiarioRead).Setup(x => x.exists(beneficiaro.Id)).Returns(true);
            Mock.Get(repositoryEmpleoRead).Setup(x => x.exists(idEmpleo)).Returns(true);

            var commando = new CommandInsertFichaDeSupervision(repositoryEmpleoRead, repositoryEmpleoCommand,
                repositoryBeneficiarioRead, repositoryBeneficarioCommand, authenticateUser,uow);

           commando.execute(ficha, beneficiaro, idEmpleo);

            Mock.Get(repositoryBeneficarioCommand).Verify(x => x.updateInformationFromMovil(beneficiaro));
        }

        [Test]
        public void execute_EmpleoRepositoryCalled_insertFichaSupervision()
        {
            var repositoryEmpleoRead = Mock.Of<IEmpleoRepositoryReadOnly>();
            var repositoryEmpleoCommand = Mock.Of<IEmpleoRepositoryCommands>();
            var repositoryBeneficiarioRead = Mock.Of<IBeneficiarioRepositoryReadOnly>();
            var repositoryBeneficarioCommand = Mock.Of<IBeneficiarioRepositoryCommands>();
            var authenticateUser = Mock.Of<IAuthenticateUser>();
            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());

            var ficha = getFichaSupervisionEmpleo();
            var beneficiaro = getBeneficiario();
            var idEmpleo = Guid.NewGuid();

            Mock.Get(authenticateUser).Setup(x => x.isValidUser(ficha.firma.user, 1)).Returns(true);
            Mock.Get(repositoryBeneficiarioRead).Setup(x => x.exists(beneficiaro.Id)).Returns(true);
            Mock.Get(repositoryEmpleoRead).Setup(x => x.exists(idEmpleo)).Returns(true);

            var commando = new CommandInsertFichaDeSupervision(repositoryEmpleoRead, repositoryEmpleoCommand,
                repositoryBeneficiarioRead, repositoryBeneficarioCommand, authenticateUser, uow);

            commando.execute(ficha, beneficiaro, idEmpleo);

            Mock.Get(repositoryEmpleoCommand).Verify(x => x.updateFromMovilVisitaSupervision(idEmpleo,ficha));
        }

        private FichaSupervisionEmpleo getFichaSupervisionEmpleo()
        {
            return new FichaSupervisionEmpleo(getSupervisor(), getFirmaAutorizada(), "GPS", "Cargo", "Funciones",
                "31804433", "31804433", 9, getContentFile());
        }
        private Beneficiario getBeneficiario()
        {
            var beneficiario = new Beneficiario(new Identidad("0801198512396"),
                new Nombre("Dante", "Castillo", "Rubén"), new DateTime(1984, 8, 2));


            var parentescoHijo = getParentescoHijo();
            var parentescoMadre = getParentescoMadre();


            beneficiario.addDependiente(getDependiente(new Identidad("0801196712396"), new Nombre("Lavinia", "Dubon", "Fajardo"), parentescoMadre));
            beneficiario.addDependiente(getDependiente(new Identidad("0801196712395"), new Nombre("Daniel", "Castillo", "Velasquez"), parentescoHijo));
            return beneficiario;
        }

        private Parentesco getParentescoHijo()
        {
            return new Parentesco("Hijo");
        }

        private Parentesco getParentescoMadre()
        {
            return new Parentesco("Madre");
        }

        private Parentesco getParentescoPadre()
        {
            return new Parentesco("Padre");
        }



        private Dependiente getDependiente(Identidad identidad, Nombre nombre, Parentesco parentesco)
        {
            return new Dependiente(identidad, nombre, parentesco, new DateTime(1984, 8, 2));
        }


        private Supervisor getSupervisor()
        {
            return new Supervisor(new User("User","User","","XXX","mail",new RolNull()));
        }

        private FirmaAutorizada getFirmaAutorizada()
        {
            return new FirmaAutorizada(new User("User", "User", "", "XXX", "mail", new RolNull()),DateTime.Now.Date);
        }

        private ContentFile getContentFile()
        {
            return new ContentFile(new byte[]{1,0});
        }
    }
}