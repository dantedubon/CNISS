using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CNISS.AutenticationDomain.Domain.Entities;
using Nancy.Security;

namespace CNISS.AutenticationDomain.Domain.Services
{
    public interface IAuthenticateUser
    {
        bool isValidUser(User user, int nivel);
    }
}
