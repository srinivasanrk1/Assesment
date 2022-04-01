using AssesmentAPI.Core.Interface;
using AssesmentAPI.Core.Modal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssesmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;
        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        [Route("ProductCountByStatus")]
        public IActionResult GetProductCountByStatus()
        {
            var result = _inventoryService.ProductCountByStatus();
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(Guid id, StatusEnum statusEnum)
        {
            var result = await _inventoryService.UpdateProductStatus(id, statusEnum);
            return Ok(result);
        }

        [HttpPut]
        [Route("SellProduct")]
        public async Task<IActionResult> SellProduct(Guid id)
        {
            var result = await _inventoryService.SellProduct(id);
            return Ok(result);
        }
    }
}
