using System;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.ValueObject_Test
{
    [TestFixture]
    public class Direccion_Test
    {

        [Test]
        public void constructor_MunicipioNulo_LanzaExcepcion()
        {
            var departamento = new Departamento();
            Assert.Throws<ArgumentNullException>(() =>
                new Direccion(departamento,null, @"B Abajo"), "El municipio no puede ser nulo"
                );
        }

        [Test]
        public void constructor_ReferenciaDireccionNula_LanzaExcepcion()
        {
            var departamento = new Departamento();
            var municipio = new Municipio();
            var referenciaNula = string.Empty;
            Assert.Throws<ArgumentException>(
                ()=>
                    new Direccion(departamento,municipio, referenciaNula), "Referencia no puede ser nula"
                );
        }

        [Test]
        public void constructor_ReferenciaDepartamentoNula_LanzaExcepcion()
        {
            var municipio = new Municipio();
           
            Assert.Throws<ArgumentNullException>(
                () =>
                    new Direccion(null, municipio, "B El Centro"), "El departamento no puede ser nulo"
                );
        }
    }
}