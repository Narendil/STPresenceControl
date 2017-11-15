using STPresenceControl.Contracts;
using STPresenceControl.Libs;
using STPresenceControl.ViewModels;
using SugaarSoft.MVVM.Services;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace STPresenceControl
{
    public partial class App : Application
    {
        #region Const

        public const string CN_UserName = "UserName";
        public const string CN_Pwd = "Pwd";

        #endregion

        protected override void OnStartup(StartupEventArgs e)
        {
            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Count() != 1)
                return;
            ConfigureIoC();
            IoC.Resolve<ViewManager>();
            base.OnStartup(e);
        }

        public static IIoCContainer IoC { get { return SugaarSoft.MVVM.Services.IoC.Current; } }

        private void ConfigureIoC()
        {
            RegisterServices();
            RegisterSections();
            IoC.RegisterType<ViewManager>();
        }

        private void RegisterSections()
        {
            IoC.RegisterType<MainViewModel>();
            IoC.RegisterType<UserConfigViewModel>();
        }

        private void RegisterServices()
        {
            IoC.RegisterType<ISerializationService, JsonSerializationService>();
            IoC.RegisterType<IDataProvider, ZucchettiDataProvider>();
            IoC.RegisterType<ISettingsService, UserSettingsService>();
        }
    }
}
