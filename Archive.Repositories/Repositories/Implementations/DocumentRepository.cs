using System;
using System.Collections.Generic;
using System.Data;
using Archive.Contracts.Entities;
using Dapper;

namespace Archive.Repositories.Repositories.Implementations
{
    public class DocumentRepository : RepositoryBase, IDocumentRepository
    {
        public DocumentRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public int Create(Document item)
        {
            return Connection.QuerySingle<int>(
                "INSERT INTO document (name, theme_id, number, receipt_date, count, cell_id) Values (@DocumentName, @ThemeId, @Number, @ReceiptDate, @Count, @CellId) RETURNING id;",
                new
                {
                    DocumentName = item.Name,
                    item.ThemeId,
                    item.ReceiptDate,
                    item.Count,
                    item.CellId,
                    item.Number
                });
        }

        public void Update(Document item)
        {
            Connection.Execute(
                "UPDATE document SET name=@DocumentName, theme_id=@ThemeId, number=@Number, receipt_date=@ReceiptDate, count=@Count, cell_id=@CellId WHERE id = @DocumentId;",
                new
                {
                    DocumentId = item.Id,
                    item.Name,
                    item.ThemeId,
                    item.ReceiptDate,
                    item.Count,
                    item.CellId,
                    item.Number
                });
        }

        public void Delete(int number)
        {
            Connection.Execute("DELETE FROM document WHERE number=@DocumentNumber", new
            {
                DocumentNumber = number
            });
        }

        public DocumentProjection GetMostCountDocument()
        {
            return Connection.QuerySingleOrDefault<DocumentProjection>("SELECT " + 
                                                        "d.id as document_id, " +
                                                        "d.name as document_name, " +
                                                        "d.count count, " +
                                                        "d.receipt_date receipt_date, " +
                                                        "d.number document_number, " +
                                                        "dt.name document_theme, " +
                                                        "c.number cell_number, " +
                                                        "s.number shelf_number, " +
                                                        "r.number rack_number " +
                                                        "FROM document as d " +
                                                        "JOIN cell c on c.id = d.cell_id " +
                                                        "JOIN document_theme dt on dt.id = d.theme_id " +
                                                        "JOIN shelf s on s.id = c.shelf_id " +
                                                        "JOIN rack r on r.id = s.rack_id " +
                                                        "ORDER BY d.count DESC " +
                                                        "LIMIT 1");
        }

        public DocumentProjectionCount GetMostSearchableDocument()
        {
            return Connection.QuerySingleOrDefault<DocumentProjectionCount>("SELECT " +
                                                                       "count(*) as search_count, " +
                                                                       "d.id as document_id, " +
                                                                       "d.name as document_name, " +
                                                                       "d.count count, " +
                                                                       "d.receipt_date receipt_date, " +
                                                                       "d.number document_number, " +
                                                                       "dt.name document_theme, " +
                                                                       "c.number cell_number, " +
                                                                       "s.number shelf_number, " +
                                                                       "r.number rack_number " +
                                                                       "FROM document as d " +
                                                                       "JOIN cell c on c.id = d.cell_id " +
                                                                       "JOIN document_theme dt on dt.id = d.theme_id " +
                                                                       "JOIN shelf s on s.id = c.shelf_id " +
                                                                       "JOIN rack r on r.id = s.rack_id " +
                                                                       "JOIN requests r2 on d.id = r2.document_id " +
                                                                       "GROUP BY (d.id, dt.name, c.number, s.number, r.number) " +
                                                                       "order by count desc " +
                                                                       "LIMIT 1");
        }



        public Document Get(int id)
        {
            throw new NotSupportedException();
        }

        public IEnumerable<DocumentProjection> GetDocumentsByName(string name)
        {
            return Connection.Query<DocumentProjection>("SELECT " +
                                                        "d.id as document_id, " +
                                                        "d.name as document_name, " +
                                                        "d.count document_count, " +
                                                        "d.receipt_date receipt_date, " +
                                                        "d.number document_number, " +
                                                        "dt.name document_theme, " +
                                                        "c.number cell_number, " +
                                                        "s.number shelf_number, " +
                                                        "r.number rack_number " +
                                                        "FROM document as d " +
                                                        "JOIN cell c on c.id = d.cell_id " +
                                                        "JOIN document_theme dt on dt.id = d.theme_id " +
                                                        "JOIN shelf s on s.id = c.shelf_id " +
                                                        "JOIN rack r on r.id = s.rack_id " +
                                                        "WHERE UPPER(d.name) LIKE CONCAT('%',UPPER(@DocumentName),'%');",
                new
                {
                    DocumentName = name
                });
        }

        public IEnumerable<DocumentProjection> GetDocumentsByTheme(string themeName)
        {
            return Connection.Query<DocumentProjection>("SELECT " +
                                                        "d.id as document_id, " +
                                                        "d.name as document_name, " +
                                                        "d.count count, " +
                                                        "d.receipt_date receipt_date, " +
                                                        "d.number document_number, " +
                                                        "dt.name document_theme, " +
                                                        "c.number cell_number, " +
                                                        "s.number shelf_number, " +
                                                        "r.number rack_number " +
                                                        "FROM document as d " +
                                                        "JOIN cell c on c.id = d.cell_id " +
                                                        "JOIN document_theme dt on dt.id = d.theme_id " +
                                                        "JOIN shelf s on s.id = c.shelf_id " +
                                                        "JOIN rack r on r.id = s.rack_id " +
                                                        "WHERE UPPER(dt.name) LIKE CONCAT('%',UPPER(@DocumentTheme),'%');",
                new
                {
                    DocumentTheme = themeName
                });
        }
    }
}