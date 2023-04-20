using AutoMapper;
using MySaleApp.Application.Repositories;
using MySaleApp.Application.Services.Contract;
using MySaleApp.Domain.DTOs;
using MySaleApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySaleApp.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IGenericRepository<Role> _repository;
        private readonly IMapper _mapper;

        public RoleService(IGenericRepository<Role> roleRepository, IMapper mapper)
        {
            _repository = roleRepository;
            _mapper = mapper;
        }

        public async Task<List<RoleDTO>> GetList()
        {
            try
            {
                var list = await _repository.GetList();
                return _mapper.Map<List<RoleDTO>>(list.ToList());
            }
            catch
            {
                throw;
            }
        }
    }
}
