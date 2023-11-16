using DAL.Entities;

namespace DAL.Contracts;

public interface IContactRepository
{
    public Task<List<Contact>> GetAllAsync();
    public Task<Contact?> GetByIdAsync(uint id);
    public Task AddAsync(Contact contact);
    public Task UpdateAsync(Contact contact);
    public Task DeleteByIdAsync(uint id);
}