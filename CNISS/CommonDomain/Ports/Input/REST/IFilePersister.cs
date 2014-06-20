using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;
using Nancy.Hosting.Aspnet;

namespace CNISS.CommonDomain.Ports.Input.REST
{
    public interface IFilePersister
    {
        void saveFile(string directory, HttpFile file, string fileExtension, string name);
    }
}
