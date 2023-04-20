using Microsoft.AspNetCore.Mvc;
using MySaleApp.Infrastructure.Common;
using MySaleApp.Infrastructure.Utility;
using MySaleApp.Application.Services.Contract;
using MySaleApp.Domain.DTOs;

namespace MySaleApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var response = new Response<List<CategoryDTO>>();
            try
            {
                response.value = await _categoryService.GetList();
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
