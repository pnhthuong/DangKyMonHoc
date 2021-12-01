using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class ChiTietPhucKhao
    {
        public int IdBd { get; set; }
        public int? Id { get; set; }
        public string MaPhucKhao { get; set; }

        public virtual BangDiem IdNavigation { get; set; }
        public virtual PhucKhao MaPhucKhaoNavigation { get; set; }
    }
}
