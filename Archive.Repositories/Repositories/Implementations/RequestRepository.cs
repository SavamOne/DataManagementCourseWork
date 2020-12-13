using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Archive.Contracts.Entities;
using Dapper;

namespace Archive.Repositories.Repositories.Implementations
{
    public class RequestRepository : RepositoryBase, IRequestsRepository
    {
        public Request Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public int Create(Request item)
        {
            return Connection.QuerySingle<int>(
                "INSERT INTO requests (worker_id, document_id, request_date) Values (@WorkerId, @DocumentId, @RequestDate) RETURNING id;",
                new
                {
                    item.WorkerId,
                    item.DocumentId,
                    item.RequestDate,
                });
        }

        public void Update(Request item)
        {
            throw new NotSupportedException();
        }

        public void Delete(int id)
        {
            Connection.Execute(
                "DELETE FROM requests r using document d where d.id = r.document_id and d.number = @Number",
                new
                {
                    Number = id
                });
        }

        public void Create(IEnumerable<Request> requests)
        {
            Connection.Execute(
                "INSERT INTO requests (worker_id, document_id, request_date) Values (@WorkerId, @DocumentId, @RequestDate)",
                requests.Select(x => new
                {
                    x.WorkerId,
                    x.DocumentId,
                    x.RequestDate
                }).ToArray());
        }

        public RequestRepository(IDbTransaction transaction) : base(transaction)
        {
        }
    }
}
