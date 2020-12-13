using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Archive.Contracts.Entities;
using Archive.Service;

namespace Archive.UI.ViewModels
{
    public class LastRequestWorkerViewModel : TabElementViewModel
    {
        private readonly ArchiveService archiveService;
        public string Query { get; set; }

        public ICommand FindCommand { get; set; }

        public ObservableCollection<WorkerDateProjection> Objects { get; set; }

        public LastRequestWorkerViewModel(ArchiveService archiveService)
        {
            this.archiveService = archiveService;
            Name = "Найти работника,\nобращавшийся последним\nк документу";
            FindCommand = new RelayCommand(Execute);
            Objects = new ObservableCollection<WorkerDateProjection>();
        }

        private async Task Execute(object obj)
        {
            var result = await Task.Run(() => archiveService.DatabaseProcessor.GetLastRequestWorker(Query));
            
            Objects.Clear();

            if (result != null)
            {
                Objects.Add(result);
            }
        }
    }
}
