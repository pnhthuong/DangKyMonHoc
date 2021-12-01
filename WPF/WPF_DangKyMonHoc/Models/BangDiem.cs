using System;
using System.Collections.Generic;

#nullable disable

namespace Wpf_DangKyMonHoc.Models
{
    public partial class BangDiem
    {


        public int Id { get; set; }
        public string MaLmh { get; set; }
        public bool Trangthai { get; set; }
        public double? DiemGk { get; set; }
        public double? DiemCk { get; set; }
        public double? DiemQt { get; set; }
        public double? DiemTk1 { get; set; }
        public double? DiemTk2 { get; set; }
        public double? DiemTk3 { get; set; }
        public bool? Ketqua { get; set; }
        public string Masv { get; set; }

    }
}
