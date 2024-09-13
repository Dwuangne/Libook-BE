using System;
using System.Collections.Generic;
using Libook_API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Libook_API.Data;

public partial class LibookDbContext : DbContext
{
    public LibookDbContext()
    {
    }

    public LibookDbContext(DbContextOptions<LibookDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdministrativeRegion> AdministrativeRegions { get; set; }

    public virtual DbSet<AdministrativeUnit> AdministrativeUnits { get; set; }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookImage> BookImages { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<CommentImage> CommentImages { get; set; }

    public virtual DbSet<Conversation> Conversations { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OrderInfo> OrderInfos { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<Participant> Participants { get; set; }

    public virtual DbSet<PaymentOrder> PaymentOrders { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

    public virtual DbSet<VoucherActived> VoucherActiveds { get; set; }

    public virtual DbSet<Ward> Wards { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=LibookConnectionString");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdministrativeRegion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("administrative_regions_pkey");

            entity.ToTable("administrative_regions");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CodeName)
                .HasMaxLength(255)
                .HasColumnName("code_name");
            entity.Property(e => e.CodeNameEn)
                .HasMaxLength(255)
                .HasColumnName("code_name_en");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.NameEn)
                .HasMaxLength(255)
                .HasColumnName("name_en");
        });

        modelBuilder.Entity<AdministrativeUnit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("administrative_units_pkey");

            entity.ToTable("administrative_units");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CodeName)
                .HasMaxLength(255)
                .HasColumnName("code_name");
            entity.Property(e => e.CodeNameEn)
                .HasMaxLength(255)
                .HasColumnName("code_name_en");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("full_name");
            entity.Property(e => e.FullNameEn)
                .HasMaxLength(255)
                .HasColumnName("full_name_en");
            entity.Property(e => e.ShortName)
                .HasMaxLength(255)
                .HasColumnName("short_name");
            entity.Property(e => e.ShortNameEn)
                .HasMaxLength(255)
                .HasColumnName("short_name_en");
        });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasIndex(e => e.AuthorId, "IX_Books_AuthorId");

            entity.HasIndex(e => e.CategoryId, "IX_Books_CategoryId");

            entity.HasIndex(e => e.SupplierId, "IX_Books_SupplierId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IsActive).HasColumnName("isActive");

            entity.HasOne(d => d.Author).WithMany(p => p.Books).HasForeignKey(d => d.AuthorId);

            entity.HasOne(d => d.Category).WithMany(p => p.Books).HasForeignKey(d => d.CategoryId);

            entity.HasOne(d => d.Supplier).WithMany(p => p.Books).HasForeignKey(d => d.SupplierId);
        });

        modelBuilder.Entity<BookImage>(entity =>
        {
            entity.HasIndex(e => e.BookId, "IX_BookImages_BookId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Book).WithMany(p => p.BookImages).HasForeignKey(d => d.BookId);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasIndex(e => e.BookId, "IX_Comments_BookId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Book).WithMany(p => p.Comments).HasForeignKey(d => d.BookId);
        });

        modelBuilder.Entity<CommentImage>(entity =>
        {
            entity.HasIndex(e => e.CommentId, "IX_CommentImages_CommentId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Comment).WithMany(p => p.CommentImages).HasForeignKey(d => d.CommentId);
        });

        modelBuilder.Entity<Conversation>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("districts_pkey");

            entity.ToTable("districts");

            entity.HasIndex(e => e.ProvinceCode, "idx_districts_province");

            entity.HasIndex(e => e.AdministrativeUnitId, "idx_districts_unit");

            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.AdministrativeUnitId).HasColumnName("administrative_unit_id");
            entity.Property(e => e.CodeName)
                .HasMaxLength(255)
                .HasColumnName("code_name");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("full_name");
            entity.Property(e => e.FullNameEn)
                .HasMaxLength(255)
                .HasColumnName("full_name_en");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.NameEn)
                .HasMaxLength(255)
                .HasColumnName("name_en");
            entity.Property(e => e.ProvinceCode)
                .HasMaxLength(20)
                .HasColumnName("province_code");

            entity.HasOne(d => d.AdministrativeUnit).WithMany(p => p.Districts)
                .HasForeignKey(d => d.AdministrativeUnitId)
                .HasConstraintName("districts_administrative_unit_id_fkey");

            entity.HasOne(d => d.ProvinceCodeNavigation).WithMany(p => p.Districts)
                .HasForeignKey(d => d.ProvinceCode)
                .HasConstraintName("districts_province_code_fkey");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasIndex(e => e.ConversationId, "IX_Messages_ConversationId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Conversation).WithMany(p => p.Messages).HasForeignKey(d => d.ConversationId);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasIndex(e => e.VoucherId, "IX_Orders_VoucherId");

            entity.Property(e => e.OrderId).ValueGeneratedNever();

            entity.HasOne(d => d.Voucher).WithMany(p => p.Orders).HasForeignKey(d => d.VoucherId);
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasIndex(e => e.BookId, "IX_OrderDetails_BookId");

            entity.HasIndex(e => e.OrderId, "IX_OrderDetails_OrderId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Book).WithMany(p => p.OrderDetails).HasForeignKey(d => d.BookId);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails).HasForeignKey(d => d.OrderId);
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasIndex(e => e.OrderId, "IX_OrderStatuses_OrderId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Order).WithMany(p => p.OrderStatuses).HasForeignKey(d => d.OrderId);
        });

        modelBuilder.Entity<Participant>(entity =>
        {
            entity.HasIndex(e => e.ConversationId, "IX_Participants_ConversationId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Conversation).WithMany(p => p.Participants).HasForeignKey(d => d.ConversationId);
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("provinces_pkey");

            entity.ToTable("provinces");

            entity.HasIndex(e => e.AdministrativeRegionId, "idx_provinces_region");

            entity.HasIndex(e => e.AdministrativeUnitId, "idx_provinces_unit");

            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.AdministrativeRegionId).HasColumnName("administrative_region_id");
            entity.Property(e => e.AdministrativeUnitId).HasColumnName("administrative_unit_id");
            entity.Property(e => e.CodeName)
                .HasMaxLength(255)
                .HasColumnName("code_name");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("full_name");
            entity.Property(e => e.FullNameEn)
                .HasMaxLength(255)
                .HasColumnName("full_name_en");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.NameEn)
                .HasMaxLength(255)
                .HasColumnName("name_en");

            entity.HasOne(d => d.AdministrativeRegion).WithMany(p => p.Provinces)
                .HasForeignKey(d => d.AdministrativeRegionId)
                .HasConstraintName("provinces_administrative_region_id_fkey");

            entity.HasOne(d => d.AdministrativeUnit).WithMany(p => p.Provinces)
                .HasForeignKey(d => d.AdministrativeUnitId)
                .HasConstraintName("provinces_administrative_unit_id_fkey");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.Property(e => e.VoucherId).ValueGeneratedNever();
        });

        modelBuilder.Entity<VoucherActived>(entity =>
        {
            entity.HasKey(e => new { e.VoucherId, e.UserId });

            entity.HasOne(d => d.Voucher).WithMany(p => p.VoucherActiveds).HasForeignKey(d => d.VoucherId);
        });

        modelBuilder.Entity<Ward>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("wards_pkey");

            entity.ToTable("wards");

            entity.HasIndex(e => e.DistrictCode, "idx_wards_district");

            entity.HasIndex(e => e.AdministrativeUnitId, "idx_wards_unit");

            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.AdministrativeUnitId).HasColumnName("administrative_unit_id");
            entity.Property(e => e.CodeName)
                .HasMaxLength(255)
                .HasColumnName("code_name");
            entity.Property(e => e.DistrictCode)
                .HasMaxLength(20)
                .HasColumnName("district_code");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("full_name");
            entity.Property(e => e.FullNameEn)
                .HasMaxLength(255)
                .HasColumnName("full_name_en");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.NameEn)
                .HasMaxLength(255)
                .HasColumnName("name_en");

            entity.HasOne(d => d.AdministrativeUnit).WithMany(p => p.Wards)
                .HasForeignKey(d => d.AdministrativeUnitId)
                .HasConstraintName("wards_administrative_unit_id_fkey");

            entity.HasOne(d => d.DistrictCodeNavigation).WithMany(p => p.Wards)
                .HasForeignKey(d => d.DistrictCode)
                .HasConstraintName("wards_district_code_fkey");
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
