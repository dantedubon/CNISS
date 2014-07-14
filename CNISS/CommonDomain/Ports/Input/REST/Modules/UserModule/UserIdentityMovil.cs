using System.Collections.Generic;
using CNISS.AutenticationDomain.Domain.Entities;
using Nancy.Security;

namespace CNISS.CommonDomain.Ports.Input.REST.Modules.UserModule
{
    public class UserIdentityMovil : IUserIdentity
    {
        public UserIdentityMovil(User user)
        {
            UserName = user.Id;
            Claims = new List<string> {"movil"};
        }
        public string UserName { get; protected set; }
        public IEnumerable<string> Claims { get; protected set; }
    }
}