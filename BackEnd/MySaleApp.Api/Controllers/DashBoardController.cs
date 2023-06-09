﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySaleApp.Application.Services.Contract;
using MySaleApp.Domain.DTOs;
using MySaleApp.Infrastructure.Common;
using MySaleApp.Infrastructure.Utility;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MySaleApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly IDashBoardService _service;

        public DashBoardController(IDashBoardService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        [Route("Summary")]
        public async Task<IActionResult> Summary()
        {
            var response = new Response<DashBoardDTO>();
            try
            {
                response.value = await _service.Summary();
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
