using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Models;

public partial class BookingAppContext : DbContext
{
    public BookingAppContext()
    {
    }

    public BookingAppContext(DbContextOptions<BookingAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Korisnici> Korisnicis { get; set; }

    public virtual DbSet<Recenzija> Recenzijas { get; set; }

    public virtual DbSet<Rezervacije> Rezervacijes { get; set; }

    public virtual DbSet<Smještaj> Smještajs { get; set; }

    public virtual DbSet<TipSmještaja> TipSmještajas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=CVJETKOVICG-LAP;Database=BookingApp;Trusted_Connection=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Korisnici>(entity =>
        {
            entity.HasKey(e => e.KorisnikId).HasName("PK__Korisnic__80B06D411403DB17");

            entity.ToTable("Korisnici");

            entity.Property(e => e.BrojTelefona)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Email).HasMaxLength(30);
            entity.Property(e => e.Ime).HasMaxLength(50);
            entity.Property(e => e.KorisničkoIme).HasMaxLength(50);
            entity.Property(e => e.Prezime).HasMaxLength(50);
        });

        modelBuilder.Entity<Recenzija>(entity =>
        {
            entity.HasKey(e => e.RecenzijaId).HasName("PK__Recenzij__D36C607027D66246");

            entity.ToTable("Recenzija");

            entity.Property(e => e.Komentar).HasMaxLength(200);
            entity.Property(e => e.Ocjena).HasColumnType("decimal(2, 1)");

            entity.HasOne(d => d.Korisnik).WithMany(p => p.Recenzijas)
                .HasForeignKey(d => d.KorisnikId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Recenzija__Koris__797309D9");

            entity.HasOne(d => d.Smještaj).WithMany(p => p.Recenzijas)
                .HasForeignKey(d => d.SmještajId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Recenzija__Smješ__7A672E12");
        });

        modelBuilder.Entity<Rezervacije>(entity =>
        {
            entity.HasKey(e => e.RezervacijaId).HasName("PK__Rezervac__CABA44DDD5B0B0B6");

            entity.ToTable("Rezervacije");

            entity.Property(e => e.DatumRezervacije).HasColumnType("datetime");
            entity.Property(e => e.KrajRezervacije).HasColumnType("datetime");
            entity.Property(e => e.PočetakRezervacije).HasColumnType("datetime");

            entity.HasOne(d => d.Korisnik).WithMany(p => p.Rezervacijes)
                .HasForeignKey(d => d.KorisnikId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rezervaci__Koris__7D439ABD");

            entity.HasOne(d => d.Smještaj).WithMany(p => p.Rezervacijes)
                .HasForeignKey(d => d.SmještajId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rezervaci__Smješ__7E37BEF6");
        });

        modelBuilder.Entity<Smještaj>(entity =>
        {
            entity.HasKey(e => e.SmještajId).HasName("PK__Smještaj__2567EE979E6BC3A2");

            entity.ToTable("Smještaj");

            entity.Property(e => e.Adresa).HasMaxLength(30);
            entity.Property(e => e.Email).HasMaxLength(30);
            entity.Property(e => e.Naziv).HasMaxLength(30);

            entity.HasOne(d => d.TipSmještaja).WithMany(p => p.Smještajs)
                .HasForeignKey(d => d.TipSmještajaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Smještaj__TipSmj__76969D2E");
        });

        modelBuilder.Entity<TipSmještaja>(entity =>
        {
            entity.HasKey(e => e.TipSmještajaId).HasName("PK__TipSmješ__9F5AFBE651AED897");

            entity.ToTable("TipSmještaja");

            entity.Property(e => e.TipSmještaja1)
                .HasMaxLength(20)
                .HasColumnName("TipSmještaja");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
