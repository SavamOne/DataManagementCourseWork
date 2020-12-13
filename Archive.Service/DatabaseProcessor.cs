using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Archive.Contracts.Entities;
using Archive.Repositories;

namespace Archive.Service
{
    public class DatabaseProcessor
    {
        private readonly DbContext dbContext;

        public DatabaseProcessor(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IEnumerable<DocumentProjection> GetDocumentsByName(string name)
        {
            using var uow = new UnitOfWork(dbContext.Connection);
            var documents = uow.DocumentRepository.GetDocumentsByName(name).ToList();
            if (!documents.Any())
            {
                return Enumerable.Empty<DocumentProjection>();
            }

            var worker = uow.WorkerRepository.GetByUsername(dbContext.Login);

            uow.RequestsRepository.Create(documents.Select(x => new Request
            {
                DocumentId = x.DocumentId,
                WorkerId = worker.WorkerId,
                RequestDate = DateTimeOffset.Now
            }));
            uow.Commit();
            return documents;
        }

        public IEnumerable<DocumentProjection> GetDocumentsByTheme(string themeName)
        {
            using var uow = new UnitOfWork(dbContext.Connection);
            var documents = uow.DocumentRepository.GetDocumentsByTheme(themeName).ToList();
            if (!documents.Any())
            {
                return Enumerable.Empty<DocumentProjection>();
            }

            var worker = uow.WorkerRepository.GetByUsername(dbContext.Login);

            uow.RequestsRepository.Create(documents.Select(x => new Request
            {
                DocumentId = x.DocumentId,
                WorkerId = worker.WorkerId,
                RequestDate = DateTimeOffset.Now
            }));
            uow.Commit();
            return documents;
        }

        public DocumentProjectionCount GetMostSearchableDocument()
        {
            using var uow = new UnitOfWork(dbContext.Connection);
            return uow.DocumentRepository.GetMostSearchableDocument();
        }

        public DocumentProjection GetMostCountDocument()
        {
            using var uow = new UnitOfWork(dbContext.Connection);
            return uow.DocumentRepository.GetMostCountDocument();
        }

        public void AddDocument(DocumentProjection item)
        {
            using var uow = new UnitOfWork(dbContext.Connection);

            int rackId = uow.RackRepository.GetIdOrCreate(new Rack { Number = item.RackNumber });
            int shelfId = uow.ShelfRepository.GetIdOrCreate(new Shelf { Number = item.ShelfNumber, RackId = rackId });
            int cellId = uow.CellRepository.GetIdOrCreate(new Cell { Number = item.CellNumber, ShelfId = shelfId });
            int themeId = uow.DocumentThemeRepository.GetIdOrCreate(new Theme { Name = item.DocumentTheme });
            uow.DocumentRepository.Create(new Document
            {
                CellId = cellId,
                Count = item.Count,
                Name = item.DocumentName,
                Number = item.DocumentNumber,
                ReceiptDate = item.ReceiptDate,
                ThemeId = themeId
            });

            uow.Commit();
        }

        public void DeleteDocument(int number)
        {
            using var uow = new UnitOfWork(dbContext.Connection);
            uow.RequestsRepository.Delete(number);
            uow.DocumentRepository.Delete(number);
            uow.Commit();
        }

        public void UpdateDepartmentPhone(Department department)
        {
            using var uow = new UnitOfWork(dbContext.Connection);
            uow.DepartmentRepository.Update(department);
            uow.Commit();
        }

        public IEnumerable<WorkerCountProjection> GetMostCountWorker()
        {
            using var uow = new UnitOfWork(dbContext.Connection);
            var workers = uow.WorkerRepository.GetMostCountWorker();
            return workers;
        }

        public WorkerDateProjection GetLastRequestWorker(string documentName)
        {
            using var uow = new UnitOfWork(dbContext.Connection);
            var worker = uow.WorkerRepository.GetLastRequestWorker(documentName);
            return worker;
        }
    }
}
