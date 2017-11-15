using STPresenceControl.Common;
using STPresenceControl.Contracts;
using STPresenceControl.Libs;
using STPresenceControl.Models;
using STPresenceControl.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;

namespace STPresenceControl
{
    public class ViewManager
    {
        #region Const

        const string CN_Info = "Configuración";
        const string CN_Refresh = "Refrescar";
        const string CN_Auto = "Autoarranque";
        const string CN_Exit = "Salir";
        const string CN_Separator = "-";

        #endregion

        #region Fields
        private readonly IDataProvider _dataProvider;
        private readonly INotificationService _notificationService;
        private readonly ISettingsService _settingsService;

        private double _leftMins;
        private readonly NotifyIcon _notifyIcon;
        private readonly Window _configurationWindow;
        private readonly DispatcherTimer _refreshData;
        private readonly DispatcherTimer _leftTimeTimer;

        #endregion

        #region Properties

        private readonly List<PresenceControlEntry> _presenceControlEntries = new List<PresenceControlEntry>();
        public List<PresenceControlEntry> PresenceControlEntries { get { return _presenceControlEntries; } }

        #endregion

        #region Context Menu

        private ContextMenu GenerateContextMenu()
        {
            var contextMenu = new ContextMenu();
            contextMenu.MenuItems.Add(CN_Info, ExecuteShowConfig);
            contextMenu.MenuItems.Add(CN_Refresh, ExecuteRefresh);
            contextMenu.MenuItems.Add(CN_Auto, ExecuteChangeStartup);
            contextMenu.MenuItems.Add(CN_Separator);
            contextMenu.MenuItems.Add(CN_Exit, ExecuteExit);
            return contextMenu;
        }

        private void ExecuteChangeStartup(object sender, EventArgs e)
        {
            if (RegistryServiceHelper.IsOnStartup())
                RegistryServiceHelper.RemoveFromStartup();
            else
                RegistryServiceHelper.AddToStartup();
            CheckContextMenuState();
        }

        private void ExecuteRefresh(object sender, EventArgs e)
        {
            GetPrensenceControlEntries();
        }

        private void CheckContextMenuState()
        {
            //¿?
            //_notifyIcon.ContextMenu.MenuItems[CN_Auto].Checked = RegistryServiceHelper.IsOnStartup();
            _notifyIcon.ContextMenu.MenuItems[2].Checked = RegistryServiceHelper.IsOnStartup();
        }

        private void ExecuteShowConfig(object sender, EventArgs e)
        {
            _configurationWindow.Show();
        }

        private void ExecuteExit(object sender, EventArgs e)
        {
            App.Current.Shutdown();
        }

        #endregion

        #region Ctor

        public ViewManager(IDataProvider dataProvider, ISettingsService settingsService)
        {
            _settingsService = settingsService;
            _dataProvider = dataProvider;
            _configurationWindow = GenerateConfigurationWindow();
            _notifyIcon = new NotifyIcon(new Container())
            {
                ContextMenuStrip = new ContextMenuStrip(),
                Icon = Properties.Resources.AppIcon,
                Text = String.Format("Esperando datos..."),
                Visible = true,
                ContextMenu = GenerateContextMenu()
            };
            _notificationService = new BallonTipNotificationService(_notifyIcon);
            App.IoC.RegisterInstance(_notificationService);

            CheckContextMenuState();

            _notificationService.Show("Iniciando...", "Control de presencia", Enums.NotificationTypeEnum.Info);
            _refreshData = new DispatcherTimer(new TimeSpan(0, 30, 0), DispatcherPriority.Normal, (sender, e) => GetPrensenceControlEntries(), Dispatcher.CurrentDispatcher);
            _leftTimeTimer = new DispatcherTimer(new TimeSpan(0, 1, 0), DispatcherPriority.Normal, (sender, e) =>
                  {
                      RefreshNotifyIcon();
                      _leftMins--;
                  },
                   Dispatcher.CurrentDispatcher);
            GetPrensenceControlEntries();
        }

        #endregion

        #region Private

        private async void GetPrensenceControlEntries()
        {
            try
            {
                PresenceControlEntries.Clear();
                var userName = await _settingsService.GetSettingAsync<string>(App.CN_UserName);
                if (string.IsNullOrEmpty(userName))
                {
                    _notificationService.Show("Datos de login no encontrados. Acceda a la sección de configuración.", "Control de presencia", Enums.NotificationTypeEnum.Info);
                    return;
                }
                var pwd = await _settingsService.GetSettingAsync<string>(App.CN_Pwd);
                await _dataProvider.LoginAsync(userName, pwd);
                PresenceControlEntries.AddRange(await _dataProvider.GetPrensenceControlEntriesAsync(DateTime.Today));
                _leftMins = PresenceControlEntriesHelper.GetLeftTimeMinutes(_presenceControlEntries);
                _notificationService.Show("Actualizadas entradas y salidas.", "Control de presencia", Enums.NotificationTypeEnum.Info);
                RefreshNotifyIcon();
            }
            catch
            {
                //TODO - Sistema de log
            }
        }

        private void RefreshNotifyIcon()
        {
            if (_presenceControlEntries.Count == 0)
                return;
            var leftTimeSpan = new TimeSpan(0, Convert.ToInt32(_leftMins), 0);
            var colorIconText = Color.Green;

            if (leftTimeSpan.TotalHours >= 1)
                _notifyIcon.Icon = Icons.CreateTextIcon(((int)leftTimeSpan.TotalHours).ToString() + "h", Color.Red);
            else
                _notifyIcon.Icon = Icons.CreateTextIcon(leftTimeSpan.TotalMinutes.ToString(), Color.Green);
            _notifyIcon.Text = String.Format("Tiempo restante {0}", leftTimeSpan.ToString(@"hh\:mm"));
            if (leftTimeSpan.TotalMinutes < 1)
                _notificationService.Show("Ha terminado tu jornada laboral.", "Control de presencia", Enums.NotificationTypeEnum.Info);
        }

        private Window GenerateConfigurationWindow()
        {
            var window = new Window();
            window.Closing += OnConfigWindowClosing;
            window.Content = new MainViewModel();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            return window;
        }

        private void OnConfigWindowClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            var window = (Window)sender;
            window.Hide();
        }

        #endregion
    }
}
