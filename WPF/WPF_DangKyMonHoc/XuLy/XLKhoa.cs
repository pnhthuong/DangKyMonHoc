using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Wpf_DangKyMonHoc.Models;

namespace Wpf_DangKyMonHoc.XuLy
{
    class XLKhoa
    {
        private static HttpClient hc = new HttpClient();

        public static List<Khoa> getds()
        {
            try
            {
                string url = @"https://localhost:44319/api/Khoa";
                var kq = hc.GetAsync(url);
                kq.Wait();
                if (kq.Result.IsSuccessStatusCode == false) return null;
                var list = kq.Result.Content.ReadAsAsync<List<Khoa>>();
                list.Wait();
                return list.Result.ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public static bool post(Khoa a)
        {
            try
            {
                string url = @"https://localhost:44319/api/Khoa";
                var kq = hc.PostAsJsonAsync(url, a);
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
                string url = @"https://localhost:44319/api/Khoa/"+ id;
                var kq = hc.DeleteAsync(url);
                kq.Wait();
                return kq.Result.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool put(Khoa a)
        {
            try
            {
                string url = @"https://localhost:44319/api/Khoa/"+a.MaKhoa;
                var kq = hc.PutAsJsonAsync(url, a);
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
