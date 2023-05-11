using System;
using System.Collections.Generic;

namespace CIPlatform_Main.Entities.Models;

public partial class MessageTable
{
    public int MessageId { get; set; }

    public long? NotificationId { get; set; }

    public string? Message { get; set; }

    public string? Url { get; set; }

    public long? FromUser { get; set; }

    public long? ToUser { get; set; }

    public DateTime CreatedAt { get; set; }

    public byte Isread { get; set; }

    public virtual User? FromUserNavigation { get; set; }

    public virtual NotificationPreference? Notification { get; set; }

    public virtual User? ToUserNavigation { get; set; }
}
