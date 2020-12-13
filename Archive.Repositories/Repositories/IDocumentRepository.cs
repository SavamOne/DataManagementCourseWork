using System.Collections.Generic;
using Archive.Contracts.Entities;

namespace Archive.Repositories.Repositories
{
    public interface IDocumentRepository : IRepository<Document>
    {
        DocumentProjection GetMostCountDocument();

        DocumentProjectionCount GetMostSearchableDocument();

        IEnumerable<DocumentProjection> GetDocumentsByName(string name);

        IEnumerable<DocumentProjection> GetDocumentsByTheme(string themeName);
    }
}