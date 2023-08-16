using BillingProcess.Models.DTO;
using BillingProcess.Models;

namespace BillingProcess.Repository.IRepository
{
   
        public interface IUserRepository
        {
            bool IsUniqueUser(string username);
            Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
            Task<LocalUser> Register(RegistrationRequestDTO registrationRequestDTO);

            Task<IEnumerable<LocalUser>> GetAllUsers();
            Task<LocalUser> GetUserById(int id);

            void DeleteUser(int id);
        }
    
}
