using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TaskIT.Models;

public partial class TaskItContext : DbContext
{
    public TaskItContext()
    {
    }

    public TaskItContext(DbContextOptions<TaskItContext> options)
        : base(options)
    {
    }     
    public virtual DbSet<FirstTable> FirstTables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TaskIT;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FirstTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FirstTab__3214EC07B07D95B1");

            entity.ToTable("FirstTable");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Amount).HasColumnType("decimal(7, 4)");
            entity.Property(e => e.CounterpartyName).HasMaxLength(20);
            entity.Property(e => e.Garbage).HasColumnType("decimal(4, 1)");
            entity.Property(e => e.Infection).HasMaxLength(20);
            entity.Property(e => e.Process).HasMaxLength(20);
            entity.Property(e => e.Product).HasMaxLength(20);
            entity.Property(e => e.RecordDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.Wetness).HasColumnType("decimal(4, 1)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
