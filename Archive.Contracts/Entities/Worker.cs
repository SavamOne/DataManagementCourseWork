namespace Archive.Contracts.Entities
{
    public class Worker
    {
        public int Id { get; set; }

        public int DepartmentId { get; set; }

        public string Name { get; set; }

        public int PgUserId { get; set; }
    }
}
