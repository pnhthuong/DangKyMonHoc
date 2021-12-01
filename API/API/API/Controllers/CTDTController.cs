using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CTDTController : ControllerBase
    {
        private DangKyMonHocContext _db = new DangKyMonHocContext();
        [HttpGet]
        public IEnumerable<ChuongTrinhDaoTao> getDS()
        {
            return _db.ChuongTrinhDaoTaos.ToList();
        }
        [HttpPost]
        public IActionResult postctdt(ChuongTrinhDaoTao ct)
        {
            var check = _db.ChuongTrinhDaoTaos.Find(ct.MaCtdt.Trim());
            if (check != null) return NotFound();
            _db.ChuongTrinhDaoTaos.Add(ct);
            _db.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deletectdt(string id)
        {
            var check = _db.ChuongTrinhDaoTaos.Find(id);
            if (check == null) return NotFound();
            //var checkrelatioship = _db.Lops.Where(x => x.MaCtdt == id);
            //if (checkrelatioship.Count() > 0) return NotFound();

            List<ChiTietCtdt> lsct = _db.ChiTietCtdts.Where(x => x.MaCtdt==id).ToList();
            foreach(var a in lsct)
            {
                _db.ChiTietCtdts.Remove(a);
            }
            _db.Remove(check);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult putctdt(ChuongTrinhDaoTao ct)
        {
            var check = _db.ChuongTrinhDaoTaos.Find(ct.MaCtdt);
            if (check == null) return NotFound();
            check.TenCtdt = ct.TenCtdt;
            check.TongSoTinChi = ct.TongSoTinChi;
            _db.SaveChanges();
            return Ok();
        }

        
    }
}
