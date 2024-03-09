using System.Linq.Expressions;
using HousesApp.Db;
using HousesApp.Models;
using HousesApp.Models.Dto;
using HousesApp.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HousesApp.Repository;

public class HouseRepository : Repository<HouseModel>, IHouseRepository
{
    private readonly HousesDb _db;

    public HouseRepository(HousesDb db) : base(db)
    {
        _db = db;
    }


    public async Task<HouseModel> UpdateHouseAsync(Guid id, HouseModel house)
    {
        var houses = await _db.houses.FirstOrDefaultAsync(h => h.Id == id);

        _db.Update(houses);
        await Save();
        return houses;


    }



}