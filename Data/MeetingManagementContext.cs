using System;
using System.Collections.Generic;
using MeetingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingAPI.Data;

public partial class MeetingManagementContext : DbContext
{
    public MeetingManagementContext()
    {
    }

    public MeetingManagementContext(DbContextOptions<MeetingManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Meeting> Meetings { get; set; }

    public virtual DbSet<MeetingParticipant> MeetingParticipants { get; set; }

    public virtual DbSet<MeetingType> MeetingTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=PINAL\\SQLEXPRESS01;Database=MeetingManagement;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Meeting>(entity =>
        {
            entity.ToTable("Meeting");

            entity.Property(e => e.MeetingId).HasColumnName("MeetingID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Location).HasMaxLength(500);
            entity.Property(e => e.MeetingDate).HasColumnType("datetime");
            entity.Property(e => e.MeetingTypeId).HasColumnName("MeetingTypeID");
            entity.Property(e => e.ModifiedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(100);

            //entity.HasOne(d => d.MeetingType).WithMany(p => p.Meetings)
            //    .HasForeignKey(d => d.MeetingTypeId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_Meeting_MeetingType");
        });

        modelBuilder.Entity<MeetingParticipant>(entity =>
        {
            entity.HasKey(e => e.ParticipantId);

            entity.Property(e => e.ParticipantId)
                .ValueGeneratedNever()
                .HasColumnName("ParticipantID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActualPresent).HasDefaultValue(true);
            entity.Property(e => e.IsInvited).HasDefaultValue(true);
            entity.Property(e => e.MeetingId)
                .ValueGeneratedOnAdd()
                .HasColumnName("MeetingID");
            entity.Property(e => e.ModifiedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            //entity.HasOne(d => d.Meeting).WithMany(p => p.MeetingParticipants)
            //    .HasForeignKey(d => d.MeetingId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_MeetingParticipants_Meeting");

            //entity.HasOne(d => d.User).WithMany(p => p.MeetingParticipants)
            //    .HasForeignKey(d => d.UserId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_MeetingParticipants_User");
        });

        modelBuilder.Entity<MeetingType>(entity =>
        {
            entity.ToTable("MeetingType");

            entity.Property(e => e.MeetingTypeId).HasColumnName("MeetingTypeID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MeetingTypeName).HasMaxLength(100);
            entity.Property(e => e.ModifiedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(100);
            entity.Property(e => e.ImagePath).HasMaxLength(500);
            entity.Property(e => e.MobileNo).HasMaxLength(100);
            entity.Property(e => e.ModifiedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.UserName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
