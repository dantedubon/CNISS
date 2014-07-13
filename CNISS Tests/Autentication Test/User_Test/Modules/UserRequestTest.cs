using System;
using CNISS.CommonDomain.Ports.Input.REST.Request.RolModule;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using NUnit.Framework;

namespace CNISS_Tests.User_Test.Modules
{
    public class UserRequestTest:TestFixtureAttribute
    {
        public object[] badRequestForAuthenticate =
        {
            new object[] {"", "password"},
            new object[] {"user", ""}
        };

        public object[] badRequestForPost =
        {
           new object[] {null,null,null,null,null,null},
            new object[]{null, "firstName", "secondName", "password", "mail", new RolRequest(){idGuid = Guid.NewGuid()}},
            new object[]{"id", "firstName", null, "password", "mail",new RolRequest(){idGuid = Guid.NewGuid()}},
            new object[]{"id", "firstName", "SecondName", null, "mail",new RolRequest(){idGuid = Guid.NewGuid()}},
            new object[]{"id", "firstName", "SecondName", "password", null,new RolRequest(){idGuid = Guid.NewGuid()}},
            new object[]{"id", "firstName", "SecondName", "password", "mail",null},
            new object[]{"id", "firstName", "SecondName", "password", "mail",new RolRequest()},
        };


        [ TestCaseSource("badRequestForPost")]
        
        public void isValidPost_InvalidData_ReturnFalse(string _id , string _firstName, string _secondName, string _password, string _mail, RolRequest _userRolId)
        {
         
            
            var request = new UserRequest
            {
                Id = _id,
                firstName = _firstName,
                secondName = _secondName,
                mail = _mail,
                password = _password,
                userRol = _userRolId
            };

            Assert.IsFalse(request.isValidPost());

            
        }


        [TestCaseSource("badRequestForAuthenticate")]
        public void isValidForAuthenticate_InvalidData_ReturnsFalse(string id, string password)
        {
            var request = new UserRequest()
            {
                Id = id,
                password = password


            };

            Assert.IsFalse(request.isValidForAuthenticate());
        }
        [Test]
        public void isValidPost_ValidData_ReturnTrue()
        {

            var request = new UserRequest
            {
                Id = "Id",
                firstName = "firstName",
                secondName = "secondName",
                mail = "mail",
                password = "password",
                userRol = new RolRequest
                {
                    idGuid = Guid.NewGuid()
                }
            };

            Assert.IsTrue(request.isValidPost());
        }

        [Test]
        public void isValidDelete_InvalidData_ReturnFalse()
        {
            var request = new UserRequest
            {
                Id = "",
                firstName = null,
                secondName = null,
                mail = null,
                password = null,
                userRol = null
            };

            Assert.IsFalse(request.isValidDelete());
        }

        [Test]
        public void isValidDelete_ValidData_ReturnTrue()
        {
            var request = new UserRequest
            {
                Id = "1",
                firstName = null,
                secondName = null,
                mail = null,
                password = null,
                userRol = null
            };

            Assert.IsTrue(request.isValidDelete());
        }
    }
}