using System;

namespace CNISS.CommonDomain.Ports.Input.REST.Request.RolModule
{
    public class RolRequest
    {
        public Guid idGuid { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}