using System;
using System.Collections.Generic;
using CNISS.AutenticationDomain.Domain.Entities;
using CNISS.AutenticationDomain.Domain.Repositories;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.EnterpriseDomain.Application;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using Moq;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.Entities_Test.Empresas_Test
{
    [TestFixture]
    public class ServiceSucursalesValidator_Test
    {
        [Test]
        public void isValid_direccionInvalida_return_false()
        {
            var validadorDireccion = Mock.Of<IServiceDireccionValidator>();
            var userRepository = Mock.Of<IUserRepositoryReadOnly>();

            Mock.Get(validadorDireccion).Setup(x => x.isValidDireccion(It.IsAny<Direccion>())).Returns(false);

            var servicio = new ServiceSucursalesValidator(validadorDireccion, userRepository);
            var sucursales = getSucursales();

            var respuesta = servicio.isValid(sucursales);

            Assert.IsFalse(respuesta);


        }

        [Test]
        public void isValid_FirmaInvalida_return_false()
        {
            var validadorDireccion = Mock.Of<IServiceDireccionValidator>();
            var userRepository = Mock.Of<IUserRepositoryReadOnly>();

            Mock.Get(validadorDireccion).Setup(x => x.isValidDireccion(It.IsAny<Direccion>())).Returns(true);
            Mock.Get(userRepository).Setup(x => x.exists(It.IsAny<string>())).Returns(false);

            var servicio = new ServiceSucursalesValidator(validadorDireccion, userRepository);
            var sucursales = getSucursales();

            var respuesta = servicio.isValid(sucursales);

            Assert.IsFalse(respuesta);


        }

        private IEnumerable<Sucursal> getSucursales()
        {
            var municipio = new Municipio("01", "01", "Municipio");
            var departamento = new Departamento() { Id = "01", Municipios = new List<Municipio>() { municipio }, Nombre = "Departamento" };
            var direccion = new Direccion(departamento, municipio, "direccion");
            var firma = new FirmaAutorizada(new User("DRCD", "Dante", "Ruben", "SDSD", "as", new Rol("rol", "rol")), DateTime.Now);

            var sucursal = new Sucursal("El Centro", direccion, firma);
            return new List<Sucursal>() { sucursal };

        }
    }
}