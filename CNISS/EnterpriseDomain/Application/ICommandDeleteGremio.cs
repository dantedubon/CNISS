using CNISS.EnterpriseDomain.Domain;

namespace CNISS.EnterpriseDomain.Application
{
    public interface ICommandDeleteGremio
    {
        bool isExecutable(RTN rtn);
        void execute(RTN rtn);
    }
}