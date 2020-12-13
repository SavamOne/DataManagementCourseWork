using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Archive.Service;
using Archive.Service.Contracts;

namespace Archive.UI.ViewModels
{
    public class LoginViewModel : NotifyPropertyChanged
    {
        public delegate void AuthorizationHandler(ArchiveService service);

        private readonly Func<AuthData, (bool, ArchiveService)> authAction;
        private readonly Action<ArchiveService> authCompletedAction;

        private string host;
        private string login;
        private string password;
        private bool hasError;

        public LoginViewModel(Func<AuthData, (bool, ArchiveService)> authAction, Action<ArchiveService> authCompletedAction)
        {
            this.authAction = authAction;
            this.authCompletedAction = authCompletedAction;

            AuthCommand = new RelayCommand(Authorize, CanExecuteAuthorize);
        }

        public ICommand AuthCommand { get; }

        public bool HasError
        {
            get => hasError;
            set
            {
                hasError = value;
                Notify();
            }
        }

        public string Host
        {
            get => host;
            set
            {
                host = value;
                HasError = false;
                Notify();
            }
        }

        public string Login
        {
            get => login;
            set
            {
                login = value;
                HasError = false;
                Notify();
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                HasError = false;
                Notify();
            }
        }

        private bool CanExecuteAuthorize(object arg)
        {
            return !string.IsNullOrEmpty(Host) && !string.IsNullOrEmpty(Login) &&
                   !string.IsNullOrEmpty(Password);
        }

        private async Task Authorize(object obj)
        {
            var (server, port) = ParseHost();
            var authData = new AuthData
            {
                Login = Login,
                Password = Password,
                Host = server,
                Port = port
            };

            var (authResult, service) = await Task.Run(() => authAction(authData));

            HasError = !authResult;

            if (authResult)
            {
                authCompletedAction(service);
            }
        }

        private (string, ushort) ParseHost()
        {
            var array = Host.Split(':');

            if (array.Length == 1)
            {
                return (host, 5432);
            }

            if (array.Length == 2)
            {
                if (ushort.TryParse(array[1], out var port))
                {
                    return (array[0], port);
                }
            }


            return (string.Empty, 0);
        }
    }
}