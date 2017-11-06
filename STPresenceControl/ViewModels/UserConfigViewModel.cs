using STPresenceControl.Common;
using STPresenceControl.Contracts;
using SugaarSoft.MVVM.Base;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace STPresenceControl.ViewModels
{
    public class UserConfigViewModel : NotificationObject, IPasswordHandler
    {
        #region Ctor

        public UserConfigViewModel()
        {
            LoadCommands();
            LoadDefaultValues();
        }

        #endregion

        #region Binding

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; SaveConfigurationChanges(); }
        }

        private string _pwd;
        public string Pwd
        {
            get { return _pwd; }
            set { _pwd = value; SaveConfigurationChanges(); }
        }

        #endregion

        #region Commands

        public ICommand SaveCommand { get; private set; }

        private void LoadCommands()
        {
            SaveCommand = new BasicCommand(ExecuteSaveCommand);
        }

        private void ExecuteSaveCommand(object obj)
        {
            SettingsService.Instance.SaveSetting("UserName", UserName);
            SettingsService.Instance.SaveSetting("Pwd", Pwd);
        }

        #endregion

        #region Private

        private void SaveConfigurationChanges([CallerMemberName]string propertyName = null)
        {
            OnPropertyChanged(propertyName);
            SaveCommand.Execute(null);
        }

        private void LoadDefaultValues()
        {
            _userName = SettingsService.Instance.LoadSetting(App.CN_UserName);
            _pwd = SettingsService.Instance.LoadSetting(App.CN_Pwd);
        }

        private bool HasChanged()
        {
            return (UserName != SettingsService.Instance.LoadSetting(App.CN_UserName)
                        || Pwd != SettingsService.Instance.LoadSetting(App.CN_Pwd));
        }

        #endregion
    }
}
