using STPresenceControl.Contracts;
using STPresenceControl.Enums;
using System;
using System.Windows.Forms;

namespace STPresenceControl.Libs
{
    public class BallonTipNotificationService : INotificationService
    {
        public readonly NotifyIcon NotifyIcon;
        public BallonTipNotificationService(NotifyIcon notifyIcon)
        {
            NotifyIcon = notifyIcon;
        }
        
        public void Show(string message)
        {
            Show(message, null);
        }

        public void Show(string message, string title)
        {
            Show(message, title, NotificationTypeEnum.None);
        }

        public void Show(string message, string title, NotificationTypeEnum notificationType)
        {
            Show(message, title, notificationType, 3000);
        }

        public void Show(string message, string title, NotificationTypeEnum notificationType, int showMilliseconds)
        {
            NotifyIcon.BalloonTipText = message;
            NotifyIcon.BalloonTipTitle = title;
            NotifyIcon.BalloonTipIcon = (ToolTipIcon)Enum.Parse(typeof(ToolTipIcon), notificationType.ToString());
            NotifyIcon.ShowBalloonTip(showMilliseconds);
        }
    }
}
