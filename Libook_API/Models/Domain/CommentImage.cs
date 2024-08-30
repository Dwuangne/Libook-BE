using System;
using System.Collections.Generic;

namespace Libook_API.Models.Domain;

public partial class CommentImage
{
    public Guid Id { get; set; }

    public string? CommentImageUrl { get; set; }

    public Guid CommentId { get; set; }

    public virtual Comment Comment { get; set; }
}
