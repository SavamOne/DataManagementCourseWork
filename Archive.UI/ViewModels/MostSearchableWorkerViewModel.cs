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
    public class MostSearchableWorkerViewModel : TabElementViewModel
    {
        private readonly ArchiveService archiveService;

        public ICommand FindCommand { get; set; }

        public ObservableCollection<WorkerCountProjection> Objects { get; set; }

        public MostSearchableWorkerViewModel(ArchiveService archiveService)
        {
            this.archiveService = archiveService;
            Name = "Найти работника,\nнаиболее часто\nобращающихся к архиву";
            FindCommand = new RelayCommand(Execute);
            Objects = new ObservableCollection<WorkerCountProjection>();
        }

        private async Task Execute(object obj)
        {
            var results = await Task.Run(() => archiveService.DatabaseProcessor.GetMostCountWorker().ToList());

            Objects.Clear();
            results.ForEach(x => Objects.Add(x));
        }
    }
}
