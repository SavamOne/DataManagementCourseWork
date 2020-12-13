using System;

namespace Archive.Contracts.Entities
{
    public class Document
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ThemeId { get; set; }

        public int Number { get; set; }

        public DateTimeOffset ReceiptDate { get; set; }

        public int Count { get; set; }

        public int CellId { get; set; }
    }
}