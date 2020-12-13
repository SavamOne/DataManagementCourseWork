using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Archive.Contracts.Entities;
using Archive.Service;

namespace Archive.UI.ViewModels
{
    public class AddDocumentViewModel : TabElementViewModel
    {
        private readonly ArchiveService service;
        private string documentName;
        private string documentTheme;
        private int documentCount;
        private int documentNumber;
        private int cellNumber;
        private int shelfNumber;
        private int rackNumber;

        public ICommand AddDocument { get; set; }

        public AddDocumentViewModel(ArchiveService service)
        {
            Name = "Добавить документ";
            this.service = service;

            AddDocument = new RelayCommand(AddDocumentCommand, CanExecuteAddDocument);
        }

        private bool CanExecuteAddDocument(object arg)
        {
            return !string.IsNullOrEmpty(DocumentName) && !string.IsNullOrEmpty(DocumentTheme) && DocumentNumber != 0 &&
                   DocumentCount != 0 && CellNumber != 0 && ShelfNumber != 0 && RackNumber != 0;
        }

        private async Task AddDocumentCommand(object arg)
        {
            try
            {
                await Task.Run(() => service.DatabaseProcessor.AddDocument(new DocumentProjection
                {
                    CellNumber = CellNumber,
                    Count = DocumentCount,
                    DocumentName = DocumentName,
                    DocumentNumber = DocumentNumber,
                    DocumentTheme = DocumentTheme,
                    RackNumber = RackNumber,
                    ShelfNumber = ShelfNumber,
                    ReceiptDate = DateTimeOffset.Now
                }));

                MessageBox.Show("Документ успешно добавлен.");
                ClearAll();
            }
            catch(Exception e)
            {
                MessageBox.Show("Произошла ошибка при добавлении документа", e.Message);
            }
        }

        private void ClearAll()
        {
            DocumentName = DocumentTheme = string.Empty;
            DocumentCount = DocumentNumber = CellNumber = ShelfNumber = RackNumber = 0;
        }

        public string DocumentName
        {
            get => documentName;
            set
            {
                documentName = value;
                Notify();
            }
        }

        public string DocumentTheme
        {
            get => documentTheme;
            set
            {
                documentTheme = value;
                Notify();
            }
        }

        public int DocumentCount
        {
            get => documentCount;
            set
            {
                documentCount = value;
                Notify();
            }
        }

        public int DocumentNumber
        {
            get => documentNumber;
            set
            {
                documentNumber = value;
                Notify();
            }
        }

        public int CellNumber
        {
            get => cellNumber;
            set
            {
                cellNumber = value;
                Notify();
            }
        }

        public int ShelfNumber
        {
            get => shelfNumber;
            set
            {
                shelfNumber = value;
                Notify();
            }
        }

        public int RackNumber
        {
            get => rackNumber;
            set
            { 
                rackNumber = value; 
                Notify();
            }
        }
    }
}
