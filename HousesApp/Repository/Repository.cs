using HousesApp.Db;
using HousesApp.Models;
using HousesApp.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HousesApp.Repository
{
    public class Repository<T> : IRepositories<T> where T : class
    {
        
        internal DbSet<T> dbSet;
        private readonly HousesDb _db;
        public Repository(HousesDb db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }
        public async Task<List<T>> GetHousesAsync(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> houses = dbSet;
            if (filter != null)
            {
                houses = houses.Where(filter);
            }
            return await houses.ToListAsync();
        }

        public async Task<T> GetHouseByIdAsync(Expression<Func<T, bool>> filter = null, bool trackChanges = true)
        {
            var house = dbSet.AsQueryable();
            if (!trackChanges)
            {
                house.AsTracking();
            }
            if (filter != null)
                house = house.Where(filter);
            return await dbSet.FirstOrDefaultAsync();
        }



        public async Task CreateHouseAsync(T house)
        {
            await dbSet.AddAsync(house);
            await Save();
        }

        public async Task DeleteHouseAsync(T id)
        {
            dbSet.Remove(id);
            await Save();
        }


        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
