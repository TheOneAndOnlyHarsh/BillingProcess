using BillingProcess.DataAccess;
using BillingProcess.Models.DTO;
using BillingProcess.Models;
using BillingProcess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BillingProcess.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ApplicationDbContext _db;


        public UserController(IUserRepository userRepository, ApplicationDbContext db)
        {
            _userRepository = userRepository;
            _db = db;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegistrationRequestDTO registrationRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_userRepository.IsUniqueUser(registrationRequest.Username))
            {
                return BadRequest("Username is already taken.");
            }

            var registeredUser = await _userRepository.Register(registrationRequest);
            return Ok(registeredUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequestDTO loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var loginResponse = await _userRepository.Login(loginRequest);

            if (loginResponse == null)
            {
                return Unauthorized("Invalid credentials");
            }

            return Ok(loginResponse);
        }

        [HttpGet]



        public async Task<IEnumerable<LocalUser>> GetAllAsync()
        {
            return await _db.LocalUsers.ToListAsync();
        }


        [HttpGet("{id}")]


        public IActionResult GetUser(int id)
        {
            var users = _db.LocalUsers.FirstOrDefault(s => s.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }


        [HttpDelete("{id}")]

        public IActionResult DeleteUser(int id)
        {
            var user = _db.LocalUsers.FirstOrDefault(s => s.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            _db.LocalUsers.Remove(user);
            _db.SaveChanges();

            return NoContent();
        }
    }
}