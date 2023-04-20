using MySaleApp.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySaleApp.Application.Services.Contract
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetList();
        Task<UserDTO> Create(UserDTO model);
        Task<bool> Update(UserDTO model);
        Task<bool> Delete(Guid id);
        Task<SessionDTO> Login(string email, string password, string key);
    }
}
