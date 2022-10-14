using IoMI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IoMI.Application.Repositories;

public interface IWriteRepository<T> where T : BaseEntity
{
    DbSet<T> Table { get; }

    Task<bool> AddAsync(T entity);
    bool Update(T entity);
    bool Delete(T entity);
    Task<bool> DeleteAsync(Guid id);
    Task SaveAsync();
}