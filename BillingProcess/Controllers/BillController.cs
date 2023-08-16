using BillingProcess.DataAccess;
using BillingProcess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BillingProcess.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public BillController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var bills = await _db.Bills.ToListAsync();
            return Ok(bills);
        }

        [HttpGet("{id}")]
        public IActionResult GetBill(int id)
        {
            var bill = _db.Bills.FirstOrDefault(b => b.BillID == id);
            if (bill == null)
            {
                return NotFound();
            }

            return Ok(bill);
        }

        [HttpPost]
        public async Task<IActionResult> PostBill([FromBody] Bill bill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var studentExists = await _db.Students.AnyAsync(s => s.StudentId == bill.StudentID);
            if (!studentExists)
            {
                return BadRequest("Student does not exist.");
            }

            _db.Bills.Add(bill);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBill), new { id = bill.BillID }, bill);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBill(int id)
        {
            var bill = _db.Bills.FirstOrDefault(b => b.BillID == id);
            if (bill == null)
            {
                return NotFound();
            }

            _db.Bills.Remove(bill);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBill(int id, [FromBody] Bill updatedBill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updatedBill.BillID)
            {
                return BadRequest("Ids Not matched");
            }

            var existingBill = await _db.Bills.FindAsync(id);
            if (existingBill == null)
            {
                return NotFound();
            }

            var studentExists = await _db.Students.AnyAsync(s => s.StudentId == updatedBill.StudentID);
            if (!studentExists)
            {
                return BadRequest("Student does not exist.");
            }

            existingBill.AmountDue = updatedBill.AmountDue;
            existingBill.DueDate = updatedBill.DueDate;
            existingBill.IsPaid = updatedBill.IsPaid;

            _db.Bills.Update(existingBill);
            await _db.SaveChangesAsync();

            return Ok(existingBill);
        }
    }
}
