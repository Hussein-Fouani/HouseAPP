﻿using System.ComponentModel.DataAnnotations;

namespace HousesApp.Models.Dto;

public class HouseDto
{
    
    public Guid Id { get; set; }
    [Required]
    [MaxLength(100)]
    [MinLength(10)]
    public string Name { get; set; }
    [Required]
    public int Occupancy { get; set; }
    [Required]
    public int SqrFt { get; set; }
    [Required]
    public string ImageUrl { get; set; }
    [Required]

    public string Amenity { get; set; }


}