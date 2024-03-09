using HousesApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HousesApp.Db; 

public class HousesDb:DbContext
{
    public DbSet<HouseModel> houses { get; set; }

    public HousesDb(DbContextOptions<HousesDb> options):base(options)
    {
       
    }

}