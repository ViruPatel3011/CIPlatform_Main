using System;
using System.Collections.Generic;

namespace CIPlatform_Main.Entities.Models;

public partial class PasswordReset
{
    public string? Email { get; set; }

    public string Token { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
