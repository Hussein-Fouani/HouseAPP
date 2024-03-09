using HousesApp.Models;
using System.Linq.Expressions;

namespace HousesApp.Repository.IRepository
{
    public interface IRepositories<T>where T : class
    {
        Task<List<T>> GetHousesAsync(Expression<Func<T, bool>> filter = null);
        Task<T> GetHouseByIdAsync(Expression<Func<T, bool>> filter = null, bool trackChanges = true);
        Task CreateHouseAsync(T house);
        Task DeleteHouseAsync(T house);
        Task Save();
    }
}
