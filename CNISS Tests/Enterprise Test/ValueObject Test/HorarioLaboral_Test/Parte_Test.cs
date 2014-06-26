using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNISS.EnterpriseDomain.Domain.ValueObjects;
using NUnit.Framework;

namespace CNISS_Tests.Enterprise_Test.ValueObject_Test.HorarioLaboral_Test
{
    [TestFixture]
    public class Parte_Test
    {
        [Test]
        public void AM_constructor_parteAM()
        {
            var am = new AM();

            Assert.AreEqual(am.parte,"AM");
        }


        [Test]
        public void PM_constructor_partePM()
        {
            var pm = new PM();

            Assert.AreEqual(pm.parte,"PM");
        }
    }
}