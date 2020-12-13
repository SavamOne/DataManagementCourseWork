using System;

namespace Archive.Contracts.Entities
{
    public class DocumentProjection
    {
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }

        public string DocumentTheme { get; set; }

        public int DocumentNumber { get; set; }

        public DateTimeOffset ReceiptDate { get; set; }

        public int Count { get; set; }

        public int CellNumber { get; set; }

        public int ShelfNumber { get; set; }
        public int RackNumber { get; set; }
    }
}