using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class ChiTietCtdt
    {
        public int Id { get; set; }
        public string MaMh { get; set; }
        public string MaCtdt { get; set; }
        public string MaHk { get; set; }

        public virtual ChuongTrinhDaoTao MaCtdtNavigation { get; set; }
        public virtual HocKyCtdt MaHkNavigation { get; set; }
        public virtual MonHoc MaMhNavigation { get; set; }
    }
}
