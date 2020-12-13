using Archive.Contracts.Entities;

namespace Archive.Repositories.Repositories
{
    public interface ICellRepository : IRepository<Cell>
    {
        int GetIdOrCreate(Cell item);
    }
}