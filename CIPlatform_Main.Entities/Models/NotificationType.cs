using System;
using System.Collections.Generic;

namespace CIPlatform_Main.Entities.Models;

public partial class NotificationType
{
    public long NotifytypeId { get; set; }

    public string? NotifType { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<NotifPreference> NotifPreferences { get; } = new List<NotifPreference>();
}
