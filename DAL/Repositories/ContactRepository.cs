using DAL.Context;
using DAL.Contracts;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly ContactsDbContext _context;

    public ContactRepository(ContactsDbContext context) => _context = context;

    public async Task<List<Contact>> GetAllAsync() => await _context.Contacts.ToListAsync();
    
    public async Task<Contact?> GetByIdAsync(uint id) => await _context.Contacts.FindAsync(id);

    public async Task AddAsync(Contact contact)
    {
        _context.Add(contact);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Contact contact)
    {
        _context.Update(contact);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(uint id)
    {
        var contact = await _context.Contacts.FindAsync(id);
        if (contact != null)
        {
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
        }
    }
}