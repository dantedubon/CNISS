using System;
using CNISS.AutenticationDomain.Domain.ValueObjects;
using CNISS.CommonDomain.Ports.Input.REST.Request.RolModule;

namespace CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest
{
    public class UserRequest
    {
        public  String Id { get; set; }
        public  String firstName { get; set; }
        public  String mail { get; set; }
        public  String secondName { get; set; }
        public  String password { get; set; }
        public  RolRequest userRol { get; set; }
    }
}