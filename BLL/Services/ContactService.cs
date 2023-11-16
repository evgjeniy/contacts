using BLL.Contracts;
using DAL.Contracts;
using DAL.Entities;

namespace BLL.Services;

public class ContactService : IContactService
{
    private readonly IContactRepository _contactRepository;

    public ContactService(IContactRepository contactRepository) => _contactRepository = contactRepository;

    public async Task<List<Contact>> GetAllContactsAsync() => await _contactRepository.GetAllAsync();

    public async Task<Contact?> GetContactByIdAsync(uint id) => await _contactRepository.GetByIdAsync(id);

    public async Task AddContact(Contact contact) => await _contactRepository.AddAsync(contact);

    public async Task UpdateContact(Contact contact) => await _contactRepository.UpdateAsync(contact);

    public async Task DeleteContactById(uint id) => await _contactRepository.DeleteByIdAsync(id);
}