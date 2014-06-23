namespace CNISS.CommonDomain.Ports.Input.REST
{
    public interface IFileGetter
    {
        bool existsFile(string directory, string name, string fileExtension);
        byte[] getFile(string directory, string name, string fileExtension);
    }
}