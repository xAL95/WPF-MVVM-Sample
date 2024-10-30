using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using WPF_MVVM_Sample.Model;
using WPF_MVVM_Sample.Services.Command;

namespace WPF_MVVM_Sample.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<User> Users { get; }

        private User? _selectedUser;

        public User? SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged();
            }
        }

        public ICommand DeleteUserCommand { get; }

        public MainViewModel()
        {
            Users = new ObservableCollection<User>();
            DeleteUserCommand = new RelayCommand(DeleteUser, CanDeleteUser);
        }

        public bool InsertUser(string username, string email)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email))
            {
                // Show error message
                MessageBox.Show("Username and Email cannot be empty.");
                return false;
            }

            foreach (var user in Users)
            {
                if (user.Username == username)
                {
                    // Show error message
                    MessageBox.Show("Username already exist.");
                    return false;
                }

                if (user.Email == email)
                {
                    // Show error message
                    MessageBox.Show("Email already exist.");
                    return false;
                }
            }

            // Simple email validation
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Invalid email format.");
                return false;
            }

            // Add a new User to the list
            Users.Add(new User { Username = username, Email = email });

            return true;
        }

        private void DeleteUser(object _)
        {
            if (SelectedUser == null)
                return;

            Users.Remove(SelectedUser);

            // Clear selection after delete
            SelectedUser = null;
        }

        private bool CanDeleteUser(object _) => SelectedUser != null;

    }
}
