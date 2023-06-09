﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySaleApp.Application.Services.Contract;
using MySaleApp.Domain.DTOs;
using MySaleApp.Infrastructure.Common;
using MySaleApp.Infrastructure.Utility;

namespace MySaleApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var response = new Response<List<RoleDTO>>();
            try
            {
                response.value = await _roleService.GetList();
                response.status = Constants.Status.True;
            }
            catch (Exception ex)
            {
                response.status = Constants.Status.False;
                response.message = ex.Message;
            }
            return Ok(response);
        }

    }
}
