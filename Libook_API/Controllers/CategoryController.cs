using Libook_API.Data;
using Libook_API.Models.DTO;
using Libook_API.Models.Response;
using Libook_API.Repositories.CategoryRepo;
using Libook_API.Service.AuthorService;
using Libook_API.Service.CategoryService;
using Microsoft.AspNetCore.Mvc;

namespace Libook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {

        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categoryResponses = await categoryService.GetAllCategoryAsync();

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get all category successfully!",
                data = categoryResponses
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var categoryResponse = await categoryService.GetCategoryByIdAsync(id);

            if (categoryResponse == null)
            {
                return NotFound();
            }

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get category by id successfully!",
                data = categoryResponse
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryDTO categoryDTO)
        {
            var categoryResponse = await categoryService.AddCategoryAsync(categoryDTO);

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Create category successfully!",
                data = categoryResponse
            };
            return Ok(response);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CategoryDTO categoryDTO)
        {
            var categoryResponse = await categoryService.UpdateCategoryAsync(id, categoryDTO);
            if (categoryResponse == null)
            {
                return NotFound();
            }
            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Update category successfully!",
                data = categoryResponse
            };
            return Ok(response);
        }

    }
}
