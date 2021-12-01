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
    public class NganhController : ControllerBase
    {
        private Models.DangKyMonHocContext dc = new Models.DangKyMonHocContext();
        [HttpGet]
        public IActionResult getDSMganh()
        {

            var list = dc.Nganhs.ToList();

            return Ok(list);
        }
        [HttpGet("{id}")]
        public IActionResult getctnganh(string id)
        {
            var a = dc.Nganhs.Find(id);
            if (a == null) return NotFound();
            return Ok(a);
        }
        [HttpPost]
        public async Task<IActionResult> postDSNganh(Models.Nganh n)
        {
            if (ModelState.IsValid == false) return BadRequest();
            Models.Nganh temp = dc.Nganhs.Find(n.MaNganh);
            if (temp != null) return BadRequest();
            Models.Nganh a = new Models.Nganh
            {
                MaNganh = n.MaNganh,
                TenNganh = n.TenNganh,
                MaKhoa = n.MaKhoa


            };
            dc.Nganhs.Add(a);
            await dc.SaveChangesAsync();
            return Ok();

        }
        [HttpDelete("{id}")]
        public IActionResult deleteLop(string id)
        {
            Models.Nganh n = dc.Nganhs.Find(id);
            if (n == null) return NotFound();
            foreach (var t in dc.Khoas.Where(x => x.MaKhoa == id))
            {
                return BadRequest();
            }
            dc.Nganhs.Remove(n);
            dc.SaveChanges();
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult putNganh(Models.Nganh n)
        {
            if (ModelState.IsValid == false) return BadRequest();
            Models.Nganh temp = dc.Nganhs.Find(n.MaNganh);
            if (temp == null) return NotFound();
          
            temp.TenNganh = n.TenNganh;
            temp.MaKhoa = n.MaKhoa;


            dc.SaveChanges();
            return Ok();
        }

    }
}

