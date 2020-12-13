using System.Data;
using Archive.Contracts.Entities;
using Dapper;

namespace Archive.Repositories.Repositories.Implementations
{
    public class ShelfRepository : RepositoryBase, IShelfRepository
    {
        public ShelfRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public int GetIdOrCreate(Shelf item)
        {
            var shelf = Connection.QueryFirstOrDefault<Shelf>("SELECT * FROM shelf WHERE number=@Number AND rack_id=@RackId", new
            {
                item.Number,
                item.RackId
            });

            return shelf?.Id ?? Create(item);
        }

        public Shelf Get(int id)
        {
            return Connection.QueryFirstOrDefault<Shelf>("SELECT * FROM shelf WHERE id=@DocumentId", new
            {
                Id = id
            });
        }

        public int Create(Shelf item)
        {
            return Connection.QuerySingle<int>(
                "INSERT INTO shelf (number, rack_id) Values (@Number, @RackId) RETURNING id",
                new
                {
                    item.Number, item.RackId
                });
        }

        public void Update(Shelf item)
        {
            Connection.Execute(
                "UPDATE shelf SET number=@Number, rack_id = @RackId WHERE id = @DocumentId;",
                new
                {
                    item.Number, item.RackId
                });
        }

        public void Delete(int id)
        {
            Connection.Execute("DELETE FROM shelf WHERE id=@DocumentId", new
            {
                Id = id
            });
        }
    }
}