using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TreasureL.Models
{
    public partial class TreasureLContext : DbContext
    {
        public TreasureLContext()
        {
        }

        public TreasureLContext(DbContextOptions<TreasureLContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ctdh> Ctdhs { get; set; }
        public virtual DbSet<DonHang> DonHangs { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<LoaiSp> LoaiSps { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<ThanhToan> ThanhToans { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=TUFA17;Initial Catalog=TreasureL;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Ctdh>(entity =>
            {
                entity.HasKey(e => e.MaCtdh);

                entity.ToTable("CTDH");

                entity.Property(e => e.MaCtdh).HasColumnName("MaCTDH");

                entity.Property(e => e.MaDh).HasColumnName("MaDH");

                entity.HasOne(d => d.MaDhNavigation)
                    .WithMany(p => p.Ctdhs)
                    .HasForeignKey(d => d.MaDh)
                    .HasConstraintName("FK_CTDH_DonHang");

                entity.HasOne(d => d.MaSpNavigation)
                    .WithMany(p => p.Ctdhs)
                    .HasForeignKey(d => d.MaSp)
                    .HasConstraintName("FK_CTDH_SanPham");
            });

            modelBuilder.Entity<DonHang>(entity =>
            {
                entity.HasKey(e => e.MaDh);

                entity.ToTable("DonHang");

                entity.Property(e => e.MaDh).HasColumnName("MaDH");

                entity.Property(e => e.MaKh).HasColumnName("MaKH");

                entity.Property(e => e.MaNv).HasColumnName("MaNV");

                entity.Property(e => e.MaTt).HasColumnName("MaTT");

                entity.Property(e => e.NgayGh)
                    .HasColumnType("datetime")
                    .HasColumnName("NgayGH");

                entity.Property(e => e.NgayLapHd)
                    .HasColumnType("datetime")
                    .HasColumnName("NgayLapHD");

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.DonHangs)
                    .HasForeignKey(d => d.MaKh)
                    .HasConstraintName("FK_DonHang_KhachHang");

                entity.HasOne(d => d.MaNvNavigation)
                    .WithMany(p => p.DonHangs)
                    .HasForeignKey(d => d.MaNv)
                    .HasConstraintName("FK_DonHang_NhanVien");

                entity.HasOne(d => d.MaTtNavigation)
                    .WithMany(p => p.DonHangs)
                    .HasForeignKey(d => d.MaTt)
                    .HasConstraintName("FK_DonHang_ThanhToan1");
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.HasKey(e => e.MaKh);

                entity.ToTable("KhachHang");

                entity.Property(e => e.MaKh).HasColumnName("MaKH");

                entity.Property(e => e.DiaChi).HasMaxLength(200);

                entity.Property(e => e.DienThoai)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GioiTinh).HasMaxLength(50);

                entity.Property(e => e.HoKh)
                    .HasMaxLength(50)
                    .HasColumnName("HoKH");

                entity.Property(e => e.MatKhau)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.TenDangNhap)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TenKh)
                    .HasMaxLength(50)
                    .HasColumnName("TenKH");
            });

            modelBuilder.Entity<LoaiSp>(entity =>
            {
                entity.HasKey(e => e.MaLoaiSp);

                entity.ToTable("LoaiSP");

                entity.Property(e => e.TenLoaiSp).HasMaxLength(50);
            });

            modelBuilder.Entity<NhanVien>(entity =>
            {
                entity.HasKey(e => e.MaNv);

                entity.ToTable("NhanVien");

                entity.Property(e => e.MaNv).HasColumnName("MaNV");

                entity.Property(e => e.DiaChi).HasMaxLength(200);

                entity.Property(e => e.DienThoai)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HoNv)
                    .HasMaxLength(50)
                    .HasColumnName("HoNV");

                entity.Property(e => e.MatKhau)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.TenDangNhap)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TenNv)
                    .HasMaxLength(50)
                    .HasColumnName("TenNV");
            });

            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.HasKey(e => e.MaSp);

                entity.ToTable("SanPham");

                entity.Property(e => e.MaSp).HasColumnName("MaSP");

                entity.Property(e => e.DonViTinh)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.HinhSp)
                    .IsUnicode(false)
                    .HasColumnName("HinhSP");

                entity.Property(e => e.TenSp)
                    .HasMaxLength(200)
                    .HasColumnName("TenSP");

                entity.HasOne(d => d.MaLoaiSpNavigation)
                    .WithMany(p => p.SanPhams)
                    .HasForeignKey(d => d.MaLoaiSp)
                    .HasConstraintName("FK_SanPham_LoaiSP");
            });

            modelBuilder.Entity<ThanhToan>(entity =>
            {
                entity.HasKey(e => e.MaTt);

                entity.ToTable("ThanhToan");

                entity.Property(e => e.MaTt).HasColumnName("MaTT");

                entity.Property(e => e.HinhThucTt)
                    .HasMaxLength(100)
                    .HasColumnName("HinhThucTT");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
