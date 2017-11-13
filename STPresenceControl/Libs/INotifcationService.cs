using STPresenceControl.Enums;

namespace STPresenceControl.Notification
{
    public interface INotifcationService
    {
        void Show(string message);
        void Show(string message, string title);
        void Show(string message, string title, NotificationTypeEnum notificationType);
        void Show(string message, string title, NotificationTypeEnum notificationType, int showMilliseconds);
    }
}
