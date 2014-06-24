using CNISS.CommonDomain.Ports.Input.REST.Request.GremioRequest;
using NUnit.Framework;

namespace CNISS_Tests.RTN_Test.Request
{
    [TestFixture]
    public class RTNRequest_Test
    {
        public object[] badRequestForPost =
            { 
                new object[]{"0801"},
                new object[]{string.Empty},
                new object[]{"0801198512396D"},
                new object[]{null}
            
            };

          [TestCaseSource("badRequestForPost")]
        public void isValidPost_invalidData_RetornaFalse(string RTN)
        {
            var rtn = new RTNRequest();
            rtn.RTN = RTN;
            var respuesta = rtn.isValidPost();

            Assert.IsFalse(respuesta);
        }

        [Test]
        public void isValidPost_validData_returnTrue()
        {
            var rtn = new RTNRequest();
            rtn.RTN = "08011985123960";
            var respuesta = rtn.isValidPost();
            Assert.IsTrue(respuesta);
        }

    }
}