﻿using System;
using System.Collections.Generic;

namespace CIPlatform_Main.Entities.Models;

public partial class Userpermission
{
    public int UserpermissionId { get; set; }

    public long? UserId { get; set; }

    public int? Status { get; set; }

    public int? MessageId { get; set; }

    public virtual NotificationMessage? Message { get; set; }

    public virtual User? User { get; set; }
}
