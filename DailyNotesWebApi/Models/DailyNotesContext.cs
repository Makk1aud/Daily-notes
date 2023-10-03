using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DailyNotesWebApi.Models;

public partial class DailyNotesContext : DbContext
{
    public DailyNotesContext()
    {
    }

    public DailyNotesContext(DbContextOptions<DailyNotesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Note> Notes { get; set; }

    public virtual DbSet<NoteType> NoteTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=.\\SQLEXPRESS;User Id=makklaud;Password=123;Database=Daily_Notes;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK__Client__BF21A4244A83B9A6");

            entity.ToTable("Client");

            entity.Property(e => e.ClientId)
                .ValueGeneratedOnAdd()
                .HasColumnName("client_id");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.GenderId).HasColumnName("gender_id");
            entity.Property(e => e.Login)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("password");

            //entity.HasOne(d => d.ClientNavigation).WithOne(p => p.Client)
            //    .HasForeignKey<Client>(d => d.ClientId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK__Client__client_i__3B75D760");
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.GenderId).HasName("PK__Gender__9DF18F879AF4B983");

            entity.ToTable("Gender");

            entity.Property(e => e.GenderId).HasColumnName("gender_id");
            entity.Property(e => e.GenderTitle)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("gender_title");
        });

        modelBuilder.Entity<Note>(entity =>
        {
            entity.HasKey(e => e.NoteId).HasName("PK__Note__CEDD0FA41DB2D296");

            entity.ToTable("Note");

            entity.Property(e => e.NoteId).HasColumnName("note_id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.EditDate)
                .HasColumnType("datetime")
                .HasColumnName("edit_date");
            entity.Property(e => e.NoteText)
                .IsUnicode(false)
                .HasColumnName("note_text");
            entity.Property(e => e.NoteTitle)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("note_title");
            entity.Property(e => e.NoteTypeId).HasColumnName("note_type_id");

            //entity.HasOne(d => d.Client).WithMany(p => p.Notes)
            //    .HasForeignKey(d => d.ClientId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK__Note__note_title__3E52440B");

            entity.HasOne(d => d.NoteType).WithMany(p => p.Notes)
                .HasForeignKey(d => d.NoteTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Note__note_type___3F466844");
        });

        modelBuilder.Entity<NoteType>(entity =>
        {
            entity.HasKey(e => e.NoteTypeId).HasName("PK__NoteType__2E9692FA9427919A");

            entity.ToTable("NoteType");

            entity.Property(e => e.NoteTypeId).HasColumnName("note_type_id");
            entity.Property(e => e.TypeTitle)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("type_title");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
