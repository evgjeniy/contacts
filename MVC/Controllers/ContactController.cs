using System.Diagnostics;
using BLL.Contracts;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers;

public class ContactController : Controller
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService) => _contactService = contactService;

    [HttpGet("{id}")]
    public async Task<IActionResult> Find(uint id)
    {
        var contact = await _contactService.GetContactByIdAsync(id);
        if (contact == null) return NotFound($"There's no contact with ID: {id}");
        
        return Json(contact);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] Contact contact)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        await _contactService.AddContact(contact);
        return Json(contact);
    }
    
    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] Contact contact)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        await _contactService.UpdateContact(contact);
        return Json(contact);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(uint id)
    {
        await _contactService.DeleteContactById(id);
        return Json(id);
    }

    public async Task<IActionResult> Index() => View(await _contactService.GetAllContactsAsync());

    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}