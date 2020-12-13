using System;
using System.Data;
using Archive.Contracts.Entities;
using Dapper;

namespace Archive.Repositories.Repositories.Implementations
{
    public class DepartmentRepository : RepositoryBase, IDepartmentRepository
    {
        public DepartmentRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public Department Get(int id)
        {
            return Connection.QuerySingle<Department>("SELECT * FROM department WHERE id=@DepartmentId", new
            {
                DepartmentId = id
            });
        }

        public int Create(Department item)
        {
            throw new NotSupportedException();
        }

        public void Update(Department item)
        {
            Connection.Execute(
                "UPDATE department SET phone=@Phone WHERE number = @DepartmentNumber;",
                new
                {
                    DepartmentNumber = item.Number,
                    item.Phone,
                });
        }

        public void Delete(int id)
        {
            throw new NotSupportedException();
        }
    }
}
