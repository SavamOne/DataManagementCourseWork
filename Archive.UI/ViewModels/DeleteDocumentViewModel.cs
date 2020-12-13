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
    public class DeleteDocumentViewModel : TabElementViewModel
    {
        private readonly ArchiveService archiveService;
        public ICommand DeleteCommand { get; set; }

        public int DocumentNumber { get; set; }

        public DeleteDocumentViewModel(ArchiveService archiveService)
        {
            this.archiveService = archiveService;
            Name = "Удалить документ";
            DeleteCommand = new RelayCommand(Execute);
        }

        private async Task Execute(object obj)
        {
            try
            {
                await Task.Run(() => archiveService.DatabaseProcessor.DeleteDocument(DocumentNumber));

                MessageBox.Show("Документ удален успешно.");
                DocumentNumber = 0;
            }
            catch (Exception e)
            {
                MessageBox.Show("Документ удалить не удалось.", e.Message);
            }

        }
    }
}
