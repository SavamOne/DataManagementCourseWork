using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Archive.Contracts.Entities;
using Archive.Service;

namespace Archive.UI.ViewModels
{
    public class ChangeDepartmentPhoneViewModel : TabElementViewModel
    {
        private readonly ArchiveService archiveService;
        public ICommand ChangeCommand { get; set; }

        public string NewPhone { get; set; }

        public int DepartmentNumber { get; set; }

        public ChangeDepartmentPhoneViewModel(ArchiveService archiveService)
        {
            this.archiveService = archiveService;
            Name = "Изменить телефон отдела";
            ChangeCommand = new RelayCommand(Execute);
        }

        private async Task Execute(object obj)
        {
            try
            {
                await Task.Run(() => archiveService.DatabaseProcessor.UpdateDepartmentPhone(new Department
                {
                    Number = DepartmentNumber,
                    Phone = NewPhone
                }));

                MessageBox.Show("Телефон изменен успешно.");
                DepartmentNumber = 0;
                NewPhone = string.Empty;
            }
            catch (Exception e)
            {
                MessageBox.Show("Телефон поменять не удалось.", e.Message);
            }

        }
    }
}
