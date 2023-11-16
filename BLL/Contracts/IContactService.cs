using DAL.Entities;

namespace BLL.Contracts;

public interface IContactService
{
    public Task<List<Contact>> GetAllContactsAsync();
    public Task<Contact?> GetContactByIdAsync(uint id);
    public Task AddContact(Contact contact);
    public Task UpdateContact(Contact contact);
    public Task DeleteContactById(uint id);
}