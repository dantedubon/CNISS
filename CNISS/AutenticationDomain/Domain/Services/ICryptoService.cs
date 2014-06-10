namespace CNISS.AutenticationDomain.Domain.Services
{
    public interface ICryptoService
    {
        
        string getEncryptedText(string text);
        string getKey();
    }
}