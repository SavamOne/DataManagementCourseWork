using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Archive.Service;
using Archive.Service.Contracts;
using Archive.UI.ViewModels;

namespace Archive.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow mainWindow;
        protected override void OnStartup(StartupEventArgs e)
        {
            mainWindow = new MainWindow();

            var login = new LoginViewModel(Authorize, AuthorizationComplete)
            {
                Login = "postgres",
                Password = "postgres",
                Host = "localhost"
            };


            mainWindow.DataContext = login;
            mainWindow.Show();
        }

        private (bool, ArchiveService) Authorize(AuthData authData)
        {
            var result = ArchiveService.TryCreateService(authData, out var archiveService);
            return (result, archiveService);
        }

        private void AuthorizationComplete(ArchiveService service)
        {
            mainWindow.DataContext = new MainViewModel(service);
        }
    }
}
