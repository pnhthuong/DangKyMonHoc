using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Wpf_DangKyMonHoc.Models;

namespace Wpf_DangKyMonHoc.XuLy
{
    class XLChucVu
    {
        private static HttpClient hc = new HttpClient();

        public static List<ChucVu> dschucvu()
        {
            try
            {
                string url = @"https://localhost:44319/api/chucvu";
                var kq = hc.GetAsync(url);
                kq.Wait();
                if (kq.Result.IsSuccessStatusCode == false) return null;
                var list = kq.Result.Content.ReadAsAsync<List<ChucVu>>();
                list.Wait();
                return list.Result.ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }

        



        public static bool postChucVu(ChucVu cv)
        {
            try
            {
                string url = @"https://localhost:44319/api/chucvu";
                var kq = hc.PostAsJsonAsync(url, cv);
                kq.Wait();
                return kq.Result.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool deleteChucVu(string id)
        {
            try
            {
                string url = @"https://localhost:44319/api/chucvu/" + id;
                var kq = hc.DeleteAsync(url);
                kq.Wait();
                return kq.Result.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool putChucVu(ChucVu cv)
        {
            try
            {
                string url = @"https://localhost:44319/api/chucvu/"+cv.MaChucVu;
                var kq = hc.PutAsJsonAsync(url, cv);
                kq.Wait();
                return kq.Result.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
