using Customers.Api.Attributes;
using Customers.Api.Contracts.Requests;
using Customers.Api.Mapping;
using Customers.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Customers.Api.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CustomerRequest request)
        {
            var customer = request.ToCustomer();
            await _customerService.CreateAsync(customer);
            var customerResponse = customer.ToCustomerResponse();
            return CreatedAtAction("Get", new { id = customerResponse.Id }, customerResponse);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            if (customer is null)
            {
                return NotFound();
            }
            var customerResponse = customer.ToCustomerResponse();
            return Ok(customerResponse);
        }
        [HttpGet()]
        public async Task<IActionResult> GetAllAsync()
        {
            var customers = await _customerService.GetAllAsync();
            var customerResponses = customers.Select(c => c.ToCustomerResponse());
            return Ok(customerResponses);
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync([FromMultiSource] UpdateCustomerRequest request)
        {
            var existingCustomer = await _customerService.GetByIdAsync(request.Id);
            if (existingCustomer is null)
            {
                return NotFound();
            }
            var customer = request.ToCustomer();
            await _customerService.UpdateAsync(customer);
            var customerResponse = customer.ToCustomerResponse();
            return Ok(customerResponse);
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleted = await _customerService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}