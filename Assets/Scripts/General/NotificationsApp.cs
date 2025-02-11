using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;

public class NotificationsApp : MonoBehaviour
{
    public static NotificationsApp instance;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(this.gameObject);
    }

    public void SendMessage()
    {
        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    public void CreateNotification(float time)
    {

        var noti = new AndroidNotification();
        noti.Title = "Hey! Your stamina is complete";
        noti.Text = "Come to play";
        noti.FireTime = System.DateTime.Now.AddSeconds(time);

        var id = AndroidNotificationCenter.SendNotification(noti, "channel_Id");

        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelAllNotifications();
            AndroidNotificationCenter.SendNotification(noti, "channel_Id");
        }
    }
}
