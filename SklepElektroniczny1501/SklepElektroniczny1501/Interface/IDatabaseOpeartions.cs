using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepElektroniczny1501.Interface
{
    public interface IDatabaseOpeartions
    {
        Task<List<T>> GetAllAsync<T>() where T : class;
        Task<bool> AddAsync<T>(T item) where T : class;
        Task<bool> UpdateAsync<T>(T item) where T : class;
        Task<T> GetItemById<T>(Guid id) where T : class;
        void DeleteAsync<T>(int itemId) where T : class;
    }
}
