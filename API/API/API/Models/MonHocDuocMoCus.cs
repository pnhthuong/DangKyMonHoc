using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class MonHocDuocMoCus
    {
        public int Id { get; set; }
        public int Soluong { get; set; }
        public bool Trangthai { get; set; }
        public string MaCdk { get; set; }
        public string MaMh { get; set; }
        public string NkapDung { get; set; }
        public bool trangthaitaolop { get; set; }
    }
}
