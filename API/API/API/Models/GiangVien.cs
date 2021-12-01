using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class GiangVien
    {
        public GiangVien()
        {
            LopMonHocGiangViens = new HashSet<LopMonHocGiangVien>();
        }

        public string MaGv { get; set; }
        public string TenGv { get; set; }
        public string Email { get; set; }
        public string Diachi { get; set; }
        public string Sdt { get; set; }
        public string Hocham { get; set; }
        public bool Phai { get; set; }
        public string Cmnd { get; set; }
        public DateTime Ngaysinh { get; set; }
        public string Matkhau { get; set; }
        public bool Trangthai { get; set; }
        public string Hinhanh { get; set; }
        public string MaChucVu { get; set; }
        public string MaKhoa { get; set; }

        public virtual ChucVu MaChucVuNavigation { get; set; }
        public virtual Khoa MaKhoaNavigation { get; set; }
        public virtual ICollection<LopMonHocGiangVien> LopMonHocGiangViens { get; set; }
    }
}
