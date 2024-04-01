using System;
using System.Collections.Generic;

namespace Library.Entities;

public partial class Token
{
    public string AccountId { get; set; } = null!;

    public string? TokenValue { get; set; }

    public DateTime? CreateTime { get; set; }

    public virtual Account Account { get; set; } = null!;
}
