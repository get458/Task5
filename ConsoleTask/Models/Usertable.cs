using System;
using System.Collections.Generic;

namespace ConsoleTask.Models;

public partial class Usertable
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Mail { get; set; } = null!;

    public DateOnly Birthday { get; set; }

    public string Telephone { get; set; } = null!;

    public int? AddressId { get; set; }

    public virtual Address? Address { get; set; }
}
