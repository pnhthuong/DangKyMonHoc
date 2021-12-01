using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Wpf_DangKyMonHoc.Models;

namespace Wpf_DangKyMonHoc.XuLy
{
    class XL_Login
    {
        private static HttpClient hc = new HttpClient();
        public static NhanVien postloginnv(NhanVien nv)
        {
            try
            {
                string url = @"https://localhost:44319/api/login/nhanvien";
                var kq = hc.PostAsJsonAsync(url, nv);
                kq.Wait();
                if (kq.Result.IsSuccessStatusCode == false)
                    return null;
                var result = kq.Result.Content.ReadAsAsync<NhanVien>();
                result.Wait();
                return result.Result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static NhanVien GetNhanVien(string id)
        {
            try
            {
                string url = @"https://localhost:44319/api/login/nhanvien/"+id;
                var kq = hc.GetAsync(url);
                kq.Wait();
                if (kq.Result.IsSuccessStatusCode == false) return null;
                var list = kq.Result.Content.ReadAsAsync<NhanVien>();
                list.Wait();
                return list.Result;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public static bool putloginnv(NhanVien nv)
        {
            try
            {
                string url = @"https://localhost:44319/api/login/nhanvien";
                var kq = hc.PutAsJsonAsync(url, nv);
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
