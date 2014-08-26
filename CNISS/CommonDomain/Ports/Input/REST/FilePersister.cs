using System.Configuration;
using System.IO;
using Nancy;

namespace CNISS.CommonDomain.Ports.Input.REST
{
    public class FilePersister:IFilePersister
    {
        private readonly IRootPathProvider _rootPath;
        private readonly string _root = ConfigurationManager.AppSettings["RootFiles"];


        public FilePersister(IRootPathProvider rootPath)
        {
            _rootPath = rootPath;
        }

        public void saveFile(string directory, HttpFile file, string fileExtension, string fileName)
        {
            var directoryPath = _root + directory;
            ensureDirectory(directoryPath);
            var fileNameWithPath = Path.Combine(directoryPath, fileName + fileExtension);
            writeFile(fileNameWithPath,file);

        }

        private void writeFile(string fileName, HttpFile file)
        {
            using (var fileStream = new FileStream(fileName,FileMode.Create))
            {
                file.Value.CopyTo(fileStream);
               
            }
        }
        private void ensureDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }
    }
}