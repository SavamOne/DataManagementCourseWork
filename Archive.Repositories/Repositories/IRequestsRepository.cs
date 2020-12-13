using System.Collections.Generic;
using Archive.Contracts.Entities;

namespace Archive.Repositories.Repositories
{
    public interface IRequestsRepository : IRepository<Request>
    {
        void Create(IEnumerable<Request> requests);
    }
}
