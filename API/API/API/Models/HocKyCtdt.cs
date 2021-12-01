using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class HocKyCtdt
    {
        public HocKyCtdt()
        {
            ChiTietCtdts = new HashSet<ChiTietCtdt>();
        }

        public string MaHk { get; set; }
        public string TenHk { get; set; }

        public virtual ICollection<ChiTietCtdt> ChiTietCtdts { get; set; }
    }
}
