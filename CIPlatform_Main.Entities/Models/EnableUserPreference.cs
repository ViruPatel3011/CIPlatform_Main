using System;
using System.Collections.Generic;

namespace CIPlatform_Main.Entities.Models;

public partial class EnableUserPreference
{
    public long Enableuserid { get; set; }

    public long? NotificationId { get; set; }

    public long? UserId { get; set; }

    public int? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual NotificationPreference? Notification { get; set; }

    public virtual User? User { get; set; }
}
