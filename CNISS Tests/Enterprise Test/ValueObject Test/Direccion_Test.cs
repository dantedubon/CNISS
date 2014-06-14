using System;
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
            Assert.Throws<ArgumentNullException>(() => 
                new Direccion(null,@"B Abajo"), "El municipio no puede ser nulo"
                );
        }

        [Test]
        public void constructor_ReferenciaDireccionNula_LanzaExcepcion()
        {
            var municipio = new Municipio();
            var referenciaNula = string.Empty;
            Assert.Throws<ArgumentException>(
                ()=>
                    new Direccion(municipio, referenciaNula),"Referencia no puede ser nula"
                );
        }
    }
}