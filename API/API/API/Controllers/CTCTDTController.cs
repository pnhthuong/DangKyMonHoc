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
    public class CTCTDTController : ControllerBase
    {
        private readonly DangKyMonHocContext _db = new DangKyMonHocContext();
        [HttpGet("{id}")]
        public IEnumerable<ChiTietCtdt> getctctdt(string id)
        {
            return _db.ChiTietCtdts.Where(x => x.MaCtdt == id).ToList();
        }
        [HttpPost]
        public async Task<IActionResult> postlistchititetctdt([FromBody] List<ChiTietCtdt> ct)
        {
            foreach (var a in ct)
            {
                _db.ChiTietCtdts.Add(a);
            }
            _db.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> putlistchititetctdt([FromBody] List<ChiTietCtdt> ct)
        {
            List<ChiTietCtdt> listtam = _db.ChiTietCtdts.Where(x => x.MaCtdt == ct[0].MaCtdt).ToList();
            foreach(var a in ct)
            {
                ChiTietCtdt cttk = _db.ChiTietCtdts.Where(x => x.MaCtdt == a.MaCtdt && x.MaMh == a.MaMh).FirstOrDefault();
                if (cttk != null)
                {
                    cttk.MaHk = a.MaHk;
                    listtam.Remove(cttk);
                    continue;
                }
                _db.ChiTietCtdts.Add(a);
            }

            if(listtam.Count>0)
            {
                foreach(var a in listtam)
                {
                    _db.ChiTietCtdts.Remove(a);
                }
            }
            _db.SaveChanges();
            return Ok();
        }
    }
}
