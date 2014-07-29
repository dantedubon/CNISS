using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Web;
using CNISS.CommonDomain.Ports;
using CNISS.CommonDomain.Ports.Input.REST.Request.AuditoriaRequest;
using CNISS.CommonDomain.Ports.Input.REST.Request.RolModule;
using CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest;
using FluentAssertions;
using NUnit.Framework;

namespace CNISS_Tests.Autentication_Test
{
    [TestFixture]
    public class RequestMovilSerializer_Test
    {
        [Test]
        public void SerializeRequest_ObjectSend_ObjectSerializedToJsonStringAndReturnedToObject()
        {

            var user = new UserRequest()
                       {
                           firstName = "Dante", 
                           secondName = "Rubén", 
                           Id = "DRCD",
                           mail = "dante.dubon", 
                           password = "xxx",
                           auditoriaRequest = new AuditoriaRequest()
                                              {
                                                  fechaCreo = DateTime.Now.Date,
                                                  fechaModifico = DateTime.Now.Date,
                                                  usuarioCreo = "DRCD",
                                                  usuarioModifico = "DRCD"

                                              },
                           userRol = new RolRequest()
                                     {
                                         idGuid = Guid.NewGuid(),
                                         name = "rol",
                                         description = "descripcion",
                                         auditoriaRequest = new AuditoriaRequest()
                                         {
                                             fechaCreo = DateTime.Now.Date,
                                             fechaModifico = DateTime.Now.Date,
                                             usuarioCreo = "DRCD",
                                             usuarioModifico = "DRCD"

                                         },
                                         nivel = 1
                                     }
                       };
            var serialized = new SerializerRequest().toJson( user);
            var objectDeserialized = new SerializerRequest().fromJson<UserRequest>(serialized);

            user.ShouldBeEquivalentTo(objectDeserialized);

        }

        



       
    }
}