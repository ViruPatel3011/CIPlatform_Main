using System;
using System.Collections.Generic;

namespace CIPlatform_Main.Entities.Models;

public partial class NotifPreference
{
    public long NotifprefId { get; set; }

    public long? Userid { get; set; }

    public long? NotifytypeId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual NotificationType? Notifytype { get; set; }

    public virtual User? User { get; set; }
}
