using System.Data;
using Archive.Contracts.Entities;
using Dapper;

namespace Archive.Repositories.Repositories.Implementations
{
    public class DocumentThemeRepository : RepositoryBase, IDocumentThemeRepository
    {
        public DocumentThemeRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public int GetIdOrCreate(Theme item)
        {
            var theme = Connection.QueryFirstOrDefault<Theme>("SELECT * FROM document_theme WHERE name=@DocumentThemeName", new
            {
                DocumentThemeName = item.Name
            });

            return theme?.Id ?? Create(item);
        }

        public Theme Get(int id)
        {
            return Connection.QueryFirstOrDefault<Theme>("SELECT * FROM document_theme WHERE id=@DocumentThemeId", new
            {
                DocumentThemeId = id
            });
        }

        public int Create(Theme item)
        {
            return Connection.QuerySingle<int>(
                "INSERT INTO document_theme (name) Values (@DocumentThemeName) RETURNING id",
                new
                {
                    DocumentThemeName = item.Name
                });
        }

        public void Update(Theme item)
        {
            Connection.Execute(
                "UPDATE document_theme SET name=@DocumentThemeName WHERE id = @DocumentId;",
                new
                {
                    DocumentId = item.Id,
                    DocumentThemeName = item.Name
                });
        }

        public void Delete(int id)
        {
            Connection.Execute("DELETE FROM document_theme WHERE id=@DocumentThemeId", new
            {
                DocumentThemeId = id
            });
        }
    }
}