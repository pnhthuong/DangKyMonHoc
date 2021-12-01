using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Wpf_DangKyMonHoc.Models;

namespace Wpf_DangKyMonHoc.XuLy
{
    class XL_CTDT
    {
        private static HttpClient hc = new HttpClient();

        public static List<ChuongTrinhDaoTao> getdsCTDT()
        {
            try
            {
                string url = @"https://localhost:44319/api/ctdt";
                var kq = hc.GetAsync(url);
                kq.Wait();
                if (kq.Result.IsSuccessStatusCode == false) return null;
                var list = kq.Result.Content.ReadAsAsync<List<ChuongTrinhDaoTao>>();
                list.Wait();
                return list.Result.ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public static bool post(ChuongTrinhDaoTao ct)
        {
            try
            {
                string url = @"https://localhost:44319/api/ctdt";
                var kq = hc.PostAsJsonAsync(url, ct);
                kq.Wait();
                return kq.Result.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool delete(string id)
        {
            try
            {
                string url = @"https://localhost:44319/api/ctdt/" + id;
                var kq = hc.DeleteAsync(url);
                kq.Wait();
                return kq.Result.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool put(ChuongTrinhDaoTao cv)
        {
            try
            {
                string url = @"https://localhost:44319/api/ctdt/" + cv.MaCtdt;
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
