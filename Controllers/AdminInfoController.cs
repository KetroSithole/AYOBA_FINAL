using AYOBA_FINAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AYOBA_FINAL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminInfoController : ControllerBase
    {
        private readonly StockContext _context;

        public AdminInfoController(StockContext context)
        {
            _context = context;
        }

        // GET: api/AdminInfo
        [HttpGet]
        public async Task<IEnumerable<AdminInfo>> Get()
        {
            return await _context.AdminInfos.ToListAsync();
        }

        // GET: api/AdminInfo/id?id=1
        [HttpGet("id")]
        [ProducesResponseType(typeof(AdminInfo), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var adminInfo = await _context.AdminInfos.FindAsync(id);
            return adminInfo == null ? NotFound() : Ok(adminInfo);
        }

        // POST: api/AdminInfo
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(AdminInfo adminInfo)
        {
            await _context.AdminInfos.AddAsync(adminInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = adminInfo.AdminId }, adminInfo);
        }

        // PUT: api/AdminInfo/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AdminInfo adminInfo)
        {
            if (id != adminInfo.AdminId) return BadRequest();
            _context.Entry(adminInfo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/AdminInfo/1
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var adminInfoToDelete = await _context.AdminInfos.FindAsync(id);
            if (adminInfoToDelete == null) return NotFound();

            _context.AdminInfos.Remove(adminInfoToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
