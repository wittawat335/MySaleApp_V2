using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MySaleApp.Application.Repositories;
using MySaleApp.Application.Services.Contract;
using MySaleApp.Domain.DTOs;
using MySaleApp.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MySaleApp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _repository;
        private readonly IPasswordHasService _service;
        private readonly IMapper _mapper;

        public UserService(IGenericRepository<User> repository, IPasswordHasService service, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _service = service;
        }

        public async Task<List<UserDTO>> GetList()
        {
            try
            {
                var query = await _repository.GetList();
                var list = query.Include(x => x.Role).ToList();

                return _mapper.Map<List<UserDTO>>(list);
            }
            catch
            {
                throw;
            }
        }

        public async Task<SessionDTO> Login(string email, string password, string key)
        {
            try
            {
                var user = await _repository.Get(x => x.Email == email);
                var verifyPassword = _service.Verify(user.PasswordHash!, password);

                if (user == null || !verifyPassword)
                    throw new TaskCanceledException("Username or password is incorrect");

                var query = await _repository.GetList(x => x.Email == email);
                if (query.FirstOrDefault() == null)
                    throw new TaskCanceledException("");

                User checkUser = query.Include(x => x.Role).First();
                if (checkUser.IsActive == true)
                    checkUser.Token = CreateToken(checkUser, key);
                else
                    throw new TaskCanceledException("");

                return _mapper.Map<SessionDTO>(checkUser);
            }
            catch
            {
                throw;
            }
        }

        public string CreateToken(User user, string key)
        {
            try
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim("UserId", user.UserId.ToString()),
                    new Claim("FullName", user.FullName!),
                    new Claim("Email", user.Email!),
                };

                var symmetrickey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var cred = new SigningCredentials(symmetrickey, SecurityAlgorithms.HmacSha512Signature);
                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: cred
                    );
                var jwt = new JwtSecurityTokenHandler().WriteToken(token);

                return jwt;
            }
            catch
            {
                throw;
            }
        }

        public async Task<UserDTO> Create(UserDTO model)
        {
            try
            {
                var checkDuplicate = await _repository.GetList(x => x.Email == model.Email);
                if (checkDuplicate.FirstOrDefault() != null)
                    throw new TaskCanceledException("Email '" + model.Email + "' is already taken");

                model.PasswordHash = _service.Hash(model.PasswordHash!);
                var createUser = await _repository.Create(_mapper.Map<User>(model));
                if (createUser.UserId == null)
                    throw new TaskCanceledException("");

                var query = await _repository.GetList(x => x.UserId == createUser.UserId);
                createUser = query.Include(x => x.Role).First();

                return _mapper.Map<UserDTO>(createUser);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Update(UserDTO model)
        {
            try
            {
                var userMap = _mapper.Map<User>(model);
                var UpdateUser = await _repository.Get(x => x.UserId == userMap.UserId);
                if (UpdateUser == null)
                    throw new TaskCanceledException("");

                UpdateUser.FullName = userMap.FullName;
                UpdateUser.Email = userMap.Email;
                UpdateUser.RoleId = userMap.RoleId;
                UpdateUser.PasswordHash = _service.Hash(userMap.PasswordHash!);
                UpdateUser.IsActive = userMap.IsActive;

                bool updated = await _repository.Update(UpdateUser);
                if (!updated)
                    throw new TaskCanceledException("");

                return updated;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var query = await _repository.Get(x => x.UserId == id);
                if (query == null)
                    throw new TaskCanceledException("");

                bool deleted = await _repository.Delete(query);
                if (deleted == false)
                    throw new TaskCanceledException("");

                return deleted;
            }
            catch
            {
                throw;
            }
        }
    }
}
