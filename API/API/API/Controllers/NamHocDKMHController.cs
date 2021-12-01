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
    public class NamHocDKMHController : ControllerBase
    {

        private readonly DangKyMonHocContext _db;

        public NamHocDKMHController(DangKyMonHocContext dangKyMonHocContext)
        {
            _db = dangKyMonHocContext;
        }

        [HttpGet]
        public IEnumerable<NamHocDkmh> getlistNamHocDkmh()
        {
            return _db.NamHocDkmhs.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NamHocDkmh>> getDetailNamHocDkmh(string id)
        {
            var detail = await _db.NamHocDkmhs.FindAsync(id.Trim());
            if (detail == null) return NotFound();
            return Ok(detail);
        }

        [HttpPost]
        public async Task<ActionResult<NamHocDkmh>> postNamHocDkmh(NamHocDkmh cv)
        {
            var checkCV = await _db.NamHocDkmhs.FindAsync(cv.MaNh.Trim());
            if (checkCV != null) return NotFound();
            _db.NamHocDkmhs.Add(cv);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<NamHocDkmh>> deleteNamHocDkmh(string id)
        {
            var checkCV = await _db.NamHocDkmhs.FindAsync(id.Trim());
            if (checkCV == null) return NotFound();
            var checkRelationshipCDK = _db.CongDangKies.Where(n => n.MaNh==checkCV.MaNh).Count();
            
            if (checkRelationshipCDK > 0) return NotFound();
            _db.NamHocDkmhs.Remove(checkCV);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<NamHocDkmh>> putNamHocDkmh(NamHocDkmh cv)
        {
            var checkCV = await _db.NamHocDkmhs.FindAsync(cv.MaNh.Trim());
            if (checkCV == null) return NotFound();

            checkCV.TenNh = cv.TenNh;
            _db.NamHocDkmhs.Update(checkCV);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
