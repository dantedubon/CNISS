using System.Collections.Generic;
using Nancy.Security;

namespace CNISS_Tests
{
    public class DummyUserIdentityMovil:IUserIdentity
    {
        public DummyUserIdentityMovil(string userName)
        {
            UserName = userName;
            Claims = new List<string>() {"movil"};
        }

        public string UserName { get; private set; }
        public IEnumerable<string> Claims { get; private set; }
    }
}