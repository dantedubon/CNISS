using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Domain;
using CNISS.EnterpriseDomain.Application;
using CNISS.EnterpriseDomain.Domain;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.Repositories;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Moq;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empleo_Test.Commands
{
    [TestFixture]
    public class CommandUpdateEmpleo_Test
    {
        [Test]
        public void isExecutable_empleoNotExists_return_false()
        {
            var empresa = getEmpresa();

            var beneficiario = getBeneficiario();

            var sucursal = empresa.sucursales.First();

            var horario = new HorarioLaboral(new Hora(7, 0, "AM"), new Hora(5, 30, "PM"),
                new DiasLaborables() { lunes = true, martes = true });

            var empleo = new Empleo(empresa, sucursal, beneficiario, horario, "Ingeniero", 30000,
                new TipoEmpleo("Horas"), new DateTime(2014, 5, 6));


            var repositoryRead = Mock.Of<IEmpleoRepositoryReadOnly>();
            var repositoryCommands = Mock.Of<IEmpleoRepositoryCommands>();
            var beneficiarioRead = Mock.Of<IBeneficiarioRepositoryReadOnly>();
            var providerDays = Mock.Of<IProvideAllowedDaysForNewEmpleo>();
            var empresaRead = Mock.Of<IEmpresaRepositoryReadOnly>();
            var tiposEmpleoRead = Mock.Of<ITipoDeEmpleoReadOnlyRepository>();
            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());

            Mock.Get(repositoryRead).Setup(x => x.exists(empleo.Id)).Returns(false);

            var command = new CommandUpdateEmpleo(repositoryCommands,uow,repositoryRead,beneficiarioRead,providerDays,empresaRead,tiposEmpleoRead);

            var respuesta = command.isExecutable(empleo);

            Assert.IsFalse(respuesta);
        }


        [Test]
        public void isExecutable_EmpleoRecienteSegunRangoDias_return_false()
        {
            var empresa = getEmpresa();

            var beneficiario = getBeneficiario();

            var sucursal = empresa.sucursales.First();

            var horario = new HorarioLaboral(new Hora(7, 0, "AM"), new Hora(5, 30, "PM"),
                new DiasLaborables() { lunes = true, martes = true });

            var empleo = new Empleo(empresa, sucursal, beneficiario, horario, "Ingeniero", 30000,
                new TipoEmpleo("Horas"), new DateTime(2014, 5, 6));


            var repositoryRead = Mock.Of<IEmpleoRepositoryReadOnly>();
            var repositoryCommands = Mock.Of<IEmpleoRepositoryCommands>();
            var beneficiarioRead = Mock.Of<IBeneficiarioRepositoryReadOnly>();
            var providerDays = Mock.Of<IProvideAllowedDaysForNewEmpleo>();
            var empresaRead = Mock.Of<IEmpresaRepositoryReadOnly>();
            var tiposEmpleoRead = Mock.Of<ITipoDeEmpleoReadOnlyRepository>();
            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());

            Mock.Get(repositoryRead).Setup(x => x.exists(empleo.Id)).Returns(true);
            Mock.Get(providerDays).Setup(x => x.getDays()).Returns(90);
            Mock.Get(repositoryRead)
                .Setup(x => x.existsEmpleoRecienteParaBeneficiario(empleo.fechaDeInicio, 90, empleo.beneficiario.Id))
                .Returns(true);

            var command = new CommandUpdateEmpleo(repositoryCommands, uow, repositoryRead, beneficiarioRead, providerDays, empresaRead, tiposEmpleoRead);

            var respuesta = command.isExecutable(empleo);

            Assert.IsFalse(respuesta);
        }


        [Test]
        public void isExecutable_BeneficiarioNoExiste_return_false()
        {
            var empresa = getEmpresa();

            var beneficiario = getBeneficiario();

            var sucursal = empresa.sucursales.First();

            var horario = new HorarioLaboral(new Hora(7, 0, "AM"), new Hora(5, 30, "PM"),
                new DiasLaborables() { lunes = true, martes = true });

            var empleo = new Empleo(empresa, sucursal, beneficiario, horario, "Ingeniero", 30000,
                new TipoEmpleo("Horas"), new DateTime(2014, 5, 6));


            var repositoryRead = Mock.Of<IEmpleoRepositoryReadOnly>();
            var repositoryCommands = Mock.Of<IEmpleoRepositoryCommands>();
            var beneficiarioRead = Mock.Of<IBeneficiarioRepositoryReadOnly>();
            var providerDays = Mock.Of<IProvideAllowedDaysForNewEmpleo>();
            var empresaRead = Mock.Of<IEmpresaRepositoryReadOnly>();
            var tiposEmpleoRead = Mock.Of<ITipoDeEmpleoReadOnlyRepository>();
            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());

            Mock.Get(repositoryRead).Setup(x => x.exists(empleo.Id)).Returns(true);
            Mock.Get(providerDays).Setup(x => x.getDays()).Returns(90);
            Mock.Get(repositoryRead)
                .Setup(x => x.existsEmpleoRecienteParaBeneficiario(empleo.fechaDeInicio, 90, empleo.beneficiario.Id))
                .Returns(false);

            Mock.Get(beneficiarioRead).Setup(x => x.exists(empleo.beneficiario.Id)).Returns(false);

            var command = new CommandUpdateEmpleo(repositoryCommands, uow, repositoryRead, beneficiarioRead, providerDays, empresaRead, tiposEmpleoRead);

            var respuesta = command.isExecutable(empleo);

            Assert.IsFalse(respuesta);
        }
        [Test]
        public void isExecutable_EmpresaNoExiste_return_false()
        {
            var empresa = getEmpresa();

            var beneficiario = getBeneficiario();

            var sucursal = empresa.sucursales.First();

            var horario = new HorarioLaboral(new Hora(7, 0, "AM"), new Hora(5, 30, "PM"),
                new DiasLaborables() { lunes = true, martes = true });

            var empleo = new Empleo(empresa, sucursal, beneficiario, horario, "Ingeniero", 30000,
                new TipoEmpleo("Horas"), new DateTime(2014, 5, 6));


            var repositoryRead = Mock.Of<IEmpleoRepositoryReadOnly>();
            var repositoryCommands = Mock.Of<IEmpleoRepositoryCommands>();
            var beneficiarioRead = Mock.Of<IBeneficiarioRepositoryReadOnly>();
            var providerDays = Mock.Of<IProvideAllowedDaysForNewEmpleo>();
            var empresaRead = Mock.Of<IEmpresaRepositoryReadOnly>();
            var tiposEmpleoRead = Mock.Of<ITipoDeEmpleoReadOnlyRepository>();
            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());

            Mock.Get(repositoryRead).Setup(x => x.exists(empleo.Id)).Returns(true);
            Mock.Get(providerDays).Setup(x => x.getDays()).Returns(90);
            Mock.Get(repositoryRead)
                .Setup(x => x.existsEmpleoRecienteParaBeneficiario(empleo.fechaDeInicio, 90, empleo.beneficiario.Id))
                .Returns(false);

            Mock.Get(beneficiarioRead).Setup(x => x.exists(empleo.beneficiario.Id)).Returns(true);
            Mock.Get(empresaRead).Setup(x => x.exists(empleo.empresa.Id)).Returns(false);


            var command = new CommandUpdateEmpleo(repositoryCommands, uow, repositoryRead, beneficiarioRead, providerDays, empresaRead, tiposEmpleoRead);

            var respuesta = command.isExecutable(empleo);

            Assert.IsFalse(respuesta);
        }

        [Test]
        public void isExecutable_TipoDeEmpleoNoExists_return_false()
        {
            var empresa = getEmpresa();

            var beneficiario = getBeneficiario();

            var sucursal = empresa.sucursales.First();

            var horario = new HorarioLaboral(new Hora(7, 0, "AM"), new Hora(5, 30, "PM"),
                new DiasLaborables() { lunes = true, martes = true });

            var empleo = new Empleo(empresa, sucursal, beneficiario, horario, "Ingeniero", 30000,
                new TipoEmpleo("Horas"), new DateTime(2014, 5, 6));


            var repositoryRead = Mock.Of<IEmpleoRepositoryReadOnly>();
            var repositoryCommands = Mock.Of<IEmpleoRepositoryCommands>();
            var beneficiarioRead = Mock.Of<IBeneficiarioRepositoryReadOnly>();
            var providerDays = Mock.Of<IProvideAllowedDaysForNewEmpleo>();
            var empresaRead = Mock.Of<IEmpresaRepositoryReadOnly>();
            var tiposEmpleoRead = Mock.Of<ITipoDeEmpleoReadOnlyRepository>();
            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());

            Mock.Get(repositoryRead).Setup(x => x.exists(empleo.Id)).Returns(true);
            Mock.Get(providerDays).Setup(x => x.getDays()).Returns(90);
            Mock.Get(repositoryRead)
                .Setup(x => x.existsEmpleoRecienteParaBeneficiario(empleo.fechaDeInicio, 90, empleo.beneficiario.Id))
                .Returns(false);

            Mock.Get(beneficiarioRead).Setup(x => x.exists(empleo.beneficiario.Id)).Returns(true);
            Mock.Get(empresaRead).Setup(x => x.exists(empleo.empresa.Id)).Returns(true);
            Mock.Get(tiposEmpleoRead).Setup(x => x.exists(empleo.tipoEmpleo.Id)).Returns(false);



            var command = new CommandUpdateEmpleo(repositoryCommands, uow, repositoryRead, beneficiarioRead, providerDays, empresaRead, tiposEmpleoRead);

            var respuesta = command.isExecutable(empleo);

            Assert.IsFalse(respuesta);
        }

        [Test]
        public void isExecutable_EmpresaExisteBeneficiarioExisteTipoEmpleoExisteEmpleoNoReciente_return_true()
        {
            var empresa = getEmpresa();

            var beneficiario = getBeneficiario();

            var sucursal = empresa.sucursales.First();

            var horario = new HorarioLaboral(new Hora(7, 0, "AM"), new Hora(5, 30, "PM"),
                new DiasLaborables() { lunes = true, martes = true });

            var empleo = new Empleo(empresa, sucursal, beneficiario, horario, "Ingeniero", 30000,
                new TipoEmpleo("Horas"), new DateTime(2014, 5, 6));


            var repositoryRead = Mock.Of<IEmpleoRepositoryReadOnly>();
            var repositoryCommands = Mock.Of<IEmpleoRepositoryCommands>();
            var beneficiarioRead = Mock.Of<IBeneficiarioRepositoryReadOnly>();
            var providerDays = Mock.Of<IProvideAllowedDaysForNewEmpleo>();
            var empresaRead = Mock.Of<IEmpresaRepositoryReadOnly>();
            var tiposEmpleoRead = Mock.Of<ITipoDeEmpleoReadOnlyRepository>();
            var uow = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uow).Setup(x => x()).Returns(new DummyUnitOfWork());

            Mock.Get(repositoryRead).Setup(x => x.exists(empleo.Id)).Returns(true);
            Mock.Get(providerDays).Setup(x => x.getDays()).Returns(90);
            Mock.Get(repositoryRead)
                .Setup(x => x.existsEmpleoRecienteParaBeneficiario(empleo.fechaDeInicio, 90, empleo.beneficiario.Id))
                .Returns(false);

            Mock.Get(beneficiarioRead).Setup(x => x.exists(empleo.beneficiario.Id)).Returns(true);
            Mock.Get(empresaRead).Setup(x => x.exists(empleo.empresa.Id)).Returns(true);
            Mock.Get(tiposEmpleoRead).Setup(x => x.exists(empleo.tipoEmpleo.Id)).Returns(true);



            var command = new CommandUpdateEmpleo(repositoryCommands, uow, repositoryRead, beneficiarioRead, providerDays, empresaRead, tiposEmpleoRead);

            var respuesta = command.isExecutable(empleo);

            Assert.IsTrue(respuesta);
        }

        private Parentesco getParentescoMadre()
        {
            return new Parentesco("Madre");
        }

        private Dependiente getDependiente(Identidad identidad, Nombre nombre, Parentesco parentesco)
        {
            return new Dependiente(identidad, nombre, parentesco, new DateTime(1984, 8, 2));
        }

        private Beneficiario getBeneficiario()
        {
            var beneficiario = new Beneficiario(new Identidad("0801198512396"),
                new Nombre("Dante", "Castillo", "Rubén"), new DateTime(1984, 8, 2));

            var dependiente1 = getDependiente(new Identidad("0801195712396"), new Nombre("Lavinia Dubon", "Fajardo", ""),
                getParentescoMadre());

            beneficiario.addDependiente(dependiente1);

            return beneficiario;
        }







        private Empresa getEmpresa()
        {
            var actividades = getActividadEconomicas();
            var sucursales = getSucursales();
            var gremio = getGremio();
            var fechaIngreso = DateTime.ParseExact(DateTime.Now.ToString("g"), "g", null);
            var rtn = new RTN("08011985123960");
            var empresa = new Empresa(rtn, "La Holgazana", fechaIngreso, gremio);

            empresa.actividadesEconomicas = actividades;
            empresa.sucursales = sucursales;
            empresa.contrato = getContrato();
            return empresa;
        }

        private IEnumerable<ActividadEconomica> getActividadEconomicas()
        {
            return new List<ActividadEconomica>()
            {
                new ActividadEconomica("Camaronera"),
                new ActividadEconomica("Arrocera")
            };
        }

        private Gremio getGremio()
        {
            var municipio = new Municipio("01", "01", "Municipio");
            var departamento = new Departamento() { Id = "01", municipios = new List<Municipio>() { municipio }, nombre = "Departamento" };
            var direccion = new Direccion(departamento, municipio, "direccion gremio");

            var RTN = new RTN("08011985123960");
            var representante = new RepresentanteLegal(new Identidad("0801198512396"), "Dante");

            var gremio = new Gremio(RTN, representante, direccion, "Camara");
            return gremio;

        }

        private IList<Sucursal> getSucursales()
        {
            var municipio = new Municipio("01", "01", "Municipio");
            var departamento = new Departamento() { Id = "01", municipios = new List<Municipio>() { municipio }, nombre = "Departamento" };
            var direccion = new Direccion(departamento, municipio, "direccion");
            var fechaDeCreacionFirma = DateTime.ParseExact(DateTime.Now.ToString("g"), "g", null);

            var firma1 = new FirmaAutorizada(new User("DRCD", "Dante", "Ruben", "SDSD", "as", new Rol("rol", "rol")), fechaDeCreacionFirma);
            var firma2 = new FirmaAutorizada(new User("Angela", "Angela", "Castillo", "SSS", "SS", new Rol("rol", "rol")), fechaDeCreacionFirma);

            var sucursal1 = new Sucursal("El Centro", direccion, firma1);
            var sucursal2 = new Sucursal("Barrio Abajo", direccion, firma2);


            return new List<Sucursal>() { sucursal1, sucursal2 };

        }

        private ContentFile getContrato()
        {
            var data = new byte[5];
            return new ContentFile(data);
        } 
    }
}