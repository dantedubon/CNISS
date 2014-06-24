using Nancy;

namespace CNISS.CommonDomain.Ports.Input.REST
{
    public interface IFilePersister
    {
        void saveFile(string directory, HttpFile file, string fileExtension, string name);
    }
}
