﻿using System;
using System.Collections.Generic;

namespace Libook_API.Models.Domain;

public partial class Supplier
{
    public Supplier()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
