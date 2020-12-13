using System;
using System.Data;
using Archive.Repositories.Repositories;
using Archive.Repositories.Repositories.Implementations;

namespace Archive.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ICellRepository cellRepository;
        private readonly IDepartmentRepository departmentRepository;
        private readonly IDocumentRepository documentRepository;
        private readonly IDocumentThemeRepository documentThemeRepository;
        private readonly IRackRepository rackRepository;
        private readonly IRequestsRepository requestsRepository;
        private readonly IShelfRepository shelfRepository;
        private readonly IWorkerRepository workerRepository;
        private bool disposed;
        private IDbTransaction transaction;

        public UnitOfWork(IDbConnection connection)
        {
            transaction = connection.BeginTransaction();
            documentRepository = new DocumentRepository(transaction);
            documentThemeRepository = new DocumentThemeRepository(transaction);
            cellRepository = new CellRepository(transaction);
            shelfRepository = new ShelfRepository(transaction);
            rackRepository = new RackRepository(transaction);
            departmentRepository = new DepartmentRepository(transaction);
            workerRepository = new WorkerRepository(transaction);
            requestsRepository = new RequestRepository(transaction);
        }

        public void Dispose()
        {
            if (!disposed)
            {
                transaction.Dispose();
                transaction = null;

                disposed = true;
            }
        }

        public IDocumentRepository DocumentRepository =>
            !disposed ? documentRepository : throw new ObjectDisposedException("UnitOfWork disposed");

        public IDocumentThemeRepository DocumentThemeRepository => !disposed
            ? documentThemeRepository
            : throw new ObjectDisposedException("UnitOfWork disposed");

        public ICellRepository CellRepository =>
            !disposed ? cellRepository : throw new ObjectDisposedException("UnitOfWork disposed");

        public IRackRepository RackRepository =>
            !disposed ? rackRepository : throw new ObjectDisposedException("UnitOfWork disposed");

        public IShelfRepository ShelfRepository =>
            !disposed ? shelfRepository : throw new ObjectDisposedException("UnitOfWork disposed");

        public IDepartmentRepository DepartmentRepository =>
            !disposed ? departmentRepository : throw new ObjectDisposedException("UnitOfWork disposed");

        public IWorkerRepository WorkerRepository =>
            !disposed ? workerRepository : throw new ObjectDisposedException("UnitOfWork disposed");

        public IRequestsRepository RequestsRepository =>
            !disposed ? requestsRepository : throw new ObjectDisposedException("UnitOfWork disposed");

        public void Commit()
        {
            try
            {
                transaction?.Commit();
            }
            catch
            {
                transaction?.Rollback();
                throw;
            }
        }
    }
}