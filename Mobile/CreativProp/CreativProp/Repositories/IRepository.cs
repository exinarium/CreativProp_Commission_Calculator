using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CreativProp.Utilities.Enums;
using CreativProp.Utilities.SQLHelpers;

namespace CreativProp.Repositories
{
    public interface IRepository<T>
    {
        Task<bool> AddAsync(T item);
        Task<bool> UpdateAsync(T item);
        Task<bool> DeleteAsync(int id);
        Task<T> GetAsync(int id);
        Task<List<T>> ListAsync(Order? order = null, List<Search> searches = null, int pageNumber = 1, int itemsPerPage = 10,
            List<Filter<T>> filters = null, List<Join> joins = null);
    }
}

