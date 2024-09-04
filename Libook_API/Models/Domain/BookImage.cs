using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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

    //[JsonIgnore] // Bỏ qua tham chiếu ngược lại Book để tránh vòng lặp
    public virtual Book Book { get; set; } = null!;
}
