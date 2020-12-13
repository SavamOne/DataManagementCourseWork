using Archive.Contracts.Entities;

namespace Archive.Repositories.Repositories
{
    public interface IShelfRepository : IRepository<Shelf>
    {
        int GetIdOrCreate(Shelf item);
    }
}