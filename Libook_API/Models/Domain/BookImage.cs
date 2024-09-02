using System;
using System.Collections.Generic;

namespace Libook_API.Models.Domain;

public partial class BookImage
{
    public BookImage()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }

    public string? BookImageUrl { get; set; }

    public Guid BookId { get; set; }

    public virtual Book Book { get; set; } = null!;
}
