using System.Linq.Expressions;
using HousesApp.Models;
using HousesApp.Models.Dto;

namespace HousesApp.Repository.IRepository;

public interface IHouseRepository:IRepositories<HouseModel>
{
   
    Task<HouseModel> UpdateHouseAsync(Guid id, HouseModel house);
    
    
}
