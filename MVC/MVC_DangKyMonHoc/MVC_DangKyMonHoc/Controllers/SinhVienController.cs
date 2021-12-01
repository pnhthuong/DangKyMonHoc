using Microsoft.AspNetCore.Mvc;
using MVC_DangKyMonHoc.Helper;
using MVC_DangKyMonHoc.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MVC_DangKyMonHoc.Controllers
{
	public class SinhVienController : Controller
	{
		APIHelper api = new APIHelper();
		public IActionResult IndexThongTinSinhVien()
		{
			SinhVien sv = SessionHelper.getObject<SinhVien>(HttpContext.Session, "login");
			if (sv == null)
			{
				return View("../Home/Login");
			}
			sv.Sdt=sv.Sdt.Trim();
			sv.Email=sv.Email.Trim();
			return View(sv);
		}
		[HttpPost] 
		public async Task<IActionResult> EditTTCNSinhVien(SinhVien a)
        {
            if (a.Email == null || a.Sdt == null)
            {
				ModelState.AddModelError("Sdt", "Bạn Nhập Đầy Đủ Thông Tin Cá Nhân");
				return View("IndexThongTinSinhVien");
			}
            
			SinhVien sv = SessionHelper.getObject<SinhVien>(HttpContext.Session, "login");
			sv.Email = a.Email;
			sv.Sdt = a.Sdt;

			HttpClient client = api.Intial();
			HttpContent httpcontent = new StringContent(JsonConvert.SerializeObject(sv), Encoding.UTF8, "application/json");
			HttpResponseMessage res = await client.PutAsync("/api/SinhVien/" + sv.MaSv, httpcontent);
            if (res.IsSuccessStatusCode)
            {
				ViewBag.note = "Chỉnh sửa thành công";
				SessionHelper.setObject(HttpContext.Session, "login", sv);
				return View("../Home/Index");
			}
			return RedirectToAction("IndexThongTinSinhVien");
		}

        [HttpPost]
        public async Task<IActionResult> editpasswordSinhVien(EditPasswword e)
        {
            if (e.matkhaucu == null || e.matkhaumoi == null)
            {
				ViewBag.note = "Kiểm tra lại Mật Khẩu";
				return View("SuaMatKhau");
            }
			SinhVien sinhVien = SessionHelper.getObject<SinhVien>(HttpContext.Session, "login");
			e.ma = sinhVien.MaSv;

			HttpClient client = api.Intial();
			HttpContent content = new StringContent(JsonConvert.SerializeObject(e), Encoding.UTF8, "application/json");
			HttpResponseMessage res = await client.PostAsync("/api/login/SinhVien/MatKhau/Resset", content);
            if (res.IsSuccessStatusCode)
            {
				ViewBag.note = "Chỉnh sửa mật khẩu thành công";
				return View("../Home/Index");
            }
			ViewBag.note="Mật Khẩu hiện tại Sai";
			return View("SuaMatKhau");
		}
        public bool checkpassold(string ma)
        {
			string hash= BCrypt.Net.BCrypt.HashPassword(ma);
			SinhVien sv = SessionHelper.getObject<SinhVien>(HttpContext.Session, "login");
            if (hash == sv.Matkhau.Trim()) { return true; }
			return false;
		}
		public IActionResult SuaMatKhau()
		{
			SinhVien sv = SessionHelper.getObject<SinhVien>(HttpContext.Session, "login");
			if (sv == null)
			{
				return View("../Home/Login");
			}
			return View();
		}
		public IActionResult dangxuat()
        {
			SessionHelper.setObject(HttpContext.Session, "login", null);
			return View("../Home/Login");
        }
	}
}
