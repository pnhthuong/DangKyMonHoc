using System;
using System.Collections.Generic;

#nullable disable

namespace MVC_DangKyMonHoc.Models
{
    public partial class MonHoc
    {
        public MonHoc()
        {
            
            ChiTietPdks = new HashSet<ChiTietPdk>();
            LopMonHocs = new HashSet<LopMonHoc>();
            MonHocDuocMos = new HashSet<MonHocDuocMo>();
        }

        public string MaMh { get; set; }
        public string TenMh { get; set; }
        public byte Sotinchi { get; set; }
        public byte HesoHp { get; set; }
        public bool Thuchanh { get; set; }
        public string MaTq { get; set; }
        public string MaSh { get; set; }
        public string MaKhoi { get; set; }

      
        public virtual ICollection<ChiTietPdk> ChiTietPdks { get; set; }
        public virtual ICollection<LopMonHoc> LopMonHocs { get; set; }
        public virtual ICollection<MonHocDuocMo> MonHocDuocMos { get; set; }
    }
}
