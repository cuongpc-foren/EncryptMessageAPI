using System;
using System.Collections.Generic;

namespace Library.Entities;

public partial class Account
{
    public string AccountId { get; set; } = null!;

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string? PassToConnect { get; set; }

    public virtual Token? Token { get; set; }
}
