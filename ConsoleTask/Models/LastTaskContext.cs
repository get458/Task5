using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ConsoleTask.Models;

public partial class LastTaskContext : DbContext
{
    public LastTaskContext()
    {
    }

    public LastTaskContext(DbContextOptions<LastTaskContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Usertable> Usertables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=5432;Database=LastTask;Username=postgres;Password=784512bf3X");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("address_pkey");

            entity.ToTable("address");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Building).HasColumnName("building");
            entity.Property(e => e.City).HasColumnName("city");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Street).HasColumnName("street");
        });

        modelBuilder.Entity<Usertable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("usertable_pkey");

            entity.ToTable("usertable");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.Lastname).HasColumnName("lastname");
            entity.Property(e => e.Mail).HasColumnName("mail");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Surname).HasColumnName("surname");
            entity.Property(e => e.Telephone)
                .HasMaxLength(11)
                .HasColumnName("telephone");

            entity.HasOne(d => d.Address).WithMany(p => p.Usertables)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("usertable_address_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
