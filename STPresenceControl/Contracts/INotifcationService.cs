using STPresenceControl.Enums;

namespace STPresenceControl.Contracts
{
    public interface INotificationService
    {
        void Show(string message);
        void Show(string message, string title);
        void Show(string message, string title, NotificationTypeEnum notificationType);
        void Show(string message, string title, NotificationTypeEnum notificationType, int showMilliseconds);
    }
}
