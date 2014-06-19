using System;
using CNISS.EnterpriseDomain.Domain.Entities;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.Identidad_Test
{
    [TestFixture]
    public class RepresentanteLegal_Test
    {
        [Test]
        public void constructor_nombreNulo_lanzaExcepcion()
        {
            var nombreRepresentanteNulo = string.Empty;
            var identidad = new Identidad("0801198512396");

            Assert.Throws<ArgumentException>(() =>
            {
                new RepresentanteLegal(identidad, nombreRepresentanteNulo);

            }, "Nombre de Representante no puede ser nulo");
        }

        [Test]
        public void constructor_identidadNula_lanzaExcepcion()
        {
            var nombreRepresentante = "Dante";
            Assert.Throws<ArgumentException>(() =>
            {
                new RepresentanteLegal(null, nombreRepresentante);
            }
                ,"Identidad no puede ser nula");
        }
    }
}