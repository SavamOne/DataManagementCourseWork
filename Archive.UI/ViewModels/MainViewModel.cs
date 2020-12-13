using System.Collections.ObjectModel;
using Archive.Service;

namespace Archive.UI.ViewModels
{
    public class MainViewModel : NotifyPropertyChanged
    {
        private TabElementViewModel selectedObject;
        public ObservableCollection<TabElementViewModel> Objects { get; set; }

        public TabElementViewModel SelectedObject
        {
            get => selectedObject;
            set
            {
                selectedObject = value;
                Notify();
            }

        }

        public MainViewModel(ArchiveService service)
        {
            Objects = new ObservableCollection<TabElementViewModel>
            {
                new FindDocumentByNameViewModel(service),
                new FindDocumentByThemeViewModel(service),
                new FindMostCountDocument(service),
                new LastRequestWorkerViewModel(service),
                new MostSearchableWorkerViewModel(service),
                new AddDocumentViewModel(service),
                new DeleteDocumentViewModel(service),
                new ChangeDepartmentPhoneViewModel(service),
            };

            //SelectedObject = Objects[0];
            //Notify(nameof(SelectedObject));
        }
    }
}
