using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Wpf_DangKyMonHoc.Models;

namespace Wpf_DangKyMonHoc.XuLy
{
    class XLLopMonHoc
    {
        private static HttpClient hc = new HttpClient();
        public static List<MonHocDuocMoCus> getdsmhdm(string macdk)
        {
            try
            {
                string url = @"https://localhost:44319/api/Lop_MonHoc/dsmhdm/" + macdk;
                var kq = hc.GetAsync(url);
                kq.Wait();
                if (kq.Result.IsSuccessStatusCode == false) return null;
                var list = kq.Result.Content.ReadAsAsync<List<MonHocDuocMoCus>>();
                list.Wait();
                return list.Result.ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }
        public static bool PostLopMonHoc(Lop_MonHoc_Custom a)
        {
            try
            {
                string url = @"https://localhost:44319/api/Lop_MonHoc/LapDSLopMonHoc";

                var kq = hc.PostAsJsonAsync(url, a);
                kq.Wait();
                return kq.Result.IsSuccessStatusCode;

            }
            catch (Exception)
            {
                return false;
            }
        }
        public static List<CongDangKy> getCDKCombo()
        {
            try
            {
                string url = @"https://localhost:44319/api/Lop_MonHoc/DSCDK/Combox";
                var kq = hc.GetAsync(url);
                kq.Wait();
                if (kq.Result.IsSuccessStatusCode == false) return null;
                var list = kq.Result.Content.ReadAsAsync<List<CongDangKy>>();
                list.Wait();
                return list.Result.ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }
        public static List<LopMonHoc> getDSLMHfromMa(string macdk)
        {
            try
            {
                string url = @"https://localhost:44319/api/Lop_MonHoc/DSLMH/"+macdk;
                var kq = hc.GetAsync(url);
                kq.Wait();
                if (kq.Result.IsSuccessStatusCode == false) return null;
                var list = kq.Result.Content.ReadAsAsync<List<LopMonHoc>>();
                list.Wait();
                return list.Result.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<DSSV_LopMonHoc> getDSSV(string malmh)
        {
            try
            {
                string url = @"https://localhost:44319/api/Lop_MonHoc/DSSV/" + malmh;
                var kq = hc.GetAsync(url);
                kq.Wait();
                if (kq.Result.IsSuccessStatusCode == false) return null;
                var list = kq.Result.Content.ReadAsAsync<List<DSSV_LopMonHoc>>();
                list.Wait();
                return list.Result.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static ThongBao PostSinhVienNew(DSSV_LopMonHoc a)
        {
            string url = @"https://localhost:44319/api/Lop_MonHoc/ThemSinhVien";

            var kq = hc.PostAsJsonAsync(url, a);
            kq.Wait();
            //var rrr=kq.Result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            ThongBao n = new ThongBao
            {
                kq = kq.Result.IsSuccessStatusCode,
                status = kq.Result.Content.ReadAsStringAsync().GetAwaiter().GetResult()
            };
            return n;
        }
        public static bool DeleteSinhVien(DSSV_LopMonHoc a)
        {
            string url = @"https://localhost:44319/api/Lop_MonHoc/XoaSinhVien/"+a.MaLmh+"/"+a.Masv;

            var kq = hc.DeleteAsync(url);
            kq.Wait();
            return kq.Result.IsSuccessStatusCode;
        }
      
    }
}
