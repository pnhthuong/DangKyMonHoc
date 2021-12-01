using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class NienKhoa
    {
        public NienKhoa()
        {
            Lops = new HashSet<Lop>();
            NienKhoaCdks = new HashSet<NienKhoaCdk>();
        }

        public string MaNk { get; set; }
        public string TenNk { get; set; }
        public string MaCtdt { get; set; }

        public virtual ChuongTrinhDaoTao MaCtdtNavigation { get; set; }
        public virtual ICollection<Lop> Lops { get; set; }
        public virtual ICollection<NienKhoaCdk> NienKhoaCdks { get; set; }
    }
}
