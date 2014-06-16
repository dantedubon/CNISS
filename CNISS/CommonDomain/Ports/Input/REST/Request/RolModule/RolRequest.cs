using System;

namespace CNISS.CommonDomain.Ports.Input.REST.Request.RolModule
{
    public class RolRequest:IValidPost
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

      
    }
}