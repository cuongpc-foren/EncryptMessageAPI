using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Library.Entities;

public partial class MessageEncryptContext : DbContext
{
    public MessageEncryptContext()
    {
    }

    public MessageEncryptContext(DbContextOptions<MessageEncryptContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Token> Tokens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DELL-PRECISION-\\CUONGPC;Database=MessageEncrypt;User Id=CuongPC;Password=Cuongpham@510;TrustServerCertificate=True;Trusted_Connection=true;Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Accounts__349DA5A6993FC6A6");

            entity.Property(e => e.AccountId).HasMaxLength(40);
            entity.Property(e => e.PassToConnect).HasMaxLength(40);
            entity.Property(e => e.Password).HasMaxLength(40);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        modelBuilder.Entity<Token>(entity =>
        {
            entity.HasKey(e => e.AccountId);

            entity.ToTable("Token");

            entity.Property(e => e.AccountId).HasMaxLength(40);
            entity.Property(e => e.CreateTime).HasColumnType("datetime");
            entity.Property(e => e.TokenValue).HasMaxLength(40);

            entity.HasOne(d => d.Account).WithOne(p => p.Token)
                .HasForeignKey<Token>(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Token__CreateTim__5DCAEF64");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
