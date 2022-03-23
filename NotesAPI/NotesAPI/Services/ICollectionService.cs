using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesAPI.Services
{
    public interface ICollectionService<T>
    {
        Task<List<T>> GetAllAsync();

        Task<T> GetAsync(Guid id);

        Task<bool> CreateAsync(T model);

        Task< bool> UpdateAsync(Guid id, T model);

        Task<bool> DeleteAsync(Guid id);

    }
}
