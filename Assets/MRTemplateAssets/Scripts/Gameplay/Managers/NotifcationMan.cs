using System.Collections;
using System.Collections.Generic;
// using Unity.Notifications.Android;
using UnityEngine;

public class NotifcationMan : MonoBehaviour
{
    // void Start()
    // {
    //     // Remove notifications already displayed
    //     AndroidNotificationCenter.CancelAllDisplayedNotifications();
        
    //     // Creating android notification channel to send messages
    //     var c = new AndroidNotificationChannel()
    //     {
    //         Id = "channel_id",
    //         Name = "Default Channel",
    //         Importance = Importance.High,
    //         Description = "Reminder notifications",
    //     };
    //     AndroidNotificationCenter.RegisterNotificationChannel(c);

    //     // Creating the notfication that is going to be sent!
    //     var notification = new AndroidNotification();
    //     notification.Title = "Saving Party Island";
    //     notification.Text = "A new event has started, come back!";
    //     notification.LargeIcon = "icon_0";
    //     notification.FireTime = System.DateTime.Now.AddMinutes(1);

    //     var id = AndroidNotificationCenter.SendNotification(notification, "channel_id");

    //     if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled)
    //     {
    //         AndroidNotificationCenter.CancelAllNotifications();
    //         AndroidNotificationCenter.SendNotification(notification, "channel_id");
    //     }
    // }
}


