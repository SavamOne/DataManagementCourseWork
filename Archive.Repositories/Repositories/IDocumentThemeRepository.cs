using Archive.Contracts.Entities;

namespace Archive.Repositories.Repositories
{
    public interface IDocumentThemeRepository : IRepository<Theme>
    {
        int GetIdOrCreate(Theme item);
    }
}