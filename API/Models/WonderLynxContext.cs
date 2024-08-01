using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

public partial class WonderLynxContext : DbContext
{
    public WonderLynxContext()
    {
    }

    public WonderLynxContext(DbContextOptions<WonderLynxContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<ReferenceItem> ReferenceItems { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<Type> Types { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MBR-LENOVO;Database=WonderLynx;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A0B7B14E91D");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<ReferenceItem>(entity =>
        {
            entity.HasKey(e => e.ReferenceId).HasName("PK__Referenc__E1A99A19A3C35965");

            entity.Property(e => e.Subtitle).HasMaxLength(200);
            entity.Property(e => e.ThumbnailUrl).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.Category).WithMany(p => p.ReferenceItems)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Reference__Categ__3E52440B");

            entity.HasOne(d => d.Type).WithMany(p => p.ReferenceItems)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK__Reference__TypeI__3D5E1FD2");

            entity.HasMany(d => d.Tags).WithMany(p => p.References)
                .UsingEntity<Dictionary<string, object>>(
                    "ReferenceTag",
                    r => r.HasOne<Tag>().WithMany()
                        .HasForeignKey("TagId")
                        .HasConstraintName("FK__Reference__TagId__4222D4EF"),
                    l => l.HasOne<ReferenceItem>().WithMany()
                        .HasForeignKey("ReferenceId")
                        .HasConstraintName("FK__Reference__Refer__412EB0B6"),
                    j =>
                    {
                        j.HasKey("ReferenceId", "TagId").HasName("PK__Referenc__37FE55839E4CE580");
                        j.ToTable("ReferenceTags");
                    });
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("PK__Tags__657CF9AC6C65A007");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Type>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__Types__516F03B52C8BBBB1");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
