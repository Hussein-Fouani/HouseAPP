using System.Linq.Expressions;
using HousesApp.Models;
using HousesApp.Models.Dto;

namespace HousesApp.Repository.IRepository;

public interface IHouseRepository
{
    Task<List<HouseModel>> GetHousesAsync(Expression<Func<HouseModel,bool>>filter = null);
    Task<HouseModel> GetHouseByIdAsync(Expression<Func<HouseModel,bool>>filter = null,bool trackChanges=true);
    Task CreateHouseAsync(HouseModel house);
    Task DeleteHouseAsync(HouseModel house);
    Task UpdateHouseAsync(Guid id, HouseModel house);
    Task Save();
    
}
