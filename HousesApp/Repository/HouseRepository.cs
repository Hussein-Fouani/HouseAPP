using System.Linq.Expressions;
using HousesApp.Db;
using HousesApp.Models;
using HousesApp.Models.Dto;
using HousesApp.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HousesApp.Repository;

public class HouseRepository:IHouseRepository
{
    private readonly HousesDb _db;

    public HouseRepository(HousesDb db)
    {
        _db = db;
    }
    public async Task<List<HouseModel>> GetHousesAsync(Expression<Func<HouseModel, bool>> filter = null)
    {
        IQueryable<HouseModel> houses= _db.houses;
        if (filter != null)
        {
            houses=   houses.Where(filter);
        }
       return await houses.ToListAsync();
    }

    public async Task<HouseModel> GetHouseByIdAsync(Expression<Func<HouseModel, bool>> filter = null,bool trackChanges=true)
    {
        var house = _db.houses.AsQueryable();
        if (!trackChanges)
        {
            house.AsTracking();
        }
        if(filter != null)
            house= house.Where(filter);
        return await _db.houses.FirstOrDefaultAsync();
    }

    

    public async Task CreateHouseAsync(HouseModel house)
    {
        await _db.AddAsync(house);
        await Save();
    }

    public async Task DeleteHouseAsync(HouseModel id)
    {
        _db.houses.Remove(id);
        await Save();
    }

    public async Task UpdateHouseAsync(Guid id, HouseModel house)
    {
        var houses  =await _db.houses.FirstOrDefaultAsync(h => h.Id == id);
       
        _db.Update(houses);
        await Save();
        
        
    }

    public async Task Save()
    {
      await  _db.SaveChangesAsync();
    }
}