using System;
using System.Collections.Generic;

namespace Libook_API.Models.Domain;

public partial class Comment
{
    public Guid Id { get; set; }

    public DateTime DateCreate { get; set; }

    public string Content { get; set; } = null!;

    public Guid UserId { get; set; }

    public Guid BookId { get; set; }

    public virtual Book Book { get; set; }

    public virtual ICollection<CommentImage> CommentImages { get; set; } = new List<CommentImage>();
}
