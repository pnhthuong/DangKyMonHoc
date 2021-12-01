using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Wpf_DangKyMonHoc.Models;

namespace Wpf_DangKyMonHoc.XuLy
{
    class Xuly_Chung
    {

        public string local()
        {
            return @"https://localhost:44319/api/";
        }
        public void textNumber(TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public bool isValidEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        public List<GioiTinh> getGT()
        {
            List<GioiTinh> gt = new List<GioiTinh>();
            gt.Add(new GioiTinh { ID = true, TenGT = "Nam" });
            gt.Add(new GioiTinh { ID = false, TenGT = "Nữ" });
            return gt;
        }

        public List<HocHam> GetHocHams()
        {
            List<HocHam> hh = new List<HocHam>();
            hh.Add(new HocHam { ID = 1, tenHocHam = "Cử Nhân" });
            hh.Add(new HocHam { ID = 2, tenHocHam = "Thạc Sĩ" });
            hh.Add(new HocHam { ID = 3, tenHocHam = "Tiến Sĩ" });
            //hh.Add(new HocHam { ID = 4, tenHocHam = "Tiến Sĩ Khoa Học" });
            hh.Add(new HocHam { ID = 4, tenHocHam = "PGS.Tiến Sĩ" });
            hh.Add(new HocHam { ID = 5, tenHocHam = "GS.Tiến Sĩ" });
            return hh;
        }

        public HocHam hienthihh(string ten)
        {
            List<HocHam> list = GetHocHams();
            HocHam hh = list.Find(x => x.tenHocHam == ten);
            if (hh == null)
            {
                return null;
            }
            return hh;
        }

        public bool checkdatengaysinhsv(DateTime ngaysinh)
        {
            DateTime now = DateTime.Now;
            int tuoi = int.Parse(now.Year.ToString()) - int.Parse(ngaysinh.Year.ToString());
            if (tuoi < 18)
                return false;
            return true;
        }

        public bool checkdatengaysinhnv(DateTime ngaysinh)
        {
            DateTime now = DateTime.Now;
            int tuoi = int.Parse(now.Year.ToString()) - int.Parse(ngaysinh.Year.ToString());
            if (tuoi < 22)
                return false;
            return true;
        }

        public string resetpass(DateTime ngaysinh)
        {
            return ngaysinh.Day.ToString() + ngaysinh.Month.ToString() + ngaysinh.Year.ToString();//1061999
        }

        public List<BatBuoc> getThuocTinh()
        {
            List<BatBuoc> thuoctinh = new List<BatBuoc>();
            thuoctinh.Add(new BatBuoc { ID = true, TenThuocTinh = "Có" });
            thuoctinh.Add(new BatBuoc { ID = false, TenThuocTinh = "Không" });
            return thuoctinh;
        }

        public List<BatBuoc> getTrangThaiCong()
        {
            List<BatBuoc> thuoctinh = new List<BatBuoc>();
            thuoctinh.Add(new BatBuoc { ID = true, TenThuocTinh = "Hoạt động" });
            thuoctinh.Add(new BatBuoc { ID = false, TenThuocTinh = "Không hoạt động" });
            return thuoctinh;
        }

        public bool checksdt(string str)
        {
            int dem = str.Length;
            if (dem == 10) return true;
            return false;
        }

        public bool checkcmnd(string str)
        {
            int dem = str.Length;
            if (dem == 9 || dem == 12)
                return true;
            return false;
        }

        public bool checkthoigiancdk(DateTime timefrom, DateTime timeto)
        {
            var dem = timeto - timefrom;
            if (dem.Days < 0)
                return false;
            return true;
        }
    }
}
