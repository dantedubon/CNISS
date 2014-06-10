using System;

namespace CNISS.CommonDomain.Ports.Input.REST.Request.RolModule
{
    public class RolRequest:IValidRequest
    {
        public Guid idGuid { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public bool isValidGet()
        {
      
           return idGuid != Guid.Empty;
        }

        public bool isValidPost()
        {
            throw new NotImplementedException();
        }

        public bool isValidPut()
        {
            throw new NotImplementedException();
        }

        public bool isValidDelete()
        {
            throw new NotImplementedException();
        }
    }
}