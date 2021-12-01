using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NienkhoaController : ControllerBase
    {
        private Models.DangKyMonHocContext dc = new Models.DangKyMonHocContext();
        [HttpGet]
        public IActionResult getDSNienkhoa()
        {

            var list = dc.NienKhoas.ToList();

            return Ok(list);
        }

        [HttpGet("NKCDK/{id}")]
        public IEnumerable<NienKhoa> getdsnk(string id)
        {
            List<NienKhoa> getlist = new List<NienKhoa>();
            var listCDKNK = dc.NienKhoaCdks.Where(x => x.MaCdk == id).ToList();
            foreach (var a in listCDKNK)
            {
                NienKhoa b = dc.NienKhoas.Find(a.MaNk);
                getlist.Add(b);
            }
            return getlist;
        }
        [HttpPost]
        public IActionResult postDSNienkhoa(Models.NienKhoa n)
        {
            if (ModelState.IsValid == false) return BadRequest();
            Models.NienKhoa temp = dc.NienKhoas.Find(n.MaNk);
            if (temp != null) return BadRequest();
            Models.NienKhoa a = new Models.NienKhoa
            {
                MaNk = n.MaNk,
                TenNk = n.TenNk,
                MaCtdt=n.MaCtdt
            };
            dc.NienKhoas.Add(a);
            dc.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult deleteNienkhoa(string id)
        {
            Models.NienKhoa n = dc.NienKhoas.Find(id);
            if (n == null) return NotFound();
            foreach (var t in dc.NienKhoaCdks.Where(x => x.MaCdk == id))
            {
                return BadRequest();
            }
            var a = dc.Lops.Where(x => x.MaNk.Trim() == n.MaNk).Count();
            if (a>0) return BadRequest();
            dc.NienKhoas.Remove(n);
            dc.SaveChanges();
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult putNienkhoa(Models.NienKhoa n)
        {
            if (ModelState.IsValid == false) return BadRequest();
            Models.NienKhoa temp = dc.NienKhoas.Find(n.MaNk);
            if (temp == null) return NotFound();
          
            temp.TenNk = n.TenNk;
            
            dc.SaveChanges();
            return Ok();
        }
    }
}
