using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace SklepElektroniczny1501
{
    public partial class DtoContex : DbContext
    {
        public DtoContex()
            : base("name=DtoContex")
        {
        }

        public virtual DbSet<kategoria> kategoria { get; set; }
        public virtual DbSet<produkt> produkt { get; set; }
        public virtual DbSet<produkt_kategoria> produkt_kategoria { get; set; }
        public virtual DbSet<zamowienie> zamowienie { get; set; }
        public virtual DbSet<zamowienie_produkt> zamowienie_produkt { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<kategoria>()
                .HasMany(e => e.produkt_kategoria)
                .WithOptional(e => e.kategoria)
                .HasForeignKey(e => e.id_kategoria);

            modelBuilder.Entity<produkt>()
                .Property(e => e.nazwa)
                .IsUnicode(false);

            modelBuilder.Entity<produkt>()
                .Property(e => e.model)
                .IsUnicode(false);

            modelBuilder.Entity<produkt>()
                .Property(e => e.opis)
                .IsUnicode(false);

            modelBuilder.Entity<produkt>()
                .Property(e => e.cena)
                .HasPrecision(10, 2);

            modelBuilder.Entity<produkt>()
                .HasMany(e => e.produkt_kategoria)
                .WithOptional(e => e.produkt)
                .HasForeignKey(e => e.id_produkt);

            modelBuilder.Entity<produkt>()
                .HasMany(e => e.zamowienie_produkt)
                .WithOptional(e => e.produkt)
                .HasForeignKey(e => e.id_produkt);

            modelBuilder.Entity<zamowienie>()
                .Property(e => e.numer_zamowienia)
                .IsUnicode(false);

            modelBuilder.Entity<zamowienie>()
                .HasMany(e => e.zamowienie_produkt)
                .WithOptional(e => e.zamowienie)
                .HasForeignKey(e => e.id_zamowienie);

            modelBuilder.Entity<zamowienie_produkt>()
                .Property(e => e.cena)
                .HasPrecision(10, 2);
        }
    }
}
