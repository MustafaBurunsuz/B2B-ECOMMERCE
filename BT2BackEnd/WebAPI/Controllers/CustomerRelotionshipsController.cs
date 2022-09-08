using Business.Repositories.CustomerRelotionshipRepository;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerRelotionshipsController : ControllerBase
    {
        private readonly ICustomerRelotionshipService _customerRelotionshipService;

        public CustomerRelotionshipsController(ICustomerRelotionshipService customerRelotionshipService)
        {
            _customerRelotionshipService = customerRelotionshipService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Add(CustomerRelationships customerRelotionship)
        {
            var result = await _customerRelotionshipService.Add(customerRelotionship);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Update(CustomerRelationships customerRelotionship)
        {
            var result = await _customerRelotionshipService.Update(customerRelotionship);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete(CustomerRelationships customerRelotionship)
        {
            var result = await _customerRelotionshipService.Delete(customerRelotionship);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList()
        {
            var result = await _customerRelotionshipService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _customerRelotionshipService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

    }
}
