using STPresenceControl.Common;
using STPresenceControl.Contracts;
using SugaarSoft.MVVM.Base;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace STPresenceControl.ViewModels
{
    public class UserConfigViewModel : NotificationObject, IPasswordHandler
    {
        #region Services

        private ISettingsService _settingsService;

        #endregion

        #region Ctor

        public UserConfigViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;
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

        private async void LoadDefaultValues()
        {
            _userName = await _settingsService.GetSettingAsync<string>(App.CN_UserName);
            _pwd = await _settingsService.GetSettingAsync<string>(App.CN_Pwd);
        }

        #endregion
    }
}
