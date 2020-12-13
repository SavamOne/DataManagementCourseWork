using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Archive.Service;

namespace Archive.UI.ViewModels
{
    public class FindDocumentByThemeViewModel : TabElementViewModel
    {
        private readonly ArchiveService archiveService;
        public string Query { get; set; }

        public ICommand FindCommand { get; set; }

        public ObservableCollection<DocumentElementViewModel> Objects { get; set; }

        public FindDocumentByThemeViewModel(ArchiveService archiveService)
        {
            this.archiveService = archiveService;
            Name = "Найти документ по теме";
            FindCommand = new RelayCommand(Execute);
            Objects = new ObservableCollection<DocumentElementViewModel>();
        }

        private async Task Execute(object obj)
        {
            var results = await Task.Run(() => archiveService.DatabaseProcessor.GetDocumentsByTheme(Query).ToList());

            Objects.Clear();
            results.ForEach(x => Objects.Add(new DocumentElementViewModel
            {
                CellNumber = x.CellNumber,
                DocumentCount = x.Count,
                DocumentName = x.DocumentName,
                DocumentNumber = x.DocumentNumber,
                DocumentTheme = x.DocumentTheme,
                RackNumber = x.RackNumber,
                ReceiptDate = x.ReceiptDate,
                ShelfNumber = x.ShelfNumber
            }));
        }
    }
}
