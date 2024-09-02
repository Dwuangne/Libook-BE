using Libook_API.Models.DTO;
using Libook_API.Models.Response;
using Libook_API.Service.CategoryService;
using Libook_API.Service.SupplierService;
using Microsoft.AspNetCore.Mvc;

namespace Libook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : Controller
    {
        private readonly ISupplierService supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            this.supplierService = supplierService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var supplierResponses = await supplierService.GetAllSupplierAsync();

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get all supplier successfully!",
                data = supplierResponses
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var supplierResponse = await supplierService.GetSupplierByIdAsync(id);

            if (supplierResponse == null)
            {
                return NotFound();
            }

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Get supplier by id successfully!",
                data = supplierResponse
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SupplierDTO supplierDTO)
        {
            var supplierResponse = await supplierService.AddSupplierAsync(supplierDTO);

            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Create supplier successfully!",
                data = supplierResponse
            };
            return Ok(response);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] SupplierDTO supplierDTO)
        {
            var supplierResponse = await supplierService.UpdateSupplierAsync(id, supplierDTO);
            if (supplierResponse == null)
            {
                return NotFound();
            }
            var response = new ResponseObject
            {
                status = System.Net.HttpStatusCode.OK,
                message = "Update supplier successfully!",
                data = supplierResponse
            };
            return Ok(response);
        }
    }
}
