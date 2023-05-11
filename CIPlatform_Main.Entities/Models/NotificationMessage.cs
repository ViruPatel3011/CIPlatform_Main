using System;
using System.Collections.Generic;

namespace CIPlatform_Main.Entities.Models;

public partial class NotificationMessage
{
    public int MessageId { get; set; }

    public long? NotificationId { get; set; }

    public string? Message { get; set; }

    public virtual NotificationPreference? Notification { get; set; }

    public virtual ICollection<Userpermission> Userpermissions { get; } = new List<Userpermission>();
}
