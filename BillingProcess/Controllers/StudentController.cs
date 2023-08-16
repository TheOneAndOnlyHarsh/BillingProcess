using BillingProcess.DataAccess;
using BillingProcess.Models;
using BillingProcess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BillingProcess.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _student;
        private readonly ApplicationDbContext _db;

        public StudentController(IStudentRepository student, ApplicationDbContext db)
        {
            _student = student;
            _db = db;
        }

        [HttpGet]
       
        public async Task<IActionResult> GetAllAsync()
        {
            var studentsWithBillIds = await _db.Students
                .Select(s => new
                {
                    s.StudentId,
                    s.Name,
                    s.ContactDetails,
                    s.EnrollmentStatus,
                    BillIds = s.Bills.Select(b => b.BillID).ToList() 
                })
                .ToListAsync();

            return Ok(studentsWithBillIds);
        }


        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            var student = _db.Students.FirstOrDefault(s => s.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> PostStudent([FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Students.Add(student);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudent), new { id = student.StudentId }, student);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var student = _db.Students.FirstOrDefault(s => s.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            _db.Students.Remove(student);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] Student updatedStudent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updatedStudent.StudentId)
            {
                return BadRequest("Ids Not matched");
            }

            var existingStudent = await _db.Students.FindAsync(id);
            if (existingStudent == null)
            {
                return NotFound();
            }

            existingStudent.Name = updatedStudent.Name;
            existingStudent.ContactDetails = updatedStudent.ContactDetails;
            existingStudent.EnrollmentStatus = updatedStudent.EnrollmentStatus;

            _db.Students.Update(existingStudent);
            await _db.SaveChangesAsync();

            return Ok(existingStudent);
        }
    }
}
