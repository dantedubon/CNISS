using System;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.Identidad_Test
{
    [TestFixture]
    public class Identidad_Test
    {
        [Test]
        public void constructor_identidadNoNumerica_lanzaExcepcion()
        {
            var identidadInvalida = @"0T";
            Assert.Throws<ArgumentException>(() =>
            {
                new Identidad(identidadInvalida);
            },"Identidad no numerica");
        }

        
        
        

        [Test]
        public void constructor_identidadNumerica_seCreaIdentidad()
        {
            var identidadValida = @"0801198512396";
            Assert.DoesNotThrow(() =>
            {
                new Identidad(identidadValida);
            });
        }

        [Test]
        public void constructor_identidadMenorA13Caracteres_lanzaExcepcion()
        {
            var identidadCorta = "0801";

            Assert.Throws<ArgumentException>(
                () =>
                {
                    new Identidad(identidadCorta);
                },"Identidad no tiene 13 caracteres");
        }
    
    }
}