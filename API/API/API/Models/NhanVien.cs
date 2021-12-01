using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class NhanVien
    {
        public string MaNv { get; set; }
        public string TenNv { get; set; }
        public string Email { get; set; }
        public string Diachi { get; set; }
        public string Sdt { get; set; }
        public DateTime Ngaysinh { get; set; }
        public bool Phai { get; set; }
        public string Cmnd { get; set; }
        public string Matkhau { get; set; }
        public bool Trangthai { get; set; }
        public string Hinhanh { get; set; }
        public string MaChucVu { get; set; }

        public virtual ChucVu MaChucVuNavigation { get; set; }
    }
}
