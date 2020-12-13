using System;
using Archive.Repositories.Repositories;

namespace Archive.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        public IDocumentRepository DocumentRepository { get; }
        public IDocumentThemeRepository DocumentThemeRepository { get; }

        public ICellRepository CellRepository { get; }

        public IRackRepository RackRepository { get; }

        public IShelfRepository ShelfRepository { get; }

        public IDepartmentRepository DepartmentRepository { get; }

        public IWorkerRepository WorkerRepository { get; }

        public IRequestsRepository RequestsRepository { get; }

        void Commit();
    }
}