using System;
using System.Collections.Generic;

namespace CIPlatform_Main.Entities.Models;

public partial class NotificationPreference
{
    public long NotificationId { get; set; }

    public string? Title { get; set; }

    public virtual ICollection<EnableUserPreference> EnableUserPreferences { get; } = new List<EnableUserPreference>();

    public virtual ICollection<MessageTable> MessageTables { get; } = new List<MessageTable>();
}
