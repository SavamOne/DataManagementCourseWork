using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using Archive.Service;

namespace Archive.UI.ViewModels
{
    public class FindDocumentByNameViewModel : TabElementViewModel
    {
        private readonly ArchiveService archiveService;
        public string Query { get; set; }

        public ICommand FindCommand { get; set; }

        public ObservableCollection<DocumentElementViewModel> Objects { get; set; }

        public FindDocumentByNameViewModel(ArchiveService archiveService)
        {
            this.archiveService = archiveService;
            Name = "Найти документ по имени";
            FindCommand = new RelayCommand(Execute);
            Objects = new ObservableCollection<DocumentElementViewModel>();
        }

        private async Task Execute(object obj)
        {
            var results = await Task.Run(() => archiveService.DatabaseProcessor.GetDocumentsByName(Query).ToList());

            Objects.Clear();
            results.ForEach(x => Objects.Add(new DocumentElementViewModel
            {
                CellNumber = x.CellNumber,
                DocumentCount = x.Count,
                DocumentName = x.DocumentName,
                DocumentTheme = x.DocumentTheme,
                RackNumber = x.RackNumber,
                ReceiptDate = x.ReceiptDate,
                ShelfNumber = x.ShelfNumber
            }));
        }
    }
}
