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

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empresas_Test.Command
{
    [TestFixture]
    public class CommandInsertEmpresa_Test
    {
        [Test]
        public void isExecutable_EmpresaAlreadyExists_return_false()
        {
            var empresa = getEmpresa();

            var repositoryReadOnly = Mock.Of<IEmpresaRepositoryReadOnly>();
            var repositoryCommand = Mock.Of<IEmpresaRepositoryCommands>();
            var repositoryActividadesRead = Mock.Of<IActividadEconomicaRepositoryReadOnly>();
            var repositoryGremiosRead = Mock.Of<IGremioRepositoryReadOnly>();
            var validadorSucursales = Mock.Of<IServiceSucursalesValidator>();
            var uof = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uof).Setup(x => x()).Returns(new DummyUnitOfWork());
            var command = new CommandInsertEmpresa(validadorSucursales, repositoryGremiosRead, repositoryActividadesRead, repositoryReadOnly, repositoryCommand, uof);

            Mock.Get(repositoryReadOnly).Setup(x => x.exists(empresa.Id)).Returns(true);
           

            var respuesta = command.isExecutable(empresa);

            Assert.IsFalse(respuesta);


        }

        private Empresa getEmpresa()
        {
            var actividades = getActividadEconomicas();
            var sucursales = getSucursales();
            var gremio = getGremio();
            var fechaIngreso = DateTime.Now;
            var rtn = new RTN("08011985123960");
            var empresa = new Empresa(rtn, "La Holgazana", fechaIngreso, gremio);
            empresa.actividadesEconomicas = actividades;
            empresa.sucursales = sucursales;
            empresa.contrato = getContrato();
            return empresa;
        }

        

        [Test]
        public void isExecutable_actividadesEconomicasNotExists_return_false()
        {
            var empresa = getEmpresa();
            var repositoryReadOnly = Mock.Of<IEmpresaRepositoryReadOnly>();
            var repositoryCommand = Mock.Of<IEmpresaRepositoryCommands>();
            var repositoryActividadesRead = Mock.Of<IActividadEconomicaRepositoryReadOnly>();
            var repositoryGremiosRead = Mock.Of<IGremioRepositoryReadOnly>();
            var validadorSucursales = Mock.Of<IServiceSucursalesValidator>();
            var uof = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uof).Setup(x => x()).Returns(new DummyUnitOfWork());
            Mock.Get(repositoryReadOnly).Setup(x => x.exists(empresa.Id)).Returns(false);
            Mock.Get(repositoryActividadesRead).Setup((x => x.existsAll(It.IsAny<IEnumerable<ActividadEconomica>>()))).Returns(false);

            var command = new CommandInsertEmpresa(validadorSucursales, repositoryGremiosRead, repositoryActividadesRead, repositoryReadOnly, repositoryCommand, uof);

            var respuesta = command.isExecutable(empresa);

           Assert.IsFalse(respuesta);

        }

       

        [Test]
        public void isExecutable_gremialNotExists_return_false()
        {
            var empresa = getEmpresa();
            var repositoryReadOnly = Mock.Of<IEmpresaRepositoryReadOnly>();
            var repositoryCommand = Mock.Of<IEmpresaRepositoryCommands>();
            var repositoryActividadesRead = Mock.Of<IActividadEconomicaRepositoryReadOnly>();
            var repositoryGremiosRead = Mock.Of<IGremioRepositoryReadOnly>();
            var validadorSucursales = Mock.Of<IServiceSucursalesValidator>();
            var uof = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uof).Setup(x => x()).Returns(new DummyUnitOfWork());
            Mock.Get(repositoryReadOnly).Setup(x => x.exists(empresa.Id)).Returns(false);
            Mock.Get(repositoryActividadesRead).Setup((x => x.existsAll(It.IsAny<IEnumerable<ActividadEconomica>>()))).Returns(true);
            Mock.Get(repositoryGremiosRead).Setup(x => x.exists(It.IsAny<RTN>())).Returns(false);

            var command = new CommandInsertEmpresa(validadorSucursales, repositoryGremiosRead, repositoryActividadesRead, repositoryReadOnly, repositoryCommand, uof);

            var respuesta = command.isExecutable(empresa);

            Assert.IsFalse(respuesta);


        }

        [Test]
        public void isExecutable_contratoNulo_return_false()
        {
            var empresa = getEmpresa();
            var repositoryReadOnly = Mock.Of<IEmpresaRepositoryReadOnly>();
            var repositoryCommand = Mock.Of<IEmpresaRepositoryCommands>();
            var repositoryActividadesRead = Mock.Of<IActividadEconomicaRepositoryReadOnly>();
            var repositoryGremiosRead = Mock.Of<IGremioRepositoryReadOnly>();
            var validadorSucursales = Mock.Of<IServiceSucursalesValidator>();
            var uof = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uof).Setup(x => x()).Returns(new DummyUnitOfWork());
            Mock.Get(repositoryReadOnly).Setup(x => x.exists(empresa.Id)).Returns(false);
            Mock.Get(repositoryActividadesRead).Setup((x => x.existsAll(It.IsAny<IEnumerable<ActividadEconomica>>()))).Returns(true);
            Mock.Get(repositoryGremiosRead).Setup(x => x.exists(It.IsAny<RTN>())).Returns(true);
            empresa.contrato.dataFile = null;

            var command = new CommandInsertEmpresa(validadorSucursales, repositoryGremiosRead, repositoryActividadesRead, repositoryReadOnly, repositoryCommand, uof);

            var respuesta = command.isExecutable(empresa);

            Assert.IsFalse(respuesta);

        }

        [Test]
        public void isExecutable_sucursalesInvalidas_return_false()
        {
            var empresa = getEmpresa();
            var repositoryReadOnly = Mock.Of<IEmpresaRepositoryReadOnly>();
            var repositoryCommand = Mock.Of<IEmpresaRepositoryCommands>();
            var repositoryActividadesRead = Mock.Of<IActividadEconomicaRepositoryReadOnly>();
            var repositoryGremiosRead = Mock.Of<IGremioRepositoryReadOnly>();
            var validadorSucursales = Mock.Of<IServiceSucursalesValidator>();
            var uof = Mock.Of<Func<IUnitOfWork>>();
            Mock.Get(uof).Setup(x => x()).Returns(new DummyUnitOfWork());
            Mock.Get(repositoryReadOnly).Setup(x => x.exists(empresa.Id)).Returns(false);
            Mock.Get(repositoryActividadesRead).Setup((x => x.existsAll(It.IsAny<IEnumerable<ActividadEconomica>>()))).Returns(true);
            Mock.Get(repositoryGremiosRead).Setup(x => x.exists(It.IsAny<RTN>())).Returns(true);
            Mock.Get(validadorSucursales).Setup(x => x.isValid(empresa.sucursales)).Returns(false);

            var command = new CommandInsertEmpresa(validadorSucursales, repositoryGremiosRead, repositoryActividadesRead, repositoryReadOnly, repositoryCommand, uof);

            var respuesta = command.isExecutable(empresa);

            Assert.IsFalse(respuesta);
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

        private IEnumerable<Sucursal> getSucursales()
        {
            var municipio = new Municipio("01", "01", "Municipio");
            var departamento = new Departamento() {Id = "01", municipios =new List<Municipio>() {municipio}, nombre = "Departamento"};
            var direccion = new Direccion(departamento, municipio, "direccion");
            var firma = new FirmaAutorizada(new User("DRCD","Dante","Ruben","SDSD","as",new Rol("rol", "rol")),DateTime.Now);

            var sucursal = new Sucursal("El Centro", direccion, firma);
            return new List<Sucursal>() {sucursal};

        }

        private ContentFile getContrato()
        {
            var data = new byte[5];
            return new ContentFile(data);
        }

        
    }
}