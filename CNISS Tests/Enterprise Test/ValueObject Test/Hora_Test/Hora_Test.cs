using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.ValueObject_Test.HorarioLaboral_Test
{
    [TestFixture]
    public class Hora_Test
    {
        [Test]
        public void constructor_horaMayor12_lanzaExcepcion()
        {
          

            Assert.Throws<ArgumentException>(() => new Hora(13, 0,"AM"));

        }

        [Test]
        public void constructor_horaMenor1_lanzaExcepcion()
        {


            Assert.Throws<ArgumentException>(() => new Hora(0, 0,"AM"));

        }
        [Test]
        public void constructor_minutosMayorA59_lanzaExcepcion()
        {


            Assert.Throws<ArgumentException>(() => new Hora(1, 60,"AM"));

        }

        [Test]
        public void constructor_minutosMenorA0_lanzaExcepcion()
        {


            Assert.Throws<ArgumentException>(() => new Hora(1, -1,"AM"));

        }

        [Test]
        public void constructor_parteDiferenteAMoPM_lanzaExcepcion()
        {


            Assert.Throws<ArgumentException>(() => new Hora(1, 0,"XX"));

        }
        [Test]
        public void constructor_parteIgualAM_NolanzaExcepcion()
        {

            Assert.DoesNotThrow(() => new Hora(1, 0, "AM"));
        

        }

        [Test]
        public void constructor_parteIgualPM_NolanzaExcepcion()
        {

            Assert.DoesNotThrow(() => new Hora(1, 0, "PM"));


        }

    }
}