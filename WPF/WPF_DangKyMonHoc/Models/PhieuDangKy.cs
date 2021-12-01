using System;
using System.Collections.Generic;

#nullable disable

namespace Wpf_DangKyMonHoc.Models
{
    public partial class PhieuDangKy
    {

        public PhieuDangKy()
        {
            ChiTietPdks = new HashSet<ChiTietPdk>();
        }

        public DateTime Ngaylap { get; set; }
        public DateTime? Ngaychinhsua { get; set; }
        public string MaSv { get; set; }
        public int MaPdk { get; set; }
        public string MaCdk { get; set; }

        public virtual CongDangKy MaCdkNavigation { get; set; }
        public virtual SinhVien MaSvNavigation { get; set; }
        public virtual ICollection<ChiTietPdk> ChiTietPdks { get; set; }

    }
}
