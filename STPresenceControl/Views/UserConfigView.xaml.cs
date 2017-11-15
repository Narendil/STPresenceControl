using STPresenceControl.Contracts;
using System.Windows;
using System.Windows.Controls;

namespace STPresenceControl.Views
{
    /// <summary>
    /// Interaction logic for UserConfigView.xaml
    /// </summary>
    public partial class UserConfigView : UserControl
    {
        IPasswordHandler ViewModel { get { return DataContext as IPasswordHandler; } }

        public UserConfigView()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanging;
        }

        private void OnDataContextChanging(object sender, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox.Password = ViewModel.Pwd;
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            ViewModel.Pwd = PasswordBox.Password;
        }
    }
}
