using System;
using System.Collections.Generic;
using System.Data;
using Archive.Contracts.Entities;
using Dapper;

namespace Archive.Repositories.Repositories.Implementations
{
    public class WorkerRepository : RepositoryBase, IWorkerRepository
    {
        public Worker Get(int id)
        {
            return Connection.QuerySingle<Worker>("SELECT * FROM workers WHERE id=@DocumentId", new
            {
                Id = id
            });
        }

        public int Create(Worker item)
        {
            throw new NotSupportedException();
        }

        public void Update(Worker item)
        {
            throw new NotSupportedException();
        }

        public void Delete(int id)
        {
            throw new NotSupportedException();
        }

        public WorkerDepartmentProjection GetByUsername(string name)
        {
            return Connection.QuerySingle<WorkerDepartmentProjection>("select "+
                "w.id worker_id, "+
                "w.name worker_name, " +
                "d.number department_number, " +
                "d.name department_name, " +
                "d.phone department_phone, " +
                "u.usesysid pg_user_id, " +
                "u.usename pg_user_name " +
                "from workers w " +
                "join department d on w.department_id = d.id " +
                "join pg_catalog.pg_user u on u.usesysid = w.pg_user_id " +
                "where u.usename = @Name", new
            {
                Name = name
            });
        }

        public IEnumerable<WorkerCountProjection> GetMostCountWorker()
        {
            return Connection.Query<WorkerCountProjection>("select " +
                                                           "w.name worker_name, " +
                                                           "count(*) requests_count, " +
                                                           "d.number department_number, " +
                                                           "d.name department_name, " +
                                                           "d.phone department_phone " +
                                                           "from workers w " +
                                                           "join department d on w.department_id = d.id " +
                                                           "join requests r on w.id = r.worker_id " +
                                                           "group by (w.id, d.number, d.name, d.phone)" +
                                                           "LIMIT 10");
        }

        public WorkerDateProjection GetLastRequestWorker(string documentName)
        {
            return Connection.QuerySingleOrDefault<WorkerDateProjection>("select " +
                                                               "w.name worker_name, " +
                                                               "r.request_date request_date, " +
                                                               "d.number department_number, " +
                                                               "d.name department_name, " +
                                                               "d.phone department_phone " +
                                                               "from workers w " +
                                                               "join department d on w.department_id = d.id " +
                                                               "join requests r on w.id = r.worker_id " +
                                                               "join document d2 on r.document_id = d2.id " +
                                                               "where d2.name LIKE @DocumentName " +
                                                               "order by r.request_date desc " +
                                                               "LIMIT 1",
                new
                {
                    DocumentName = documentName
                });
        }

        public WorkerRepository(IDbTransaction transaction) : base(transaction)
        {
        }
    }
}
