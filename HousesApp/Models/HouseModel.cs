namespace HousesApp.Models;

public class HouseModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Sqrft { get; set; }
    public int Occupancy { get; set; }
    public string ImageUrl { get; set; }
    public string Amenity { get; set; }
    public DateTime UpdatedDate { get; set; }
    public DateTime CreatedDate { get; set; }

}