using System;
using System.Collections.Generic;

namespace ConsoleTask.Models;

public partial class Address
{
    public int Id { get; set; }

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string Building { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Usertable> Usertables { get; } = new List<Usertable>();
}
