using System.Configuration;
using System.IO;
using Nancy;

namespace CNISS.CommonDomain.Ports.Input.REST
{
    public class FileGetter:IFileGetter
    {

        private readonly IRootPathProvider _rootPath;
        private readonly string _root = ConfigurationManager.AppSettings["RootFiles"];
        public FileGetter(IRootPathProvider rootPath)
        {
            _rootPath = rootPath;
        }

        public bool existsFile(string directory, string name, string fileExtension)
        {
            var fileToSearch = Path.Combine(_root+ directory,  name + fileExtension);
            return File.Exists(fileToSearch);


        }

        public byte[] getFile(string directory, string name, string fileExtension)
        {
            var fileToSearch = Path.Combine(_root + directory, name + fileExtension);
            var file = File.ReadAllBytes(fileToSearch);
            return file;
        }
    }
}