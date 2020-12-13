using System.Collections.Generic;
using Archive.Contracts.Entities;

namespace Archive.Repositories.Repositories
{
    public interface IWorkerRepository : IRepository<Worker>
    {
        WorkerDepartmentProjection GetByUsername(string name);

        IEnumerable<WorkerCountProjection> GetMostCountWorker();

        WorkerDateProjection GetLastRequestWorker(string documentName);
    }
}
