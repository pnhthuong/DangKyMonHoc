using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class ChiTietPdk
    {
        public int Id { get; set; }
        public string MaMh { get; set; }
        public bool Trangthai { get; set; }
        public int? MaPdk { get; set; }

        public virtual MonHoc MaMhNavigation { get; set; }
        public virtual PhieuDangKy MaPdkNavigation { get; set; }
    }
}
