using System.Data;
using Archive.Contracts.Entities;
using Dapper;

namespace Archive.Repositories.Repositories.Implementations
{
    public class CellRepository : RepositoryBase, ICellRepository
    {
        public CellRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public int GetIdOrCreate(Cell item)
        {
            var cell = Connection.QueryFirstOrDefault<Cell>("SELECT * FROM cell WHERE number=@Number AND shelf_id=@ShelfId", new
            {
                item.Number,
                item.ShelfId
            });

            return cell?.Id ?? Create(item);
        }

        public Cell Get(int id)
        {
            return Connection.QueryFirstOrDefault<Cell>("SELECT * FROM cell WHERE id=@DocumentId", new
            {
                Id = id
            });
        }

        public int Create(Cell item)
        {
            return Connection.QuerySingle<int>(
                "INSERT INTO cell (number, shelf_id) Values (@Number, @ShelfId) RETURNING id",
                new
                {
                    item.Number, item.ShelfId
                });
        }

        public void Update(Cell item)
        {
            Connection.Execute(
                "UPDATE cell SET number=@Number, shelf_id = @ShelfId WHERE id = @DocumentId;",
                new
                {
                    item.Number, item.ShelfId
                });
        }

        public void Delete(int id)
        {
            Connection.Execute("DELETE FROM cell WHERE id=@DocumentId", new
            {
                Id = id
            });
        }
    }
}