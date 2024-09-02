using System;
using System.Collections.Generic;

namespace Libook_API.Models.Domain;

public partial class Book
{

    public Book()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public double Price { get; set; }

    public float PrecentDiscount { get; set; }

    public int Remain { get; set; }

    public bool IsActive { get; set; }

    public Guid AuthorId { get; set; }

    public Guid CategoryId { get; set; }

    public Guid SupplierId { get; set; }

    public virtual Author Author { get; set; } = null!;

    public virtual ICollection<BookImage> BookImages { get; set; } = new List<BookImage>();

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Supplier Supplier { get; set; } = null!;
}
