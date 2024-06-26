using System.Collections.Generic;
using System.Threading.Tasks;
using Interview.Data.Models;
using Interview.Data.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Interview.Api.Controllers;

[ApiController]
[Route("api/customer")]
public class CustomerController : Controller
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpPost]
    public async Task<IActionResult> AddOrUpdateCustomer(Customer customer)
    {
        await _customerService.AddOrUpdateCustomerAsync(customer);

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomer(string email)
    {
        var customer = await _customerService.GetCustomerAsync(email);

        if (customer is null)
        {
            return NotFound();
        }

        return Ok(customer);
    }
}
