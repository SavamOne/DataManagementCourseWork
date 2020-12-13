using System.Data;
using Archive.Contracts.Entities;
using Dapper;

namespace Archive.Repositories.Repositories.Implementations
{
    public class RackRepository : RepositoryBase, IRackRepository
    {
        public RackRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public int GetIdOrCreate(Rack item)
        {
            var rack = Connection.QueryFirstOrDefault<Rack>("SELECT * FROM rack WHERE number=@Number", new
            {
                item.Number
            });

            return rack?.Id ?? Create(item);
        }

        public Rack Get(int id)
        {
            return Connection.QueryFirstOrDefault<Rack>("SELECT * FROM rack WHERE id=@DocumentId", new
            {
                Id = id
            });
        }

        public int Create(Rack item)
        {
            return Connection.QuerySingle<int>(
                "INSERT INTO rack (number) Values (@Number) RETURNING id",
                new
                {
                    item.Number
                });
        }

        public void Update(Rack item)
        {
            Connection.Execute(
                "UPDATE rack SET number=@Number WHERE id = @DocumentId;",
                new
                {
                    item.Number
                });
        }

        public void Delete(int id)
        {
            Connection.Execute("DELETE FROM rack WHERE id=@DocumentId", new
            {
                Id = id
            });
        }
    }
}