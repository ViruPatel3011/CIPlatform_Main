﻿using System;
using System.Collections.Generic;

namespace CIPlatform_Main.Entities.Models;

public partial class NotifAlluser
{
    public long NotallId { get; set; }

    public string? Notification { get; set; }

    public string? Url { get; set; }

    public byte Isread { get; set; }

    public DateTime CreatedAt { get; set; }
}
