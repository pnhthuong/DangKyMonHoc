using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class ChuongTrinhDaoTao
    {
        public ChuongTrinhDaoTao()
        {
            ChiTietCtdts = new HashSet<ChiTietCtdt>();
            NienKhoas = new HashSet<NienKhoa>();
        }

        public string MaCtdt { get; set; }
        public string TenCtdt { get; set; }
        public string NienKhoa { get; set; }
        public int TongSoTinChi { get; set; }
        public string MaDt { get; set; }
        public string MaNganh { get; set; }

        public virtual HeDaoTao MaDtNavigation { get; set; }
        public virtual Nganh MaNganhNavigation { get; set; }
        public virtual ICollection<ChiTietCtdt> ChiTietCtdts { get; set; }
        public virtual ICollection<NienKhoa> NienKhoas { get; set; }
    }
}
