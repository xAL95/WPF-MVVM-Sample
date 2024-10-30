using WPF_MVVM_Sample.ViewModel;

namespace WPF_MVVM_Sample
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            var mainViewModel = new MainViewModel();

            // Create the InsertUserViewModel with a reference to the ApplicationViewModel
            var insertUserrViewModel = new InsertUserViewModel(mainViewModel);

            // Set the DataContext for the Window
            DataContext = new { 
                MainViewModel = mainViewModel, 
                InsertUserViewModel = insertUserrViewModel 
            };
        }
    }
}