using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class PhucKhao
    {
        public PhucKhao()
        {
            ChiTietPhucKhaos = new HashSet<ChiTietPhucKhao>();
        }

        public string MaPhucKhao { get; set; }
        public DateTime Ngaykhoitao { get; set; }
        public string MaSv { get; set; }

        public virtual SinhVien MaSvNavigation { get; set; }
        public virtual ICollection<ChiTietPhucKhao> ChiTietPhucKhaos { get; set; }
    }
}
