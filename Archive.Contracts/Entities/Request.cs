using System;

namespace Archive.Contracts.Entities
{
    public class Request
    {
        public int Id { get; set; }

        public int WorkerId { get; set; }

        public int DocumentId { get; set; }

        public DateTimeOffset RequestDate { get; set; }
    }
}
