using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Wpf_DangKyMonHoc.Models;

namespace Wpf_DangKyMonHoc.XuLy
{
    class XLNganh
    {
        private static HttpClient hc = new HttpClient();

        public static List<Nganh> getds()
        {
            try
            {
                string url = @"https://localhost:44319/api/Nganh";
                var kq = hc.GetAsync(url);
                kq.Wait();
                if (kq.Result.IsSuccessStatusCode == false) return null;
                var list = kq.Result.Content.ReadAsAsync<List<Nganh>>();
                list.Wait();
                return list.Result.ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public static Nganh getct(string id)
        {
            try
            {
                string url = @"https://localhost:44319/api/Nganh/"+id;
                var kq = hc.GetAsync(url);
                kq.Wait();
                if (kq.Result.IsSuccessStatusCode == false) return null;
                var list = kq.Result.Content.ReadAsAsync<Nganh>();
                list.Wait();
                return list.Result;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public static bool post(Nganh a)
        {
            try
            {
                string url = @"https://localhost:44319/api/Nganh";
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
                string url = @"https://localhost:44319/api/Nganh/" + id;
                var kq = hc.DeleteAsync(url);
                kq.Wait();
                return kq.Result.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool put(Nganh a)
        {
            try
            {
                string url = @"https://localhost:44319/api/Nganh/" + a.MaNganh;
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
