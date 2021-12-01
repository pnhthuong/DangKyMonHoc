using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC_DangKyMonHoc.Helper;
using MVC_DangKyMonHoc.Models;
using Newtonsoft.Json;

namespace MVC_DangKyMonHoc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        APIHelper api = new APIHelper();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var a = SessionHelper.getObject<SinhVien>(HttpContext.Session, "login");
            
            ViewBag.sv = a.TenSv;
            return View();
        }

        public async Task<IActionResult> CTCTDTSV()
        {
            SinhVien sv = SessionHelper.getObject<SinhVien>(HttpContext.Session, "login");
            List<CT_CTDT_SV> list = null;
            HttpClient client = api.Intial();
            HttpResponseMessage res = await client.GetAsync("api/MonHoc/CTDT/DanhSach/ChiTiet/"+sv.MaSv);
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<CT_CTDT_SV>>(result);
                return View(list);
            }
            ViewBag.error = "Không tìm thấy";
            return View();
        }
        public IActionResult Login()
		{
            return View();
		}
        
        public async Task<IActionResult> PostLogin(string username,string password)
		{
            SinhVien a = new SinhVien();
            if(ModelState.IsValid)
            {
                if (username == null || password == null)
                {
                    ViewBag.error = "Thông tin đăng nhập không chính xác";
                    return RedirectToAction("Login");
                }
                else
                {
                    HttpClient client = api.Intial();
                    a.MaSv = username.Trim();
                    a.Matkhau = password.Trim();
                    HttpContent httpcontent = new StringContent(JsonConvert.SerializeObject(a), Encoding.UTF8, "application/json");
                    HttpResponseMessage res = await client.PostAsync("api/login/SignIn/SinhVien", httpcontent);
                    if (!res.IsSuccessStatusCode)
                    {
                        return StatusCode(500, "Bạn Kiểm tra lại mật khẩu hoặc do tài khoản của bạn bị khóa !");
                    }
                    else
                    {
                        var result = res.Content.ReadAsStringAsync().Result;
                        SinhVien sv = JsonConvert.DeserializeObject<SinhVien>(result);
                        SessionHelper.setObject(HttpContext.Session,"login", sv);
                        return RedirectToAction("Index");
                    }
                }
            }

            return RedirectToAction("Login");
        }
           
            
		
      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
