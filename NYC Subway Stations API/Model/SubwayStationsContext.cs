using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace NYC_Subway_Stations_API.Models
{
    public partial class SubwayStationsContext : DbContext
    {
        public SubwayStationsContext()
        {
        }

        public SubwayStationsContext(DbContextOptions<SubwayStationsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SubwayStation> SubwayStation { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserSubwayStation> UserSubwayStation { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Data Source=DESKTOP-H9K2OBI\\SQLEXPRESS;Initial Catalog=SubwayStations;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubwayStation>(entity =>
            {
                entity.HasKey(e => e.Objectid)
                    .HasName("PK__SubwaySt__DD7784169F254298");

                entity.Property(e => e.Objectid)
                    .HasColumnName("objectid")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Line)
                    .IsRequired()
                    .HasColumnName("line")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(34)
                    .IsUnicode(false);

                entity.Property(e => e.Notes)
                    .IsRequired()
                    .HasColumnName("notes")
                    .HasMaxLength(86)
                    .IsUnicode(false);

                entity.Property(e => e.TheGeomcoordinates0)
                    .HasColumnName("the_geomcoordinates0")
                    .HasColumnType("numeric(18, 14)");

                entity.Property(e => e.TheGeomcoordinates1)
                    .HasColumnName("the_geomcoordinates1")
                    .HasColumnType("numeric(18, 15)");

                entity.Property(e => e.TheGeomtype)
                    .IsRequired()
                    .HasColumnName("the_geomtype")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("url")
                    .HasMaxLength(33)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserSubwayStation>(entity =>
            {
                entity.HasKey(e => new { e.IdUser, e.IdSubwayStation })
                    .HasName("PK__UserSubw__B07AC63B189522D3");

                entity.Property(e => e.IdSubwayStation)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdSubwayStationNavigation)
                    .WithMany(p => p.UserSubwayStation)
                    .HasForeignKey(d => d.IdSubwayStation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubwayStation");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
