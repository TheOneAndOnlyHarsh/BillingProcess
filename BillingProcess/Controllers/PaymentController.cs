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
    public class PaymentController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public PaymentController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var payments = await _db.Payments.ToListAsync();
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public IActionResult GetPayment(int id)
        {
            var payment = _db.Payments.FirstOrDefault(p => p.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return Ok(payment);
        }

        [HttpPost]
        public async Task<IActionResult> PostPayment([FromBody] Payment payment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var billExists = await _db.Bills.AnyAsync(b => b.BillID == payment.BillId);
            if (!billExists)
            {
                return BadRequest("Bill does not exist.");
            }

            _db.Payments.Add(payment);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPayment), new { id = payment.PaymentId }, payment);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePayment(int id)
        {
            var payment = _db.Payments.FirstOrDefault(p => p.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            _db.Payments.Remove(payment);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, [FromBody] Payment updatedPayment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updatedPayment.PaymentId)
            {
                return BadRequest("Ids Not matched");
            }

            var existingPayment = await _db.Payments.FindAsync(id);
            if (existingPayment == null)
            {
                return NotFound();
            }

            var billExists = await _db.Bills.AnyAsync(b => b.BillID == updatedPayment.BillId);
            if (!billExists)
            {
                return BadRequest("Bill does not exist.");
            }

            existingPayment.PaymentDate = updatedPayment.PaymentDate;
            existingPayment.PaymentAmount = updatedPayment.PaymentAmount;
            existingPayment.PaymentType = updatedPayment.PaymentType;

            _db.Payments.Update(existingPayment);
            await _db.SaveChangesAsync();

            return Ok(existingPayment);
        }
    }
}
