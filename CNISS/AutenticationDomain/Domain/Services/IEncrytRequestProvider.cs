using System;

namespace CNISS.AutenticationDomain.Domain.Services
{
    public interface IEncrytRequestProvider
    {
        string encryptString(String plainMessage);
        string decryptString(String encryptedMessage);
    }
}