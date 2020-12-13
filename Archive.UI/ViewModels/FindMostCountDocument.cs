using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Archive.Service;

namespace Archive.UI.ViewModels
{
    public class FindMostCountDocument : TabElementViewModel
    {
        private readonly ArchiveService service;
        private int count;
        public ICommand FindCountCommand { get; set; }

        public ICommand FindSearchableCommand { get; set; }

        public int Count
        {
            get => count;
            set
            {
                count = value;
                Notify();
            }
        }

        public ObservableCollection<DocumentElementViewModel> Objects { get; set; }

        public FindMostCountDocument(ArchiveService service)
        {
            this.service = service;
            Name = "Найти документ в макс. кол-ве\nнаиболее требуемый";
            FindCountCommand = new RelayCommand(ExecuteCount);
            FindSearchableCommand = new RelayCommand(ExecuteSearhable);
            Objects = new ObservableCollection<DocumentElementViewModel>();
        }

        private async Task ExecuteSearhable(object arg)
        {
            var result = await Task.Run(() => service.DatabaseProcessor.GetMostSearchableDocument());

            Objects.Clear();
            Objects.Add(new DocumentElementViewModel
            {
                CellNumber = result.CellNumber,
                DocumentCount = result.Count,
                DocumentName = result.DocumentName,
                DocumentTheme = result.DocumentTheme,
                RackNumber = result.RackNumber,
                ReceiptDate = result.ReceiptDate,
                ShelfNumber = result.ShelfNumber
            });

            Count = result.SearchCount;
        }

        private async Task ExecuteCount(object obj)
        {
            var result = await Task.Run(() => service.DatabaseProcessor.GetMostCountDocument());

            Objects.Clear();
            if (result != null)
            {
                Objects.Add(new DocumentElementViewModel
                {
                    CellNumber = result.CellNumber,
                    DocumentCount = result.Count,
                    DocumentName = result.DocumentName,
                    DocumentTheme = result.DocumentTheme,
                    RackNumber = result.RackNumber,
                    ReceiptDate = result.ReceiptDate,
                    ShelfNumber = result.ShelfNumber
                });
            }

            Count = 0;
        }
    }
}
