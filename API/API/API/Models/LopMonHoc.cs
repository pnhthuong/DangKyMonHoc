using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class LopMonHoc
    {
        public LopMonHoc()
        {
            BangDiems = new HashSet<BangDiem>();
            LopMonHocGiangViens = new HashSet<LopMonHocGiangVien>();
        }

        public string TenLmh { get; set; }
        public int Siso { get; set; }
        public string MaCdk { get; set; }
        public string MaMh { get; set; }
        public string MaLmh { get; set; }

        public virtual CongDangKy MaCdkNavigation { get; set; }
        public virtual MonHoc MaMhNavigation { get; set; }
        public virtual ICollection<BangDiem> BangDiems { get; set; }
        public virtual ICollection<LopMonHocGiangVien> LopMonHocGiangViens { get; set; }
    }
}
