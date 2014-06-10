using System;
using CNISS.CommonDomain.Ports.Input.REST.Request.RolModule;

namespace CNISS.CommonDomain.Ports.Input.REST.Request.UserRequest
{
    public class UserRequest:IValidRequest
    {
        public  String Id { get; set; }
        public  String firstName { get; set; }
        public  String mail { get; set; }
        public  String secondName { get; set; }
        public  String password { get; set; }
        public  RolRequest userRol { get; set; }
        public bool isValidPost()
        {
            if (userRol != null)
            {
                var isValidRol = userRol.isValidGet();
                return !String.IsNullOrEmpty(Id) 
                       && !String.IsNullOrEmpty(firstName)
                       && !String.IsNullOrEmpty(secondName)
                       && !String.IsNullOrEmpty(password)
                       && !String.IsNullOrEmpty(mail)
                       && isValidRol;
            }
            
            return false;

        }

        public bool isValidPut()
        {
            throw new NotImplementedException();
        }

        public bool isValidDelete()
        {
            return !String.IsNullOrEmpty(Id);
        }

        public bool isValidGet()
        {
            throw new NotImplementedException();
        }
    }
}