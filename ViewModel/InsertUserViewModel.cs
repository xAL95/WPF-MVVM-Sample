using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WPF_MVVM_Sample.Model;
using WPF_MVVM_Sample.Services.Command;

namespace WPF_MVVM_Sample.ViewModel
{
    public class InsertUserViewModel : INotifyPropertyChanged
    {
        private readonly MainViewModel _mainViewModel;

        private string _username = string.Empty;
        private string _email = string.Empty;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public ICommand InsertUserCommand { get; }

        public InsertUserViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

            InsertUserCommand = new RelayCommand(InsertUser);
        }

        private void InsertUser(object _)
        {
            bool success = _mainViewModel.InsertUser(Username, Email);

            if (!success)
            {
                return;
            }

            // Optionally, clear the input fields
            Username = string.Empty;
            Email = string.Empty;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
