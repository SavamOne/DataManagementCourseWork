using Archive.Contracts.Entities;

namespace Archive.Repositories.Repositories
{
    public interface IRackRepository : IRepository<Rack>
    {
        int GetIdOrCreate(Rack item);
    }
}