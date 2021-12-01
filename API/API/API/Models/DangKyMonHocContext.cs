using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace API.Models
{
    public partial class DangKyMonHocContext : DbContext
    {
        public DangKyMonHocContext()
        {
        }

        public DangKyMonHocContext(DbContextOptions<DangKyMonHocContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BangDiem> BangDiems { get; set; }
        public virtual DbSet<ChiTietCtdt> ChiTietCtdts { get; set; }
        public virtual DbSet<ChiTietPdk> ChiTietPdks { get; set; }
        public virtual DbSet<ChiTietPhucKhao> ChiTietPhucKhaos { get; set; }
        public virtual DbSet<ChucVu> ChucVus { get; set; }
        public virtual DbSet<ChuongTrinhDaoTao> ChuongTrinhDaoTaos { get; set; }
        public virtual DbSet<CongDangKy> CongDangKies { get; set; }
        public virtual DbSet<GiangVien> GiangViens { get; set; }
        public virtual DbSet<HeDaoTao> HeDaoTaos { get; set; }
        public virtual DbSet<HocKyCtdt> HocKyCtdts { get; set; }
        public virtual DbSet<HocKyDkmh> HocKyDkmhs { get; set; }
        public virtual DbSet<Khoa> Khoas { get; set; }
        public virtual DbSet<KhoiKienThuc> KhoiKienThucs { get; set; }
        public virtual DbSet<Lop> Lops { get; set; }
        public virtual DbSet<LopMonHoc> LopMonHocs { get; set; }
        public virtual DbSet<LopMonHocGiangVien> LopMonHocGiangViens { get; set; }
        public virtual DbSet<MonHoc> MonHocs { get; set; }
        public virtual DbSet<MonHocDuocMo> MonHocDuocMos { get; set; }
        public virtual DbSet<NamHocDkmh> NamHocDkmhs { get; set; }
        public virtual DbSet<Nganh> Nganhs { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<NienKhoa> NienKhoas { get; set; }
        public virtual DbSet<NienKhoaCdk> NienKhoaCdks { get; set; }
        public virtual DbSet<PhieuDangKy> PhieuDangKies { get; set; }
        public virtual DbSet<PhucKhao> PhucKhaos { get; set; }
        public virtual DbSet<SinhVien> SinhViens { get; set; }
        public virtual DbSet<ThongTinMonHoc> ThongTinMonHocs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-9D8IC3BJ\\SQLEXPRESS;Database=DangKyMonHoc;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<BangDiem>(entity =>
            {
                entity.ToTable("BangDiem");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DiemCk).HasColumnName("diemCK");

                entity.Property(e => e.DiemGk).HasColumnName("diemGK");

                entity.Property(e => e.DiemQt).HasColumnName("diemQT");

                entity.Property(e => e.DiemTk1).HasColumnName("diemTK1");

                entity.Property(e => e.DiemTk2).HasColumnName("diemTK2");

                entity.Property(e => e.DiemTk3).HasColumnName("diemTK3");

                entity.Property(e => e.Ketqua).HasColumnName("ketqua");

                entity.Property(e => e.MaLmh)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("maLMH")
                    .IsFixedLength(true);

                entity.Property(e => e.Masv)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("masv")
                    .IsFixedLength(true);

                entity.Property(e => e.Trangthai).HasColumnName("trangthai");

                entity.HasOne(d => d.MaLmhNavigation)
                    .WithMany(p => p.BangDiems)
                    .HasForeignKey(d => d.MaLmh)
                    .HasConstraintName("fk_BD_LMH");

                entity.HasOne(d => d.MasvNavigation)
                    .WithMany(p => p.BangDiems)
                    .HasForeignKey(d => d.Masv)
                    .HasConstraintName("fk_BD_SV");
            });

            modelBuilder.Entity<ChiTietCtdt>(entity =>
            {
                entity.ToTable("ChiTietCTDT");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MaCtdt)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maCTDT")
                    .IsFixedLength(true);

                entity.Property(e => e.MaHk)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maHK")
                    .IsFixedLength(true);

                entity.Property(e => e.MaMh)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maMH")
                    .IsFixedLength(true);

                entity.HasOne(d => d.MaCtdtNavigation)
                    .WithMany(p => p.ChiTietCtdts)
                    .HasForeignKey(d => d.MaCtdt)
                    .HasConstraintName("fk_CTCTDT_CTDT");

                entity.HasOne(d => d.MaHkNavigation)
                    .WithMany(p => p.ChiTietCtdts)
                    .HasForeignKey(d => d.MaHk)
                    .HasConstraintName("fk_CTCTDT_HocKy_CTDT");

                entity.HasOne(d => d.MaMhNavigation)
                    .WithMany(p => p.ChiTietCtdts)
                    .HasForeignKey(d => d.MaMh)
                    .HasConstraintName("fk_CTCTDT_MonHoc");
            });

            modelBuilder.Entity<ChiTietPdk>(entity =>
            {
                entity.ToTable("ChiTietPDK");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MaMh)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maMH")
                    .IsFixedLength(true);

                entity.Property(e => e.MaPdk).HasColumnName("maPDK");

                entity.Property(e => e.Trangthai).HasColumnName("trangthai");

                entity.HasOne(d => d.MaMhNavigation)
                    .WithMany(p => p.ChiTietPdks)
                    .HasForeignKey(d => d.MaMh)
                    .HasConstraintName("fk_CTPDK_MonHoc");

                entity.HasOne(d => d.MaPdkNavigation)
                    .WithMany(p => p.ChiTietPdks)
                    .HasForeignKey(d => d.MaPdk)
                    .HasConstraintName("fk_Chitietpdk_PDK");
            });

            modelBuilder.Entity<ChiTietPhucKhao>(entity =>
            {
                entity.HasKey(e => e.IdBd)
                    .HasName("PK__ChiTietP__8B6207EC835D1D54");

                entity.ToTable("ChiTietPhucKhao");

                entity.Property(e => e.IdBd).HasColumnName("ID_BD");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MaPhucKhao)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maPhucKhao")
                    .IsFixedLength(true);

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.ChiTietPhucKhaos)
                    .HasForeignKey(d => d.Id)
                    .HasConstraintName("fk_CTPK_CTBD");

                entity.HasOne(d => d.MaPhucKhaoNavigation)
                    .WithMany(p => p.ChiTietPhucKhaos)
                    .HasForeignKey(d => d.MaPhucKhao)
                    .HasConstraintName("fk_CTPK_PK");
            });

            modelBuilder.Entity<ChucVu>(entity =>
            {
                entity.HasKey(e => e.MaChucVu)
                    .HasName("PK__ChucVu__6E42BCD92DD016C7");

                entity.ToTable("ChucVu");

                entity.Property(e => e.MaChucVu)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maChucVu")
                    .IsFixedLength(true);

                entity.Property(e => e.TenChucVu)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("tenChucVu");
            });

            modelBuilder.Entity<ChuongTrinhDaoTao>(entity =>
            {
                entity.HasKey(e => e.MaCtdt)
                    .HasName("PK__ChuongTr__FD2652EA856C5123");

                entity.ToTable("ChuongTrinhDaoTao");

                entity.Property(e => e.MaCtdt)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maCTDT")
                    .IsFixedLength(true);

                entity.Property(e => e.MaDt)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maDT")
                    .IsFixedLength(true);

                entity.Property(e => e.MaNganh)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("maNganh")
                    .IsFixedLength(true);

                entity.Property(e => e.NienKhoa)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("nienKhoa")
                    .IsFixedLength(true);

                entity.Property(e => e.TenCtdt)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("tenCTDT");

                entity.Property(e => e.TongSoTinChi).HasColumnName("tongSoTinChi");

                entity.HasOne(d => d.MaDtNavigation)
                    .WithMany(p => p.ChuongTrinhDaoTaos)
                    .HasForeignKey(d => d.MaDt)
                    .HasConstraintName("fk_CTDT_HDT");

                entity.HasOne(d => d.MaNganhNavigation)
                    .WithMany(p => p.ChuongTrinhDaoTaos)
                    .HasForeignKey(d => d.MaNganh)
                    .HasConstraintName("fk_CTDT_Nganh");
            });

            modelBuilder.Entity<CongDangKy>(entity =>
            {
                entity.HasKey(e => e.MaCdk)
                    .HasName("PK__CongDang__2C863B0136AD1C55");

                entity.ToTable("CongDangKy");

                entity.Property(e => e.MaCdk)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maCDK")
                    .IsFixedLength(true);

                entity.Property(e => e.MaHk)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("maHK")
                    .IsFixedLength(true);

                entity.Property(e => e.MaNh)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("maNH")
                    .IsFixedLength(true);

                entity.Property(e => e.TenCdk)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("tenCDK");

                entity.Property(e => e.ThoigianBatDau)
                    .HasColumnType("datetime")
                    .HasColumnName("thoigianBatDau");

                entity.Property(e => e.ThoigianKetThuc)
                    .HasColumnType("datetime")
                    .HasColumnName("thoigianKetThuc");

                entity.Property(e => e.Trangthai).HasColumnName("trangthai");

                entity.HasOne(d => d.MaHkNavigation)
                    .WithMany(p => p.CongDangKies)
                    .HasForeignKey(d => d.MaHk)
                    .HasConstraintName("FK_CDK_HK");

                entity.HasOne(d => d.MaNhNavigation)
                    .WithMany(p => p.CongDangKies)
                    .HasForeignKey(d => d.MaNh)
                    .HasConstraintName("fk_CDK_NH");
            });

            modelBuilder.Entity<GiangVien>(entity =>
            {
                entity.HasKey(e => e.MaGv)
                    .HasName("PK__GiangVie__7A3E2D755023712D");

                entity.ToTable("GiangVien");

                entity.Property(e => e.MaGv)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maGV")
                    .IsFixedLength(true);

                entity.Property(e => e.Cmnd)
                    .IsRequired()
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("cmnd")
                    .IsFixedLength(true);

                entity.Property(e => e.Diachi)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("diachi");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("email")
                    .IsFixedLength(true);

                entity.Property(e => e.Hinhanh)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("hinhanh")
                    .IsFixedLength(true);

                entity.Property(e => e.Hocham)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("hocham");

                entity.Property(e => e.MaChucVu)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maChucVu")
                    .IsFixedLength(true);

                entity.Property(e => e.MaKhoa)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maKhoa")
                    .IsFixedLength(true);

                entity.Property(e => e.Matkhau)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("matkhau")
                    .IsFixedLength(true);

                entity.Property(e => e.Ngaysinh)
                    .HasColumnType("date")
                    .HasColumnName("ngaysinh");

                entity.Property(e => e.Phai).HasColumnName("phai");

                entity.Property(e => e.Sdt)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("sdt")
                    .IsFixedLength(true);

                entity.Property(e => e.TenGv)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("tenGV");

                entity.Property(e => e.Trangthai).HasColumnName("trangthai");

                entity.HasOne(d => d.MaChucVuNavigation)
                    .WithMany(p => p.GiangViens)
                    .HasForeignKey(d => d.MaChucVu)
                    .HasConstraintName("fk_GiangVien_ChucVu");

                entity.HasOne(d => d.MaKhoaNavigation)
                    .WithMany(p => p.GiangViens)
                    .HasForeignKey(d => d.MaKhoa)
                    .HasConstraintName("fk_GiangVien_Khoa");
            });

            modelBuilder.Entity<HeDaoTao>(entity =>
            {
                entity.HasKey(e => e.MaDt)
                    .HasName("PK__HeDaoTao__7A3EF4138C679863");

                entity.ToTable("HeDaoTao");

                entity.Property(e => e.MaDt)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maDT")
                    .IsFixedLength(true);

                entity.Property(e => e.TenDt)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("tenDT");
            });

            modelBuilder.Entity<HocKyCtdt>(entity =>
            {
                entity.HasKey(e => e.MaHk)
                    .HasName("PK__HocKy_CT__7A3E1489BACE1F92");

                entity.ToTable("HocKy_CTDT");

                entity.Property(e => e.MaHk)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maHK")
                    .IsFixedLength(true);

                entity.Property(e => e.TenHk)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("tenHK");
            });

            modelBuilder.Entity<HocKyDkmh>(entity =>
            {
                entity.HasKey(e => e.MaHk)
                    .HasName("PK__HocKy_DK__7A3E148981A9A8F6");

                entity.ToTable("HocKy_DKMH");

                entity.Property(e => e.MaHk)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("maHK")
                    .IsFixedLength(true);

                entity.Property(e => e.TenHk)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("tenHK");
            });

            modelBuilder.Entity<Khoa>(entity =>
            {
                entity.HasKey(e => e.MaKhoa)
                    .HasName("PK__Khoa__C79B8C2294AE3216");

                entity.ToTable("Khoa");

                entity.Property(e => e.MaKhoa)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maKhoa")
                    .IsFixedLength(true);

                entity.Property(e => e.TenKhoa)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("tenKhoa");

                entity.Property(e => e.TenVietTat)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("tenVietTat")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<KhoiKienThuc>(entity =>
            {
                entity.HasKey(e => e.MaKhoi)
                    .HasName("PK__KhoiKien__C79B8C2A6399D157");

                entity.ToTable("KhoiKienThuc");

                entity.Property(e => e.MaKhoi)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maKhoi")
                    .IsFixedLength(true);

                entity.Property(e => e.Batbuoc).HasColumnName("batbuoc");

                entity.Property(e => e.TenChuyenMon)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("tenChuyenMon");

                entity.Property(e => e.TenKhoi)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("tenKhoi");
            });

            modelBuilder.Entity<Lop>(entity =>
            {
                entity.HasKey(e => e.MaLop)
                    .HasName("PK__Lop__261ECAE307BD3BCD");

                entity.ToTable("Lop");

                entity.Property(e => e.MaLop)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maLop")
                    .IsFixedLength(true);

                entity.Property(e => e.MaNganh)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("maNganh")
                    .IsFixedLength(true);

                entity.Property(e => e.MaNk)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maNK")
                    .IsFixedLength(true);

                entity.Property(e => e.Siso).HasColumnName("siso");

                entity.Property(e => e.TenLop)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("tenLop")
                    .IsFixedLength(true);

                entity.HasOne(d => d.MaNganhNavigation)
                    .WithMany(p => p.Lops)
                    .HasForeignKey(d => d.MaNganh)
                    .HasConstraintName("fk_Lop_Nganh");

                entity.HasOne(d => d.MaNkNavigation)
                    .WithMany(p => p.Lops)
                    .HasForeignKey(d => d.MaNk)
                    .HasConstraintName("fk_Lop_NienKhoa");
            });

            modelBuilder.Entity<LopMonHoc>(entity =>
            {
                entity.HasKey(e => e.MaLmh)
                    .HasName("PK__Lop_MonH__26213EF1DC4EC933");

                entity.ToTable("Lop_MonHoc");

                entity.Property(e => e.MaLmh)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("maLMH")
                    .IsFixedLength(true);

                entity.Property(e => e.MaCdk)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maCDK")
                    .IsFixedLength(true);

                entity.Property(e => e.MaMh)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maMH")
                    .IsFixedLength(true);

                entity.Property(e => e.Siso).HasColumnName("siso");

                entity.Property(e => e.TenLmh)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("tenLMH");

                entity.HasOne(d => d.MaCdkNavigation)
                    .WithMany(p => p.LopMonHocs)
                    .HasForeignKey(d => d.MaCdk)
                    .HasConstraintName("fk_LMH_CDK");

                entity.HasOne(d => d.MaMhNavigation)
                    .WithMany(p => p.LopMonHocs)
                    .HasForeignKey(d => d.MaMh)
                    .HasConstraintName("fk_LMH_MH");
            });

            modelBuilder.Entity<LopMonHocGiangVien>(entity =>
            {
                entity.ToTable("LopMonHoc_GiangVien");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MaGv)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maGV")
                    .IsFixedLength(true);

                entity.Property(e => e.MaLmh)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("maLMH")
                    .IsFixedLength(true);

                entity.HasOne(d => d.MaGvNavigation)
                    .WithMany(p => p.LopMonHocGiangViens)
                    .HasForeignKey(d => d.MaGv)
                    .HasConstraintName("fk_LMHGV_GV");

                entity.HasOne(d => d.MaLmhNavigation)
                    .WithMany(p => p.LopMonHocGiangViens)
                    .HasForeignKey(d => d.MaLmh)
                    .HasConstraintName("fk_LMHGV_LMH");
            });

            modelBuilder.Entity<MonHoc>(entity =>
            {
                entity.HasKey(e => e.MaMh)
                    .HasName("PK__MonHoc__7A3EDFA6DC33FB0B");

                entity.ToTable("MonHoc");

                entity.Property(e => e.MaMh)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maMH")
                    .IsFixedLength(true);

                entity.Property(e => e.HesoHp).HasColumnName("hesoHP");

                entity.Property(e => e.MaKhoi)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maKhoi")
                    .IsFixedLength(true);

                entity.Property(e => e.MaSh)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maSH")
                    .IsFixedLength(true);

                entity.Property(e => e.MaTq)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maTQ")
                    .IsFixedLength(true);

                entity.Property(e => e.Sotinchi).HasColumnName("sotinchi");

                entity.Property(e => e.TenMh)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("tenMH");

                entity.Property(e => e.Thuchanh).HasColumnName("thuchanh");

                entity.HasOne(d => d.MaKhoiNavigation)
                    .WithMany(p => p.MonHocs)
                    .HasForeignKey(d => d.MaKhoi)
                    .HasConstraintName("fk_MonHoc_KhoiKienThuc");
            });

            modelBuilder.Entity<MonHocDuocMo>(entity =>
            {
                entity.ToTable("MonHocDuocMo");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MaCdk)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maCDK")
                    .IsFixedLength(true);

                entity.Property(e => e.MaMh)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maMH")
                    .IsFixedLength(true);

                entity.Property(e => e.NkapDung)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("NKApDung")
                    .IsFixedLength(true);

                entity.Property(e => e.Soluong).HasColumnName("soluong");

                entity.Property(e => e.Trangthai).HasColumnName("trangthai");

                entity.HasOne(d => d.MaCdkNavigation)
                    .WithMany(p => p.MonHocDuocMos)
                    .HasForeignKey(d => d.MaCdk)
                    .HasConstraintName("fk_MHDM_CDK");

                entity.HasOne(d => d.MaMhNavigation)
                    .WithMany(p => p.MonHocDuocMos)
                    .HasForeignKey(d => d.MaMh)
                    .HasConstraintName("fk_MHDM_MH");
            });

            modelBuilder.Entity<NamHocDkmh>(entity =>
            {
                entity.HasKey(e => e.MaNh)
                    .HasName("PK__NamHoc_D__7A3EC7C7E2E548DD");

                entity.ToTable("NamHoc_DKMH");

                entity.Property(e => e.MaNh)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("maNH")
                    .IsFixedLength(true);

                entity.Property(e => e.TenNh)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("tenNH");
            });

            modelBuilder.Entity<Nganh>(entity =>
            {
                entity.HasKey(e => e.MaNganh)
                    .HasName("PK__Nganh__4E0C0217CE59A88C");

                entity.ToTable("Nganh");

                entity.Property(e => e.MaNganh)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("maNganh")
                    .IsFixedLength(true);

                entity.Property(e => e.MaKhoa)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maKhoa")
                    .IsFixedLength(true);

                entity.Property(e => e.TenNganh)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("tenNganh");

                entity.HasOne(d => d.MaKhoaNavigation)
                    .WithMany(p => p.Nganhs)
                    .HasForeignKey(d => d.MaKhoa)
                    .HasConstraintName("fk_Nganh_Khoa");
            });

            modelBuilder.Entity<NhanVien>(entity =>
            {
                entity.HasKey(e => e.MaNv)
                    .HasName("PK__NhanVien__7A3EC7D53C3C2A51");

                entity.ToTable("NhanVien");

                entity.Property(e => e.MaNv)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maNV")
                    .IsFixedLength(true);

                entity.Property(e => e.Cmnd)
                    .IsRequired()
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("cmnd")
                    .IsFixedLength(true);

                entity.Property(e => e.Diachi)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("diachi");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("email")
                    .IsFixedLength(true);

                entity.Property(e => e.Hinhanh)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("hinhanh")
                    .IsFixedLength(true);

                entity.Property(e => e.MaChucVu)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maChucVu")
                    .IsFixedLength(true);

                entity.Property(e => e.Matkhau)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("matkhau")
                    .IsFixedLength(true);

                entity.Property(e => e.Ngaysinh)
                    .HasColumnType("date")
                    .HasColumnName("ngaysinh");

                entity.Property(e => e.Phai).HasColumnName("phai");

                entity.Property(e => e.Sdt)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("sdt")
                    .IsFixedLength(true);

                entity.Property(e => e.TenNv)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("tenNV");

                entity.Property(e => e.Trangthai).HasColumnName("trangthai");

                entity.HasOne(d => d.MaChucVuNavigation)
                    .WithMany(p => p.NhanViens)
                    .HasForeignKey(d => d.MaChucVu)
                    .HasConstraintName("fk_NhanVien_ChucVu");
            });

            modelBuilder.Entity<NienKhoa>(entity =>
            {
                entity.HasKey(e => e.MaNk)
                    .HasName("PK__NienKhoa__7A3EC7C24328A450");

                entity.ToTable("NienKhoa");

                entity.Property(e => e.MaNk)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maNK")
                    .IsFixedLength(true);

                entity.Property(e => e.MaCtdt)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maCTDT")
                    .IsFixedLength(true);

                entity.Property(e => e.TenNk)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("tenNK");

                entity.HasOne(d => d.MaCtdtNavigation)
                    .WithMany(p => p.NienKhoas)
                    .HasForeignKey(d => d.MaCtdt)
                    .HasConstraintName("fk_nienkhoa_CTDT");
            });

            modelBuilder.Entity<NienKhoaCdk>(entity =>
            {
                entity.ToTable("NienKhoa_CDK");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MaCdk)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maCDK")
                    .IsFixedLength(true);

                entity.Property(e => e.MaNk)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maNK")
                    .IsFixedLength(true);

                entity.HasOne(d => d.MaCdkNavigation)
                    .WithMany(p => p.NienKhoaCdks)
                    .HasForeignKey(d => d.MaCdk)
                    .HasConstraintName("fk_NKCDK_CDK");

                entity.HasOne(d => d.MaNkNavigation)
                    .WithMany(p => p.NienKhoaCdks)
                    .HasForeignKey(d => d.MaNk)
                    .HasConstraintName("fk_NKCDK_NK");
            });

            modelBuilder.Entity<PhieuDangKy>(entity =>
            {
                entity.HasKey(e => e.MaPdk)
                    .HasName("PK__PhieuDan__2719D8426538DAA2");

                entity.ToTable("PhieuDangKy");

                entity.Property(e => e.MaPdk).HasColumnName("maPDK");

                entity.Property(e => e.MaCdk)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maCDK")
                    .IsFixedLength(true);

                entity.Property(e => e.MaSv)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("maSV")
                    .IsFixedLength(true);

                entity.Property(e => e.Ngaychinhsua)
                    .HasColumnType("date")
                    .HasColumnName("ngaychinhsua");

                entity.Property(e => e.Ngaylap)
                    .HasColumnType("date")
                    .HasColumnName("ngaylap");

                entity.HasOne(d => d.MaCdkNavigation)
                    .WithMany(p => p.PhieuDangKies)
                    .HasForeignKey(d => d.MaCdk)
                    .HasConstraintName("fk_PDK_CDK");

                entity.HasOne(d => d.MaSvNavigation)
                    .WithMany(p => p.PhieuDangKies)
                    .HasForeignKey(d => d.MaSv)
                    .HasConstraintName("fk_pdk_sv");
            });

            modelBuilder.Entity<PhucKhao>(entity =>
            {
                entity.HasKey(e => e.MaPhucKhao)
                    .HasName("PK__PhucKhao__3487CF8AA1E789AE");

                entity.ToTable("PhucKhao");

                entity.Property(e => e.MaPhucKhao)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maPhucKhao")
                    .IsFixedLength(true);

                entity.Property(e => e.MaSv)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("maSV")
                    .IsFixedLength(true);

                entity.Property(e => e.Ngaykhoitao)
                    .HasColumnType("date")
                    .HasColumnName("ngaykhoitao");

                entity.HasOne(d => d.MaSvNavigation)
                    .WithMany(p => p.PhucKhaos)
                    .HasForeignKey(d => d.MaSv)
                    .HasConstraintName("fk_phuckhao_sinhvien");
            });

            modelBuilder.Entity<SinhVien>(entity =>
            {
                entity.HasKey(e => e.MaSv)
                    .HasName("PK__SinhVien__7A227A645225EFDE");

                entity.ToTable("SinhVien");

                entity.Property(e => e.MaSv)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("maSV")
                    .IsFixedLength(true);

                entity.Property(e => e.Cnmd)
                    .IsRequired()
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("cnmd")
                    .IsFixedLength(true);

                entity.Property(e => e.Diachi)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("diachi");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email")
                    .IsFixedLength(true);

                entity.Property(e => e.Hinhanh)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("hinhanh")
                    .IsFixedLength(true);

                entity.Property(e => e.MaLop)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maLop")
                    .IsFixedLength(true);

                entity.Property(e => e.Matkhau)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("matkhau")
                    .IsFixedLength(true);

                entity.Property(e => e.Ngaysinh)
                    .HasColumnType("date")
                    .HasColumnName("ngaysinh");

                entity.Property(e => e.Phai).HasColumnName("phai");

                entity.Property(e => e.Sdt)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("sdt")
                    .IsFixedLength(true);

                entity.Property(e => e.TenSv)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("tenSV");

                entity.Property(e => e.Trangthai).HasColumnName("trangthai");

                entity.HasOne(d => d.MaLopNavigation)
                    .WithMany(p => p.SinhViens)
                    .HasForeignKey(d => d.MaLop)
                    .HasConstraintName("fk_SinhVien_Lop");
            });

            modelBuilder.Entity<ThongTinMonHoc>(entity =>
            {
                entity.HasKey(e => e.MaTtmh)
                    .HasName("PK__ThongTin__27FBEBE70F17DCBB");

                entity.ToTable("ThongTinMonHoc");

                entity.Property(e => e.MaTtmh)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("maTTMH")
                    .IsFixedLength(true);

                entity.Property(e => e.PhantramCk).HasColumnName("phantramCK");

                entity.Property(e => e.PhantramGk).HasColumnName("phantramGK");

                entity.Property(e => e.PhantramQt).HasColumnName("phantramQT");

                entity.Property(e => e.Sotiet).HasColumnName("sotiet");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
