using System;
using System.Collections.Generic;
using System.Text;

namespace Archive.UI.ViewModels
{
    public class DocumentElementViewModel
    {
        public string DocumentName { get; set; }

        public string DocumentTheme { get; set; }

        public int DocumentCount { get; set; }

        public int DocumentNumber { get; set; }

        public DateTimeOffset ReceiptDate { get; set; }

        public int CellNumber { get; set; }

        public int ShelfNumber { get; set; }

        public int RackNumber { get; set; }
    }
}
